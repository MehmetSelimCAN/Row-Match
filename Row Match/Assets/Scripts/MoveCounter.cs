using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveCounter : MonoBehaviour {

    public static MoveCounter Instance { get; private set; }

    [SerializeField] private Text remainingMoveCountText;
    private int remainingMoveCount = 5;
    public int RemainingMoveCount { get { return remainingMoveCount; } }

    private void Awake() {
        Instance = this;
        UpdateRemainingMoveCountText();
    }

    public void SpentMove() {
        remainingMoveCount--;
        UpdateRemainingMoveCountText();
    }

    private void UpdateRemainingMoveCountText() {
        remainingMoveCountText.text = remainingMoveCount.ToString();
    }

}
