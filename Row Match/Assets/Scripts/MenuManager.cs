using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuManager : MonoBehaviour {

    [SerializeField] private Transform levelsPopup;
    [SerializeField] private Button openLevelsPopupButton;
    [SerializeField] private Button closeLevelsPopupButton;
    [SerializeField] private Transform celebrationScreen;
    [SerializeField] private Transform fadeInScreen;

    [SerializeField] private TextMeshProUGUI highscoreText;

    public static bool Highscored;
    public static bool ComingFromGameScene;


    private void Awake() {
        openLevelsPopupButton.onClick.AddListener(() => {
            OpenLevelPopup();
        });

        closeLevelsPopupButton.onClick.AddListener(() => {
            CloseLevelPopup();
        });


        if (ComingFromGameScene) {
            if (Highscored) {
                OpenCelebrationScreen();
                UpdateHighscoreText();
                Highscored = false;
            }
            else {
                OpenFadeInScreen();
                OpenLevelPopup();
            }
        }
    }

    public void OpenLevelPopup() {
        openLevelsPopupButton.gameObject.SetActive(false);
        levelsPopup.gameObject.SetActive(true);
    }

    public void CloseLevelPopup() {
        openLevelsPopupButton.gameObject.SetActive(true);
        levelsPopup.gameObject.SetActive(false);
    }

    private void OpenCelebrationScreen() {
        celebrationScreen.gameObject.SetActive(true);
    }

    private void OpenFadeInScreen() {
        fadeInScreen.gameObject.SetActive(true);
    }

    private void UpdateHighscoreText() {
        highscoreText.SetText(PlayerPrefs.GetInt("CelebrationHighscore").ToString());
    }
}
