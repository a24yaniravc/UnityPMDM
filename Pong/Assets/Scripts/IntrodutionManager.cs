using UnityEngine;

public class IntrodutionManager : MonoBehaviour
{
    [SerializeField] GameObject introductionPanel;
    [SerializeField] GameObject gameElementPanel;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        introductionPanel.SetActive(true);
        if (gameElementPanel != null)
        {
            gameElementPanel.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            introductionPanel.SetActive(false);
            if (gameElementPanel != null)
            {
                gameElementPanel.SetActive(true);
            }
        }
    }
}
