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

    public static ScoreManager Instance;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateScoreText(textScore);
        UpdateEndGameText();
    }

    private void UpdateScoreText(TextMeshProUGUI text)
    {
        text.text = "SCORE: " + score;
    }

    private void UpdateEndGameText()
    {
        endOfGameText.text = "Your final score is: " + score;

    }

    public void AddScore(int scoreToAdd)
    {
        score += scoreToAdd;
        if (score < 0) score = 0;
        Debug.Log(score);
    }

}
