using UnityEngine;

public class SpriteProvider : MonoBehaviour {

    public static SpriteProvider Instance { get; private set; }

    public Sprite CompletedCubeSprite;
    public Sprite RedCubeSprite;
    public Sprite GreenCubeSprite;
    public Sprite BlueCubeSprite;
    public Sprite YellowCubeSprite;

    private void Awake() {
        Instance = this;
    }
}
