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
    [SerializeField] TMP_Text txtHighScore;
    int highScore;

    public void AddPointP1()
    {
        p1Score++;
        txtP1Score.text = p1Score.ToString();
        CheckHighScore();
    }
    public void AddPointP2()
    {
        p2Score++;
        txtP2Score.text = p2Score.ToString();
        CheckHighScore();
    }

    void Start()
    {
        Cursor.visible = false;
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        txtHighScore.text = highScore.ToString();
    }

    void CheckHighScore()
    {
        int currentScore = Mathf.Max(p1Score, p2Score);
        if (currentScore > highScore)
        {
            highScore = currentScore;
            txtHighScore.text = highScore.ToString();
            PlayerPrefs.SetInt("HighScore", highScore);
            PlayerPrefs.Save();
        }
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