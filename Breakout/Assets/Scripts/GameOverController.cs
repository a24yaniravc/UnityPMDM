using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI GameOver;
    //Variable que indica si el juego ya ha rematado
    bool gameOver = false;

    void Update()
    {
        //En la función se comprueba que no estamos en game over y si las vidas han llegado ya 0
        if (!gameOver && GameManager.Lives <= 0)
        {
            //Si se cumple se activa el texto "Game Over"
            GameOver.gameObject.SetActive(true);
            gameOver = true;
        }
        
        //Si el juego ya ha terminado y el usuario presiona cualquier tecla se reinicia el juego
        if (gameOver && Input.anyKeyDown)
        {
            GameManager.ResetGame();
        }
    }
}