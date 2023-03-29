using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour {

    [SerializeField] private Grid Grid;
    [SerializeField] private TextMeshProUGUI scoreText;

    public static int score;

    private void Awake() {
        score = 0;
        Grid.OnRowCompleted += ScoreManager_OnRowCompleted;
    }

    private void ScoreManager_OnRowCompleted(object sender, Grid.OnRowCompletedEventArgs e) {
        GainScore(e.itemType, e.completedCellCount);
    }

    private void GainScore(ItemType itemType, int completedCellCount) {
        score += completedCellCount * (int)itemType;
        UpdateScoreText();
    }

    private void UpdateScoreText() {
        scoreText.SetText(score.ToString());
    }

    public static bool CheckHighscore() {
        if (score > PlayerPrefs.GetInt("Highscore" + LevelManager.levelData.levelName)) {
            SetHighscore();
            MenuManager.Highscored = true;
            return true;
        }

        return false;
    }

    public static void SetHighscore() {
        PlayerPrefs.SetInt("CelebrationHighscore", score);

        PlayerPrefs.SetInt("Highscore" + LevelManager.levelData.levelName, score);
    }
}
