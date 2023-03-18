using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CelebrationAnimationEvents : MonoBehaviour {

    [SerializeField] private Transform openLevelsPopupButton;
    [SerializeField] private Transform levelsPopup;

    public void OpenLevelsPopup() {
        CloseLevelsPopupButton();
        levelsPopup.gameObject.SetActive(true);
    }

    public void CloseLevelsPopupButton() {
        openLevelsPopupButton.gameObject.SetActive(false);
    }
}
