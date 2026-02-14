using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    const int LIVES = 3;
    [SerializeField] int scoreForExtraLife = 500;
    private int nextExtraLifeScore;

    // Variables de datos
    private int score;
    private int maxScore;
    private int lives;
    private bool isGameOver = false;

    // Referencias de UI (Se llenan solas)
    private TextMeshProUGUI txtScore, txtMaxScore, txtMessage;
    private GameObject[] imgLives;

    static GameManager instance;
    public static GameManager GetInstance() => instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            maxScore = PlayerPrefs.GetInt("HighScore", 0);
            InitValues();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void InitValues()
    {
        score = 0;
        lives = LIVES;
        nextExtraLifeScore = scoreForExtraLife;
        isGameOver = false;
        Time.timeScale = 1;
    }

    // ESTO ES LO MÁS IMPORTANTE: La UI llama a esto al aparecer
    public void RegisterUI(TextMeshProUGUI s, TextMeshProUGUI m, TextMeshProUGUI msg, GameObject[] icons)
    {
        txtScore = s;
        txtMaxScore = m;
        txtMessage = msg;
        imgLives = icons;
        UpdateScoreUI(); // Refresca nada más conectar
    }

    public void AddScore(int points)
    {
        if (isGameOver) return;
        score += points;

        if (score > maxScore)
        {
            maxScore = score;
            PlayerPrefs.SetInt("HighScore", maxScore);
        }

        while (score >= nextExtraLifeScore)
        {
            if (lives < (imgLives?.Length ?? 5)) lives++;
            nextExtraLifeScore += scoreForExtraLife;
        }
        UpdateScoreUI();
    }

    public void LoseLife()
    {
        if (isGameOver) return;
        lives--;
        if (lives <= 0) GameOver();
        UpdateScoreUI();
    }

    void GameOver()
    {
        isGameOver = true;
        Time.timeScale = 0;
        if (txtMessage != null) txtMessage.gameObject.SetActive(true);
    }

    void Update()
    {
        if (isGameOver && Input.GetKeyDown(KeyCode.R))
        {
            InitValues();
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    private void UpdateScoreUI()
    {
        if (txtScore != null) txtScore.text = string.Format("{0,5:D5}", score);
        if (txtMaxScore != null) txtMaxScore.text = string.Format("{0,5:D5}", maxScore);
        if (imgLives != null)
        {
            for (int i = 0; i < imgLives.Length; i++)
                imgLives[i].SetActive(i < lives);
        }
    }

    public bool IsGameOverCheck() => isGameOver;
}