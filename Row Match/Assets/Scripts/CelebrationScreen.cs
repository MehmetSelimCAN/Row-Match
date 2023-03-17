using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CelebrationScreen : MonoBehaviour {

    [SerializeField] private Transform openLevelsPopupButton;
    [SerializeField] private Transform levelsPopup;

    public void OpenLevelsPopup() {
        levelsPopup.gameObject.SetActive(true);
        openLevelsPopupButton.gameObject.SetActive(true);
    }

    public void CloseLevelsPopupButton() {
        openLevelsPopupButton.gameObject.SetActive(false);
    }
}
