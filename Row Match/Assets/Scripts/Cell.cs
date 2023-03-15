using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cell : MonoBehaviour {

    [HideInInspector] public int X;
    [HideInInspector] public int Y;
    [HideInInspector] public Vector3Int Position { get { return new Vector3Int(X, Y, 0); } }

    private CubeItem cubeItem;
    public CubeItem CubeItem { get { return cubeItem; } }

    public Text LabelText;

    private float swipeAnimationSpeed = 10f;

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

    public void Move(Vector3Int newPosition) {
        StartCoroutine(MoveAnimation(newPosition));

        X = newPosition.x;
        Y = newPosition.y;
    }

    private IEnumerator MoveAnimation(Vector3Int newPosition) {
        while (Vector3.Distance(transform.localPosition, newPosition) > Mathf.Epsilon) {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, newPosition, Time.deltaTime * swipeAnimationSpeed);
            yield return null;
        }

        transform.localPosition = newPosition;
        UpdateLabel();
    }
}
