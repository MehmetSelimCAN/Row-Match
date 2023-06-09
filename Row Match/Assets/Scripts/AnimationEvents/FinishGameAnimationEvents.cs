using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishGameAnimationEvents : MonoBehaviour {

    public void GameOver() {
        GameManager.GameOver();
    }

    public void BackToMenu() {
        GameManager.BackToMenu();
    }

    public void CheckNextLevelLock() {
        GameManager.CheckNextLevelLock();
    }

    public void LevelFadeOut() {
        Grid.LevelFadeOut();
    }
}
