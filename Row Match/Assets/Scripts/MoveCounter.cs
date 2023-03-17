using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MoveCounter : MonoBehaviour {

    [SerializeField] private ItemSwiper ItemSwiper;

    [SerializeField] private TextMeshProUGUI remainingMoveCountText;
    public static int RemainingMoveCount;

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
    }

    private void UpdateRemainingMoveCountText() {
        remainingMoveCountText.SetText(RemainingMoveCount.ToString());
    }

}
