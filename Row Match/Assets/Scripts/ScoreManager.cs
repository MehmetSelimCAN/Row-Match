using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

    [SerializeField] private Grid Grid;
    [SerializeField] private Text scoreText;

    private int score;

    private void Awake() {
        Grid.OnRowCompleted += ScoreManager_OnRowCompleted;
    }

    private void ScoreManager_OnRowCompleted(object sender, Grid.OnRowCompletedEventArgs e) {
        GainScore(e.itemType, e.completedCellCount);
    }

    private void GainScore(ItemType itemType, int completedCellCount) {
        switch (itemType) {
            case ItemType.RedCube:
                score += (completedCellCount * 100);
                break;
            case ItemType.GreenCube:
                score += (completedCellCount * 150);
                break;
            case ItemType.BlueCube:
                score += (completedCellCount * 200);
                break;
            case ItemType.YellowCube:
                score += (completedCellCount * 250);
                break;
        }

        UpdateScoreText();
    }

    private void UpdateScoreText() {
        scoreText.text = score.ToString();
    }
}
