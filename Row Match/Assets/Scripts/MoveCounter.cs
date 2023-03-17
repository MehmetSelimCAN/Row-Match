using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MoveCounter : MonoBehaviour {

    [SerializeField] private ItemSwiper ItemSwiper;

    [SerializeField] private TextMeshProUGUI remainingMoveCountText;
    public static int remainingMoveCount = 25;

    private void Start() {
        ItemSwiper.OnSwapExecuted += MoveCounter_OnSwapExecuted;

        UpdateRemainingMoveCountText();
    }

    private void MoveCounter_OnSwapExecuted(object sender, ItemSwiper.OnSwapExecutedEventArgs e) {
        SpentMove();
    }

    private void SpentMove() {
        remainingMoveCount--;
        UpdateRemainingMoveCountText();
    }

    private void UpdateRemainingMoveCountText() {
        remainingMoveCountText.SetText(remainingMoveCount.ToString());
    }

}
