using UnityEngine;

public class HitController : MonoBehaviour
{
    const float DELAY = 0.25f;

    [SerializeField] AudioClip clip;

    void Start()
    {
        // Reproducimos un sonido
        AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position);
        // Pasado un tiempo determinado, el objeto se destruirá
        Destroy(gameObject, DELAY);
    }
}