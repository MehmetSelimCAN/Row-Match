using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelButton : MonoBehaviour {

    private LevelData levelData;
    [SerializeField] private LevelName levelName;
    [SerializeField] private Button playButton;
    [SerializeField] private Transform lockedImage;
    [SerializeField] private TextMeshProUGUI levelNameText;
    [SerializeField] private TextMeshProUGUI moveCountText;
    [SerializeField] private TextMeshProUGUI levelScoreInfoText;

    private Animator lockAnimator;

    private void Awake() {
        lockAnimator = GetComponentInChildren<Animator>();

        if (!PlayerPrefs.GetString(LevelName.Level1.ToString()).Equals("Unlocked")) {
            PlayerPrefs.SetString(LevelName.Level1.ToString(), "Unlocked");
        }

        if (PlayerPrefs.GetString(levelName.ToString()).Equals("Unlocking"))        UnlockLevelWithAnimation();
        else if (PlayerPrefs.GetString(levelName.ToString()).Equals("Unlocked"))    UnlockLevel();


        playButton.onClick.AddListener(() => {
            OpenLevel();
        });

        UpdateLevelInformations();
    }

    private void UpdateLevelInformations() {
        levelData = LevelDataFactory.CreateLevelData(levelName);

        UpdateLevelName();
        UpdateMoveCount(levelData.GetMoveCount());
        UpdateHighestScore();
    }

    private void UpdateLevelName() {
        levelNameText.SetText("Level " + (int)levelName + " - ");
    }

    private void UpdateMoveCount(int moveCount) {
        moveCountText.SetText(moveCount + " Moves");
    }

    private void UpdateHighestScore() {
        bool locked = PlayerPrefs.GetString(levelName.ToString()).Equals("Locked");
        if (locked) {
            levelScoreInfoText.SetText("Locked Level");
            return;
        }

        int highestScore = PlayerPrefs.GetInt("Highscore" + levelName);
        if (highestScore > 0) {
            levelScoreInfoText.SetText("Highest Score: " + highestScore);
            return;
        }

        levelScoreInfoText.SetText("No Score");
    }

    private void OpenLevel() {
        LevelManager.levelData = levelData;
        SceneManager.LoadScene("GameScene");
    }

    private void UnlockLevel() {
        lockedImage.gameObject.SetActive(false);
        playButton.gameObject.SetActive(true);
    }

    private void UnlockLevelWithAnimation() {
        lockAnimator.SetTrigger("UnlockLevel");
        PlayerPrefs.SetString(levelName.ToString(), "Unlocked");
    }
}
