using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeItem : MonoBehaviour {

    public SpriteRenderer SpriteRenderer;

    private Cell cell;

    private ItemType itemType;
    public ItemType ItemType { get { return itemType; } }

    public void PrepareCubeItem(ItemType itemType) {
        this.itemType = itemType;
        PrepareSprite();
    }

    public void PrepareSprite() {
        Sprite sprite = GetSpritesForItemType();
        SpriteRenderer = AddSprite(sprite);
    }

    public Sprite GetSpritesForItemType() {
        SpriteProvider spriteProvider = SpriteProvider.Instance;

        switch (itemType) {
            case ItemType.GreenCube:
                return spriteProvider.GreenCubeSprite;
            case ItemType.YellowCube:
                return spriteProvider.YellowCubeSprite;
            case ItemType.BlueCube:
                return spriteProvider.BlueCubeSprite;
            case ItemType.RedCube:
                return spriteProvider.RedCubeSprite;
        }

        return null;
    }

    public SpriteRenderer AddSprite(Sprite sprite) {
        var spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
        spriteRenderer.sprite = sprite;

        return spriteRenderer;
    }

    public void UpdateSprite(Sprite sprite) {
        SpriteRenderer.sprite = sprite;
    }
}
