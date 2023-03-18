using System.Collections;
using UnityEngine;

public class CellBackground : MonoBehaviour {

    private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite lightGridTile;
    [SerializeField] private Sprite darkGridTile;

    private void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Prepare(int x, int y) {
        transform.localPosition = new Vector3(x, y);
        spriteRenderer.sprite = (x + y) % 2 == 0 ? lightGridTile : darkGridTile;
    }

    public IEnumerator FadeOutAnimation() {
        Color newSpriteColor = spriteRenderer.color;
        float alphaValue = 1;
        while (alphaValue > 0) {
            alphaValue -= Time.deltaTime;
            newSpriteColor.a = alphaValue;

            spriteRenderer.color = newSpriteColor;
            yield return null;
        }
    }
}
