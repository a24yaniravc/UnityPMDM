using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    const int LIVES = 3;
    [SerializeField] TextMeshProUGUI txtScore;
    [SerializeField] TextMeshProUGUI txtMaxScore;
    [SerializeField] TextMeshProUGUI txtMessage;
    //Array paara las imágenes que marcan las vidas 
    [SerializeField] GameObject[] imgLives;

    int score;
    int maxScore;
    //Inicializamos las vidas a la constante 
    int lives = LIVES;

    static GameManager instance;

    // Método estático para obtener la instancia del GameManager
    public static GameManager GetInstance()
    {
        return instance;
    }

    private void OnGUI()
    {
        for (int i = 0; i < imgLives.Length; i++)
        {
            imgLives[i].SetActive(i < lives);
        }
        txtScore.text = string.Format("{0,4:D4}", score);
    }

    // Función Awake se ejecuta cuando se instancia el objeto
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Evitar que el objeto se destruya al cambiar de escena
        }
        else if (instance != this)
        {
            // Si ya existe una instancia, destruimos el nuevo GameManager para mantener la singularidad
            Destroy(gameObject);
        }
    }

}