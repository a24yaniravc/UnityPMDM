using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    int p1Score;
    int p2Score;
    bool running = false;
    [SerializeField] GameObject pelota;

    [SerializeField] TMP_Text txtP1Score;
    [SerializeField] TMP_Text txtP2Score;

    public void AddPointP1()
    {
        p1Score++;
        txtP1Score.text = p1Score.ToString();
    }
    public void AddPointP2()
    {
        p2Score++;
        txtP2Score.text = p2Score.ToString();
    }

    void Start()
    {
        Cursor.visible = false;
    }


    void Update()
    {
        if (!running && Input.GetKeyDown(KeyCode.Space))
        {
            // Activamos la pelota 
            pelota.SetActive(true);
            // Indicamos que el juego ha comenzado
            running = true;
        }

        // Si se pulsa la tecla Escape, salimos de la aplicación 
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}