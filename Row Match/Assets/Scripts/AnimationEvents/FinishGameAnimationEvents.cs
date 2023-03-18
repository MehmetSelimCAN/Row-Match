using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishGameAnimationEvents : MonoBehaviour {

    public void BackToMenu() {
        GameManager.BackToMenu();
    }

    public void CheckNextLevelLock() {
        GameManager.CheckNextLevelLock();
    }

    public void CellsFadeOut() {
        Grid.CellsFadeOut();
    }
}
