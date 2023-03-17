using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    [SerializeField] private MoveCounter MoveCounter;
    [SerializeField] private Grid Grid;

    private void Awake() {
        MoveCounter.OnMoveCountFinished += GameManager_OnMoveCountFinished;
        Grid.OnPossibleRowMatchCountReachedZero += GameManager_OnPossibleRowMatchCountReachedZero;
    }

    private void GameManager_OnPossibleRowMatchCountReachedZero(object sender, EventArgs e) {
        FinishGame();
    }

    private void GameManager_OnMoveCountFinished(object sender, EventArgs e) {
        FinishGame();
    }

    private void FinishGame() {
        MenuManager.highScore = true;
        SceneManager.LoadScene("MainMenu");
    }
}
