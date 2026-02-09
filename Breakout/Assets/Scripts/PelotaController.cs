using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class PelotaController : MonoBehaviour
{
    // ID de la escena 
    int sceneId;
    AudioSource sfx;  // Componente AudioSource

    void Start()
    {
        sceneId = SceneManager.GetActiveScene().buildIndex;
        rb = GetComponent<Rigidbody2D>();
        Invoke("LanzarPelota", delay);
        sfx = GetComponent<AudioSource>();
    }

    int brickCount;

    // Mantenemos un registro de los golpes con la pala.
    int contadorGolpes = 0;

    // Definimos la fuerza a aplicar para aumentar la velocidad.
    [SerializeField] float fuerzaIncrementada;

    [SerializeField] AudioClip sfxStart;   // Sonido al iniciar el juego
    [SerializeField] AudioClip sfxPaddel;  // Sonido al chocar con la pala
    [SerializeField] AudioClip sfxBrick;   // Sonido al chocar con un ladrillo
    [SerializeField] AudioClip sfxWall;    // Sonido al chocar con una pared
    [SerializeField] AudioClip sfxFail;    // Sonido al salir por la pared inferior

    Rigidbody2D rb;
    [SerializeField] float delay;
    [SerializeField] float force;

    [SerializeField] GameObject pala;
    bool halved = false;

    // Estructura donde almacenaremos las etiquetas y la puntuación de cada ladrillo
    Dictionary<string, int> ladrillos = new Dictionary<string, int>(){
    {"Ladrillo-Amarilo", 10},
    {"Ladrillo-Verde", 15},
    {"Ladrillo-Naranja", 20},
    {"Ladrillo-Rojo", 25},
    {"Ladrillo-Trigger", 25},
};

    void Update()
    {

    }

    private void LanzarPelota()
    {
        transform.position = Vector3.zero;
        rb.linearVelocity = Vector2.zero;
        float dirX, dirY = -1;
        dirX = Random.Range(0, 2) == 0 ? -1 : 1;
        Vector2 dir = new Vector2(dirX, dirY);
        dir.Normalize();

        rb.AddForce(dir * force, ForceMode2D.Impulse);
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        // Comprobamos si el objeto que estamos atravesando es la pared inferior 
        if (other.tag == "Wall-B")
        {
            // Actualizamos el contador de vidas
            GameManager.UpdateLives();
            if (GameManager.Lives <= 0)
            {
                //Se detiene el movimiento de la pelota
                rb.linearVelocity = Vector2.zero;
                //Se desactiva la pelota
                gameObject.SetActive(false);
                //Se sale del método para que no se relance
                return;
            }

            // Si aún quedan vidas se vuelve a lanzar la pelota
            Invoke("LanzarPelota", delay);


            if (halved)
            {
                HalvePaddle(false);

            }

            sfx.clip = sfxFail;
            sfx.Play();
        }

        if (other.tag == "Ladrillo-Trigger")
        {
            //Sumamos puntos
            GameManager.UpdateScore(ladrillos[other.tag]);
            //Sonido del ladrillo
            sfx.clip = sfxBrick;
            sfx.Play();
            //Se desactiva el collider para que la pelota no detecte el "Trigger" y no sumar puntos
            other.enabled = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        // Almacenamos la etiqueta del objeto con el que estamos colisionando
        string tag = other.gameObject.tag;

        if (!halved && tag == "Wall-T")
        {
            HalvePaddle(true);
        }

        // Comprobamos si la etiqueta es un ladrillo 
        if (ladrillos.ContainsKey(tag))
        {
            DestroyBrick(other.gameObject);
        }

        if (tag == "Pala")
        {
            // Incrementamos el contador de golpes cada vez que la pelota golpea la pala.
            contadorGolpes++;

            // Si el contador de golpes es un múltiplo de 4, incrementamos la velocidad.
            if (contadorGolpes % 4 == 0)
            {
                // Aplicamos una fuerza adicional en la dirección actual de movimiento de la pelota.
                rb.AddForce(rb.linearVelocity * fuerzaIncrementada, ForceMode2D.Impulse);
            }

            // Obtenemos la posición de la pala
            Vector3 pala = other.gameObject.transform.position;
            // Obtenemos el punto de contacto. Cuando colisionan dos objetos, colisionan en una superficie, y devolvería todos los puntos donde colisionan. Nos quedamos con el primero 
            Vector2 contact = other.GetContact(0).point;

            // Comprobamos la dirección en x (para saber si está viajando hacia la izquierda o a la derecha)
            // Si la pelota está viajando desde la izquierda hacia la derecha y está golpeando con la parte derecha de la pala
            // o si la pelota está viajando desde la derecha hacia la izquierda y está golpeando con la parte izquierda de la pala
            if (rb.linearVelocity.x < 0 && contact.x > pala.x ||
                    rb.linearVelocity.x > 0 && contact.x < pala.x)
            {
                rb.linearVelocity = new Vector2(-rb.linearVelocity.x, rb.linearVelocity.y);
            }

            sfx.clip = sfxPaddel;
            sfx.Play();
        }

        if (tag == "Wall-L" || tag == "Wall-T" || tag == "Wall-R" || tag == "Ladrillo-Gris")
        {
            sfx.clip = sfxWall;
            sfx.Play();
        }
    }

    public void DestroyBrick(GameObject obj)
{
    // 1. Verificación de seguridad: si el tag ya no es de ladrillo, ignoramos
    if (!ladrillos.ContainsKey(obj.tag)) return;

    sfx.clip = sfxBrick;
    sfx.Play();

    // 2. Sumamos puntos usando el tag actual
    GameManager.UpdateScore(ladrillos[obj.tag]);

    // 3. ¡ESTA ES LA CLAVE!: Cambiamos el tag ANTES de destruir
    // Al cambiarlo a "Untagged", si hay otra colisión en este frame,
    // el 'if (ladrillos.ContainsKey(tag))' del OnCollision dará falso y no entrará aquí.
    obj.tag = "Untagged"; 

    // 4. Lo hacemos invisible e intangible inmediatamente
    obj.SetActive(false); 
    Destroy(obj);

    // 5. Contamos el ladrillo
    ++brickCount;
    
    int requiredBricks = GameManager.totalBricks[SceneManager.GetActiveScene().buildIndex];
    Debug.Log($"Ladrillos: {brickCount} / Requeridos: {requiredBricks}");

    if (brickCount >= requiredBricks && requiredBricks > 0) 
    {
        rb.linearVelocity = Vector2.zero;
        sfx.clip = sfxStart;
        sfx.Play();
        Invoke("NextScene", 3);
    }
}

    void NextScene()
    {
        int nextId = sceneId + 1;
        if (nextId == SceneManager.sceneCountInBuildSettings)
        {
            nextId = 0;
        }
        SceneManager.LoadScene(nextId);
    }

    public void HalvePaddle(bool reducir)
    {
        halved = reducir;
        Vector3 escalaActual = pala.transform.localScale;
        pala.transform.localScale = reducir ?
            new Vector3(escalaActual.x * 0.5f, escalaActual.y, escalaActual.z) :
            new Vector3(escalaActual.x * 2f, escalaActual.y, escalaActual.z);
    }
}