using UnityEngine;
using UnityEngine.UI;

public class LevelScroll : MonoBehaviour {

    private ScrollRect scrollRect;

    private void Awake() {
        scrollRect = GetComponent<ScrollRect>();
        scrollRect.verticalNormalizedPosition = PlayerPrefs.GetFloat("LevelScrollRectPosition", 1);
    }

    public void UpdateScrollRectValue() {
        PlayerPrefs.SetFloat("LevelScrollRectPosition", scrollRect.verticalNormalizedPosition);
    }
}
