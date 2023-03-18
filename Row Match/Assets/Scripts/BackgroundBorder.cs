using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundBorder : MonoBehaviour {

    private SpriteRenderer spriteRenderer;

    private void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Prepare(BackgroundBorderLocation location) {
        switch (location) {
            case BackgroundBorderLocation.Top:
                transform.localPosition = new Vector3((Grid.Cols - 1) / 2f, Grid.Rows);
                spriteRenderer.size = new Vector2(Grid.Cols, 1);
                break;
            case BackgroundBorderLocation.Bottom:
                transform.localPosition = new Vector3((Grid.Cols - 1) / 2f, -1);
                spriteRenderer.size = new Vector2(Grid.Cols, 1);

                spriteRenderer.flipY = true;
                break;
            case BackgroundBorderLocation.Left:
                transform.localPosition = new Vector3(-1, (Grid.Rows - 1) / 2f);
                spriteRenderer.size = new Vector2(Grid.Rows, 1);

                transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 90f));
                break;
            case BackgroundBorderLocation.Right:
                transform.localPosition = new Vector3(Grid.Cols, (Grid.Rows - 1) / 2f);
                spriteRenderer.size = new Vector2(Grid.Rows, 1);

                transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 90f));
                spriteRenderer.flipY = true;
                break;
        }
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
