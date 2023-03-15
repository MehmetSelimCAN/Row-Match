using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour {

    public const int Rows = 7;
    public const int Cols = 5;

    public Transform CellsParent;

    [SerializeField] private Cell cellPrefab;

    public readonly Cell[,] Cells = new Cell[Cols, Rows];

    private void Start() {
        CreateCells();
        PrepareCells();
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

    public void SwapCells(Cell firstCell, Cell secondCell) {
        Cells[firstCell.X, firstCell.Y] = secondCell;
        Cells[secondCell.X, secondCell.Y] = firstCell;

        Vector3Int tempPosition = firstCell.Position;
        firstCell.Move(secondCell.Position);
        secondCell.Move(tempPosition);
    }
}
