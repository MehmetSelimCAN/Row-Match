using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteProvider : MonoBehaviour {

    public static SpriteProvider Instance { get; private set; }

    public Sprite RedCubeSprite;
    public Sprite GreenCubeSprite;
    public Sprite BlueCubeSprite;
    public Sprite YellowCubeSprite;

    private void Awake() {
        Instance = this;
    }
}
