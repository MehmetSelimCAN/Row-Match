using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cell : MonoBehaviour {

    [HideInInspector] public int X;
    [HideInInspector] public int Y;
    [HideInInspector] public Vector2Int Position { get { return new Vector2Int(X, Y); } }

    private CubeItem cubeItem;
    public CubeItem CubeItem { get { return cubeItem; } }

    public Text LabelText;

    public void Prepare(int x, int y) {
        X = x;
        Y = y;
        transform.localPosition = new Vector3(x, y);
        UpdateLabel();

        ItemType itemType = ItemFactory.GetRandomCubeItemType();
        InsertItem(itemType);
    }

    private void UpdateLabel() {
        var cellName = X + ":" + Y;
        LabelText.text = cellName;
        gameObject.name = "Cell " + cellName;
    }

    public void InsertItem(ItemType itemType) {
        if (cubeItem != null) return;

        cubeItem = ItemFactory.Instance.CreateCubeItem(this, itemType);
        cubeItem.transform.position = transform.position;
    }

    public void Move(Vector2Int newPosition) {
        X = newPosition.x;
        Y = newPosition.y;

        transform.position = new Vector3Int(X, Y, 0);
        UpdateLabel();
    }
}
