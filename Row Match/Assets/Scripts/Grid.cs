using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour {

    [SerializeField] private ItemSwiper ItemSwiper;

    public const int Rows = 7;
    public const int Cols = 5;

    public Transform CellsParent;

    [SerializeField] private Cell cellPrefab;

    public readonly Cell[,] Cells = new Cell[Cols, Rows];

    private List<int> completedRowIndexes = new List<int>();

    public event EventHandler<OnRowCompletedEventArgs> OnRowCompleted;
    public class OnRowCompletedEventArgs : EventArgs {
        public ItemType itemType;
        public int completedCellCount;
    }

    private void Start() {
        ItemSwiper.OnSwapExecuted += Grid_OnSwapExecuted;

        CreateCells();
        PrepareCells();
    }

    private void Grid_OnSwapExecuted(object sender, ItemSwiper.OnSwapExecutedEventArgs e) {
        SwapCells(e.firstCell, e.secondCell);
        CheckEveryRow();
    }

    private void CreateCells() {
        for (int y = 0; y < Rows; y++) {
            for (int x = 0; x < Cols; x++) {
                var cell = Instantiate(cellPrefab, Vector3.zero, Quaternion.identity, CellsParent);
                Cells[x, y] = cell;
            }
        }
    }

    private void PrepareCells() {
        for (int y = 0; y < Rows; y++) {
            for (int x = 0; x < Cols; x++) {
                Cells[x, y].Prepare(x, y);
            }
        }
    }

    private void SwapCells(Cell firstCell, Cell secondCell) {
        Cells[firstCell.X, firstCell.Y] = secondCell;
        Cells[secondCell.X, secondCell.Y] = firstCell;

        Vector3Int tempPosition = firstCell.Position;
        firstCell.Move(secondCell.Position);
        secondCell.Move(tempPosition);
    }

    private void CheckEveryRow() {
        for (int x = 0; x < Rows; x++) {
            if (completedRowIndexes.Contains(x)) continue;

            int matchedCellsCountWithFirstCell = 0;
            for (int y = 0; y < Cols - 1; y++) {
                if (Cells[y, x].Item.ItemType == Cells[y + 1, x].Item.ItemType) {
                    matchedCellsCountWithFirstCell++;
                }
            }

            if (matchedCellsCountWithFirstCell == Cols - 1) {
                ItemType completedRowItemType = Cells[0, x].Item.ItemType;

                OnRowCompleted?.Invoke(this, new OnRowCompletedEventArgs {
                    itemType = completedRowItemType,
                    completedCellCount = matchedCellsCountWithFirstCell + 1
                });

                completedRowIndexes.Add(x);
                UpdateCompletedRow(x);
            }
        }
    }

    private void UpdateCompletedRow(int rowIndex) {
        for (int y = 0; y < Cols; y++) {
            Cells[y, rowIndex].Item.ChangeItemType(ItemType.CompletedCube);
        }
    }
}
