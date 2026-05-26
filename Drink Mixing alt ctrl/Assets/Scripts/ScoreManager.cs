using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] public static int score;
    [SerializeField] private TextMeshProUGUI textScore;

    [Header("Score values")]
    [SerializeField] private int smallScoreAward;
    [SerializeField] private int mediumScoreAward;
    [SerializeField] private int highScoreAward;
    [SerializeField] private int tipAmmount;

    [Header("end of game UI")]
    [SerializeField] private TextMeshProUGUI endOfGameText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        updateScoreText(textScore);
        updateEndGameText();
    }

    private void updateScoreText(TextMeshProUGUI text)
    {
        text.text = "SCORE: " + score;
    }

    private void updateEndGameText()
    {
        endOfGameText.text = "Your final score is: " + score;

    }

    public void addScore(int scoreToAdd)
    {
        score += scoreToAdd;
    }

}
