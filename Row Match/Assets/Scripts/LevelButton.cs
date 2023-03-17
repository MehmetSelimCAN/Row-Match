using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelButton : MonoBehaviour {

    private LevelData levelData;
    [SerializeField] private LevelName levelName;
    [SerializeField] private Button playButton;
    [SerializeField] private TextMeshProUGUI levelNameText;
    [SerializeField] private TextMeshProUGUI moveCountText;
    [SerializeField] private TextMeshProUGUI highestScoreText;

    private void Awake() {
        UpdateLevelInformations();

        playButton.onClick.AddListener(() => {
            OpenLevel();
        });
    }

    private void UpdateLevelInformations() {
        levelData = LevelDataFactory.CreateLevelData(levelName);

        UpdateLevelName();
        UpdateMoveCount(levelData.GetMoveCount());
        UpdateHighestScore();
    }

    private void UpdateLevelName() {
        levelNameText.SetText("Level " + (int)levelName + " - ");
    }

    private void UpdateMoveCount(int moveCount) {
        moveCountText.SetText(moveCount + " Moves");
    }

    private void UpdateHighestScore() {
        string highestScore = PlayerPrefs.GetString("HighestScore" + levelName);
        highestScoreText.SetText(highestScore);
    }

    private void OpenLevel() {
        LevelManager.levelData = levelData;
        SceneManager.LoadScene("GameScene");
    }
}
