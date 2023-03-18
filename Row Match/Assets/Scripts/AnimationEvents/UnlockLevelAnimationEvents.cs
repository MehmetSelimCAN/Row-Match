using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnlockLevelAnimationEvents : MonoBehaviour {

    [SerializeField] private Button playButton;

    public void EnablePlayButton() {
        playButton.gameObject.SetActive(true);
    }
}
