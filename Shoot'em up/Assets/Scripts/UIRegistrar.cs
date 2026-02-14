using UnityEngine;
using TMPro;

public class UIRegistrar : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI txtScore;
    [SerializeField] TextMeshProUGUI txtMaxScore;
    [SerializeField] TextMeshProUGUI txtMessage;
    [SerializeField] GameObject[] imgLives;

    void Start()
    {
        // Le pasamos nuestras referencias locales al GameManager persistente
        if (GameManager.GetInstance() != null)
        {
            GameManager.GetInstance().RegisterUI(txtScore, txtMaxScore, txtMessage, imgLives);
        }
    }
}