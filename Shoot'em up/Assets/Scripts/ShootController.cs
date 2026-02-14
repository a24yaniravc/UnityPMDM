using UnityEngine;

public class ShootController : MonoBehaviour
{
    // Velocidad de los disparos
    [SerializeField] float speed;

    // Tiempo que duran los disparos antes de autodestruirse
    [SerializeField] float lifetime;
    [SerializeField] GameObject hit;

    void Start()
    {
        // Destruir el disparo después de un cierto tiempo
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        // Mover el disparo hacia arriba
        transform.Translate(Vector3.up * speed * Time.deltaTime);
    }

    // Método para destruir el disparo cuando sale de la pantalla
    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "enemy")
        {
            Instantiate(hit, transform.position, Quaternion.identity);
            
            // El enemigo explota y suma puntos
            other.gameObject.SendMessage("DestroyEnemy", SendMessageOptions.DontRequireReceiver);
            Destroy(gameObject);
        }
    }
}