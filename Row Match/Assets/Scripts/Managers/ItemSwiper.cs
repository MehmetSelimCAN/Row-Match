using System;
using UnityEngine;

public class ItemSwiper : MonoBehaviour {

    private const string CellCollider = "CellCollider";

    [SerializeField] private Camera Camera;

    Vector2 dragStartPosition = Vector2.zero;
    Vector2 dragCurrentPosition = Vector2.zero;

    float minimumDistanceToSwipe = 0.75f;

    private bool swapExecuted = false;

    public event EventHandler<OnSwapExecutedEventArgs> OnSwapExecuted;
    public class OnSwapExecutedEventArgs : EventArgs {
        public Cell firstCell;
        public Cell secondCell;
    }

    private void Awake() {
        Camera = Camera.main;
    }

    private void Update() {
        HandleDragging();
    }

    private void HandleDragging() {
        if (GameManager.isGameOver) return;
        if (MoveCounter.RemainingMoveCount == 0) return;

        if (Input.GetMouseButtonDown(0)) {
            dragStartPosition = Camera.ScreenToWorldPoint(Input.mousePosition);
            swapExecuted = false;
        }

        if (Input.GetMouseButton(0)) {
            dragCurrentPosition = Camera.ScreenToWorldPoint(Input.mousePosition);
            if (Vector2.Distance(dragStartPosition, dragCurrentPosition) > minimumDistanceToSwipe) {
                if (!swapExecuted) {
                    TryToExecuteSwapping(dragStartPosition, dragCurrentPosition);
                }
            }
        }
    }

    private void TryToExecuteSwapping(Vector2 dragStartPosition, Vector2 dragCurrentPosition) {
        Vector2 swipeDirection = FindSwipeDirection(dragStartPosition, dragCurrentPosition);
        var swipePosition = dragStartPosition + swipeDirection;

        var firstCellHit = Physics2D.OverlapPoint(dragStartPosition) as BoxCollider2D;
        var secondCellHit = Physics2D.OverlapPoint(swipePosition) as BoxCollider2D;

        if (firstCellHit == null || secondCellHit == null) return;
        if (!firstCellHit.CompareTag(CellCollider) || !secondCellHit.CompareTag(CellCollider)) return;

        Cell firstCell = firstCellHit.gameObject.GetComponent<Cell>();
        Cell secondCell = secondCellHit.gameObject.GetComponent<Cell>();

        if (firstCell.Item.ItemType == ItemType.CompletedCube) return;
        if (secondCell.Item.ItemType == ItemType.CompletedCube) return;

        OnSwapExecuted?.Invoke(this, new OnSwapExecutedEventArgs {
            firstCell = firstCell,
            secondCell = secondCell
        });

        swapExecuted = true;
    }

    private Vector2 FindSwipeDirection(Vector2 dragStartPosition, Vector2 dragCurrentPosition) {
        Vector2 dragVector = dragCurrentPosition - dragStartPosition;
        float positiveX = Mathf.Abs(dragVector.x);
        float positiveY = Mathf.Abs(dragVector.y);

        Vector2 swipeDirection;
        if (positiveX > positiveY) {
            swipeDirection = (dragVector.x > 0) ? Vector2.right : Vector2.left;
        }
        else {
            swipeDirection = (dragVector.y > 0) ? Vector2.up : Vector2.down;
        }

        return swipeDirection;
    }
}
