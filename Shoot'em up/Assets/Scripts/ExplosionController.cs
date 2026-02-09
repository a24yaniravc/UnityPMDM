using UnityEngine;

public class ExplosionController : MonoBehaviour
{
    // Tiempo de espera antes de destruir la explosión
    const float DELAY = 0.25f;
    // Sonido de la explosión
    [SerializeField] AudioClip explosionSound;

    void Start()
    {
        // Reproducir sonido de la explosión en la posición de la cámara
        AudioSource.PlayClipAtPoint(explosionSound, Camera.main.transform.position);
        // Destruir la explosión después de un cierto tiempo
        Destroy(gameObject, DELAY);
    }
}
