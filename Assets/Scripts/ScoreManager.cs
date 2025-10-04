using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{

    public static ScoreManager instance;

    public TextMeshProUGUI scoreText;
    int score = 0;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        scoreText.gameObject.SetActive(false); // An diem khi vo game
        scoreText.text = "Score: " + score.ToString();
    }

    public void AddPoint()
    {
        score += 1;
        scoreText.text = "Score: " + score.ToString();
    }

    public void ShowScore()
    {
        scoreText.gameObject.SetActive(true); //  thang hien diem
    }
}
