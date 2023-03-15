using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveCounter : MonoBehaviour {

    [SerializeField] private ItemSwiper ItemSwiper;

    [SerializeField] private Text remainingMoveCountText;
    private int remainingMoveCount = 25;
    public int RemainingMoveCount { get { return remainingMoveCount; } }

    private void Awake() {
        UpdateRemainingMoveCountText();
    }

    private void Start() {
        ItemSwiper.OnSwapExecuted += MoveCounter_OnSwapExecuted;
    }

    private void MoveCounter_OnSwapExecuted(object sender, ItemSwiper.OnSwapExecutedEventArgs e) {
        SpentMove();
    }

    private void SpentMove() {
        remainingMoveCount--;
        UpdateRemainingMoveCountText();
    }

    private void UpdateRemainingMoveCountText() {
        remainingMoveCountText.text = remainingMoveCount.ToString();
    }

}
