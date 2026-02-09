using UnityEngine;
using System.Collections;

public class PelotaController : MonoBehaviour
{
    [SerializeField] AudioClip sfxPaddel;  // Sonido al chocar con la pala
    [SerializeField] AudioClip sfxWall;    // Sonido al chocar con una pared
    [SerializeField] AudioClip sfxStart;    // Sonido al empezar el juego
    [SerializeField] AudioClip sfxGoal;    // Sonido al salir por la portería

    Rigidbody2D rb;
    [SerializeField] float force = 1f;
    [SerializeField] float delay;
    [SerializeField] GameManager gameManager;

    // Definimos dos constantes para el ángulo mínimo y máximo en grados.
    const float MIN_ANG = 25.0f;
    const float MAX_ANG = 40.0f;


    // Declaramos dos constantes con las posiciones y máximas y mínimas.
    const float MAX_Y = 2.5f;
    const float MIN_Y = -2.5f;

    AudioSource sfx;  // Componente AudioSource

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sfx = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();
        transform.position = new Vector3(0, 0, 0); //Vector3.zero;

        int directionX = Random.Range(0, 2) == 0 ? -1 : 1; // El límite superior es exclusivo (el 2 quedaría fuera).
        AudioSource.PlayClipAtPoint(sfxStart, transform.position);
        StartCoroutine(LanzarPelota(directionX));
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator LanzarPelota(int direccionX)
    {
        yield return new WaitForSeconds(delay);

        // Reset velocity to zero before launching
        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;

        // Posición Y aleatoria dentro de los límites establecidos.
        float posY = Random.Range(MIN_Y, MAX_Y);
        transform.position = new Vector3(0, posY, 0);

        // Definimos el ángulo en radianes usando Range, especificando el mínimo y máximo.
        float angulo = Random.Range(MIN_ANG, MAX_ANG) * Mathf.Deg2Rad;
        float x = Mathf.Cos(angulo) * direccionX;

        // Determinamos si nos movemos hacia la derecha o izquierda.
        // Si el valor devuelto es 0, la dirección en Y será negativa; si es 1, será positiva.
        int direccionY = Random.Range(0, 2) == 0 ? -1 : 1;
        float y = Mathf.Sin(angulo) * direccionY;

        Vector2 impulso = new Vector2(x, y);
        rb.AddForce(new Vector2(x, y) * force, ForceMode2D.Impulse);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Gol en " + other.tag + "!!");

        if (other.tag == "PorteriaEsquerda")
        {
            // Lanzaremos la pelota hacia la derecha
            gameManager.AddPointP1();
            StartCoroutine(LanzarPelota(1));
        }
        else if (other.tag == "PorteriaDereita")
        {
            // Lanzaremos la pelota hacia la izquierda
            gameManager.AddPointP2();
            StartCoroutine(LanzarPelota(-1));
        }

        sfx.clip = sfxGoal;
        sfx.Play();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        string tag = other.gameObject.tag;

        if (tag == "Pala1" || tag == "Pala2")
        {
            sfx.clip = sfxPaddel;
            sfx.Play();
        }
        else if (tag == "Wall")
        {
            sfx.clip = sfxWall;
            sfx.Play();
        }
    }
}
