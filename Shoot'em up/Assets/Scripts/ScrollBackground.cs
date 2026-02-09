using UnityEngine;

public class ScrollBackground : MonoBehaviour
{
    // Añadimos un campo para la velocidad a la que se desplazarán las imágenes
    [SerializeField] float speed;
    // Altura de la imagen para determinar cuándo pasarla hacia arriba. La inicializaremos en el método Start()
    float height;

    // Start is called before the first frame update
    void Start()
    {
        // Accedemos a la propiedad SpriteRenderer, a su tamaño y su altura
        height = GetComponent<SpriteRenderer>().bounds.size.y;
    }

    // Update is called once per frame
    void Update()
    {
        // En qué dirección nos movemos y cuánto movemos. Vector.down (0, -1, 0)
        transform.Translate(Vector3.down * speed * Time.deltaTime);

        // Reposicionamos cuando el centro de la imagen haya recorrido toda su altura
        if (transform.position.y < -height)
        {
            // Queremos desplazar el doble de la altura, porque queremos saltar la imagen que está saliendo por la parte inferior
            transform.Translate(Vector3.up * 2 * height);
        }
    }
}