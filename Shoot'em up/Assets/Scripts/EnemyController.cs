using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // Velocidad de caída de la nave enemiga
    [SerializeField] float speed;
    [SerializeField] GameObject explosionPrefab; // Prefab de la explosión

    // Altura a la que se destruirá la nave enemiga
    const float DESTROY_HEIGHT = -6f;

    void Update()
    {
        // Movimiento hacia abajo
        transform.Translate(Vector3.down * speed * Time.deltaTime);

        // Destruir la nave enemiga cuando alcanza la altura de destrucción
        if (transform.position.y < DESTROY_HEIGHT)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        DestroyEnemy();
    }

    // Método llamado cuando el objeto entra en contacto con otro collider
    private void OnTriggerEnter2D(Collider2D other)
    {
        DestroyEnemy(); // Destruir la nave enemiga
    }

    // Destruir la nave enemiga y crear una explosión
    void DestroyEnemy()
    {
        // Instanciar la animación de la explosión en la posición de la nave enemiga
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        // Destruir la nave enemiga
        Destroy(gameObject);
    }
}
