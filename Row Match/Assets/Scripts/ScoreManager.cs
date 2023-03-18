using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
        switch (itemType) {
            case ItemType.RedCube:
                score += (completedCellCount * (int)ItemType.RedCube);
                break;
            case ItemType.GreenCube:
                score += (completedCellCount * (int)ItemType.GreenCube);
                break;
            case ItemType.BlueCube:
                score += (completedCellCount * (int)ItemType.BlueCube);
                break;
            case ItemType.YellowCube:
                score += (completedCellCount * (int)ItemType.YellowCube);
                break;
        }

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
