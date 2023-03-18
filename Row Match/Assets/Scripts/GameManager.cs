using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    [SerializeField] private MoveCounter MoveCounter;
    [SerializeField] private Grid Grid;

    [SerializeField] private Animator UI_Animator;

    private void Awake() {
        MoveCounter.OnMoveCountFinished += GameManager_OnMoveCountFinished;
        Grid.OnPossibleRowMatchCountReachedZero += GameManager_OnPossibleRowMatchCountReachedZero;
    }

    private void GameManager_OnPossibleRowMatchCountReachedZero(object sender, EventArgs e) {
        FinishGameAnimation();
    }

    private void GameManager_OnMoveCountFinished(object sender, EventArgs e) {
        FinishGameAnimation();
    }

    private void FinishGameAnimation() {
        UI_Animator.SetTrigger("FinishGame");
    }

    public static void CheckNextLevelLock() {
        //Unlock next level
        string nextLevelName = (LevelManager.levelData.levelName + 1).ToString();
        if (ScoreManager.score > 0 && PlayerPrefs.GetString(nextLevelName).Equals("Locked")) {
            PlayerPrefs.SetString(nextLevelName, "Unlocking");
        }
    }

    public static void BackToMenu() {
        ScoreManager.CheckHighscore();
        MenuManager.ComingFromGameScene = true;
        SceneManager.LoadScene("MainMenu");
    }
}
