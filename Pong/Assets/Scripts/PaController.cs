using UnityEngine;

public class PaController : MonoBehaviour
{
    const float MAX_Y = 3.75f;
    const float MIN_Y = -3.70f;
    [SerializeField] float speed = 6f;

    void Update()
    {
        if (CompareTag("Pala2"))
        {
            if (Input.GetKey(KeyCode.UpArrow) && transform.position.y < MAX_Y)
            {
                transform.Translate(Vector3.up * speed * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.DownArrow) && transform.position.y > MIN_Y)
            {
                transform.Translate(Vector3.down * speed * Time.deltaTime);
            }
        }
        else if (CompareTag("Pala1"))
        {
            if (Input.GetKey(KeyCode.W) && transform.position.y < MAX_Y)
            {
                transform.Translate(Vector3.up * speed * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.S) && transform.position.y > MIN_Y)
            {
                transform.Translate(Vector3.down * speed * Time.deltaTime);
            }
        }
    }
}
