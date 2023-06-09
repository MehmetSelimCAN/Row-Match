using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    [SerializeField] private MoveCounter MoveCounter;
    [SerializeField] private Grid Grid;

    [SerializeField] private Animator UI_Animator;

    public static bool isGameOver;

    private void Awake() {
        isGameOver = false;

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

    public static void GameOver() {
        isGameOver = true;
    }

    public static void CheckNextLevelLock() {
        //Unlock next level
        string nextLevelName = (LevelManager.levelData.levelName + 1).ToString();
        bool nextLevelUnlocked = PlayerPrefs.GetString(nextLevelName).Equals("Unlocked");
        if (ScoreManager.score > 0 && !nextLevelUnlocked) {
            PlayerPrefs.SetString(nextLevelName, "Unlocking");
        }
    }

    public static void BackToMenu() {
        ScoreManager.CheckHighscore();
        MenuManager.ComingFromGameScene = true;
        SceneManager.LoadScene("Menu");
    }
}
