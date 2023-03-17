using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour {

    [SerializeField] private Transform levelsPopup;
    [SerializeField] private Button openLevelsPopupButton;
    [SerializeField] private Button closeLevelsPopupButton;
    [SerializeField] private Transform celebrationScreen;
    public static bool highScore;


    private void Awake() {
        openLevelsPopupButton.onClick.AddListener(() => {
            OpenLevelPopup();
        });

        closeLevelsPopupButton.onClick.AddListener(() => {
            CloseLevelPopup();
        });


        if (highScore) {
            celebrationScreen.gameObject.SetActive(true);
        }
    }

    public void OpenLevelPopup() {
        levelsPopup.gameObject.SetActive(true);
    }

    public void CloseLevelPopup() {
        levelsPopup.gameObject.SetActive(false);
    }
}
