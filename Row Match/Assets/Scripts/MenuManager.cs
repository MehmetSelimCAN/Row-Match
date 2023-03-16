using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour {

    [SerializeField] private Transform levelPopup;

    public void OpenLevelPopup() {
        levelPopup.gameObject.SetActive(true);
    }

    public void CloseLevelPopup() {
        levelPopup.gameObject.SetActive(false);
    }
}
