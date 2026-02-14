using UnityEngine;

public class AsteroidsController : MonoBehaviour
{
    [SerializeField] float minSpeedY = 3f;
    [SerializeField] float maxSpeedY = 6f;
    [SerializeField] float speedX = 2f; // Velocidad horizontal fija para el rebote

    private float limiteDerecho = 4.16f;
    private float limiteIzquierdo = -4.16f;
    [SerializeField] float destroyY = -7f;

    [SerializeField] int scoreValue = 50;
    [SerializeField] GameObject explosionPrefab;

    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        rb.mass = 1f;

        // Dirección inicial aleatoria (Izquierda o Derecha)
        float dirX = Random.value > 0.5f ? 1f : -1f;
        float sY = Random.Range(minSpeedY, maxSpeedY);
        
        rb.linearVelocity = new Vector2(dirX * speedX, -sY);
        rb.angularVelocity = Random.Range(-40f, 40f);
    }

    void Update()
    {
        ManejarRebote();

        // Destrucción por abajo (Esto se mantiene igual)
        if (transform.position.y < destroyY)
        {
            Destroy(gameObject);
        }
    }

    void ManejarRebote()
    {
        Vector2 vel = rb.linearVelocity;

        // REBOTE DERECHA
        if (transform.position.x >= limiteDerecho && vel.x > 0)
        {
            // Invertimos la velocidad
            rb.linearVelocity = new Vector2(-vel.x, vel.y);
            // Lo movemos un pelín a la izquierda para que no se quede pegado
            transform.position = new Vector3(limiteDerecho - 0.1f, transform.position.y, 0);
        }
        // REBOTE IZQUIERDA
        else if (transform.position.x <= limiteIzquierdo && vel.x < 0)
        {
            // Invertimos la velocidad
            rb.linearVelocity = new Vector2(-vel.x, vel.y);
            // Lo movemos un pelín a la derecha
            transform.position = new Vector3(limiteIzquierdo + 0.1f, transform.position.y, 0);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("shoot") || other.gameObject.CompareTag("Player"))
        {
            Explode();
        }
    }

    public void Explode()
    {
        if (GameManager.GetInstance() != null)
            GameManager.GetInstance().AddScore(scoreValue);

        if (explosionPrefab != null)
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);

        Destroy(gameObject);
    }
}