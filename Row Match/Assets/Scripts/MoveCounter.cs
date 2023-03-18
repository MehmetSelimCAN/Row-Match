using System;
using UnityEngine;
using TMPro;

public class MoveCounter : MonoBehaviour {

    [SerializeField] private ItemSwiper ItemSwiper;

    [SerializeField] private TextMeshProUGUI remainingMoveCountText;
    public static int RemainingMoveCount;

    public event EventHandler OnMoveCountFinished;

    private void Start() {
        ItemSwiper.OnSwapExecuted += MoveCounter_OnSwapExecuted;

        UpdateRemainingMoveCountText();
    }

    private void MoveCounter_OnSwapExecuted(object sender, ItemSwiper.OnSwapExecutedEventArgs e) {
        SpentMove();
    }

    private void SpentMove() {
        RemainingMoveCount--;
        UpdateRemainingMoveCountText();

        if (RemainingMoveCount == 0) {
            OnMoveCountFinished?.Invoke(this, EventArgs.Empty);
        }
    }

    private void UpdateRemainingMoveCountText() {
        remainingMoveCountText.SetText(RemainingMoveCount.ToString());
    }

}
