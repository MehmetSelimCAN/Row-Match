using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour {

    [SerializeField] private ItemSwiper ItemSwiper;

    public static int Rows;
    public static int Cols;

    public Transform CellsParent;
    [SerializeField] private Cell cellPrefab;
    public static Cell[,] Cells;

    public Transform CellsBackgroundParent;
    [SerializeField] private CellBackground cellBackgroundPrefab;
    public static CellBackground[,] CellsBackground;

    [SerializeField] private SpriteRenderer gridTopBorder;
    [SerializeField] private SpriteRenderer gridBottomBorder;
    [SerializeField] private SpriteRenderer gridLeftBorder;
    [SerializeField] private SpriteRenderer gridRightBorder;

    private HashSet<int> completedRowIndexes = new HashSet<int>();
    private HashSet<int> cannotBeCompletedRowIndexes = new HashSet<int>();

    public event EventHandler<OnRowCompletedEventArgs> OnRowCompleted;
    public class OnRowCompletedEventArgs : EventArgs {
        public ItemType itemType;
        public int completedCellCount;
    }

    public event EventHandler OnPossibleRowMatchCountReachedZero;

    private void Start() {
        ItemSwiper.OnSwapExecuted += Grid_OnSwapExecuted;
        completedRowIndexes.Add(-1);
        completedRowIndexes.Add(Rows);
    }

    private void Grid_OnSwapExecuted(object sender, ItemSwiper.OnSwapExecutedEventArgs e) {
        SwapCells(e.firstCell, e.secondCell);
        CheckEveryRow();
    }

    public void Prepare() {
        CreateCellsBackground();
        PrepareCellsBackground();
        PrepareBackgroundBorders();
        CreateCells();
        PrepareCells();
    }

    private void CreateCellsBackground() {
        for (int y = 0; y < Rows; y++) {
            for (int x = 0; x < Cols; x++) {
                var cellBackground = Instantiate(cellBackgroundPrefab, Vector3.zero, Quaternion.identity, CellsBackgroundParent);
                CellsBackground[x, y] = cellBackground;
            }
        }
    }

    private void PrepareCellsBackground() {
        for (int y = 0; y < Rows; y++) {
            for (int x = 0; x < Cols; x++) {
                CellsBackground[x, y].Prepare(x, y);
            }
        }
    }

    private void PrepareBackgroundBorders() {
        gridTopBorder.transform.localPosition = new Vector3((Cols - 1) / 2f, Rows);
        gridTopBorder.size = new Vector2(Cols, 1);

        gridBottomBorder.transform.localPosition = new Vector3((Cols - 1) / 2f, -1);
        gridBottomBorder.size = new Vector2(Cols, 1);

        gridLeftBorder.transform.localPosition = new Vector3(-1, (Rows - 1) / 2f);
        gridLeftBorder.size = new Vector2(Rows, 1);

        gridRightBorder.transform.localPosition = new Vector3(Cols, (Rows - 1) / 2f);
        gridRightBorder.size = new Vector2(Rows, 1);
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

                UpdateCompletedRow(x);
                CheckAnyRowMatchLeft();
            }
        }
    }

    private void UpdateCompletedRow(int rowIndex) {
        completedRowIndexes.Add(rowIndex);

        for (int y = 0; y < Cols; y++) {
            Cells[y, rowIndex].Item.ChangeItemType(ItemType.CompletedCube);
        }
    }

    private void CheckAnyRowMatchLeft() {
        List<List<int>> uncompletedRowIntervals = FindUncompletedRowIntervals();
        cannotBeCompletedRowIndexes = FindCannotBeCompletedRowIndexes(uncompletedRowIntervals);

        int completedRowCount = completedRowIndexes.Count;
        int cannotBeCompletedRowCount = cannotBeCompletedRowIndexes.Count;

        if (completedRowCount + cannotBeCompletedRowCount == Rows + 2) {
            OnPossibleRowMatchCountReachedZero?.Invoke(this, EventArgs.Empty);
        }
    }

    private List<List<int>> FindUncompletedRowIntervals() {
        List<List<int>> uncompletedRowIntervals = new List<List<int>>();
        List<int> uncompletedRowInterval = new List<int>();

        for (int x = -1; x < Rows + 1; x++) {
            if (completedRowIndexes.Contains(x)) {
                uncompletedRowInterval.Add(x);

                if (uncompletedRowInterval.Count == 2) {
                    uncompletedRowIntervals.Add(uncompletedRowInterval);

                    int lastBorder = uncompletedRowInterval.Last();
                    uncompletedRowInterval = new List<int>();
                    uncompletedRowInterval.Add(lastBorder);
                }
            }
        }

        return uncompletedRowIntervals;
    }

    private HashSet<int> FindCannotBeCompletedRowIndexes(List<List<int>> uncompletedRowIntervals) {
        for (int i = 0; i < uncompletedRowIntervals.Count; i++) {
            int firstBorder = uncompletedRowIntervals[i][0];
            int secondBorder = uncompletedRowIntervals[i][1];

            //Completed row'larýn arasýnda bir row kalmýþ.
            if (Mathf.Abs(firstBorder - secondBorder) == 2) {
                cannotBeCompletedRowIndexes.Add(firstBorder + 1);
            }

            //Completed row'larýn arasýnda birden fazla row kalmýþ.
            else if (secondBorder - firstBorder > 2) {
                bool isThereEnoughCubeToRowMatchInInterval = IsThereEnoughCubeToRowMatchInInterval(firstBorder, secondBorder);
                if (!isThereEnoughCubeToRowMatchInInterval) {
                    for (int x = firstBorder + 1; x < secondBorder; x++) {
                        cannotBeCompletedRowIndexes.Add(x);
                    }
                }
            }
        }

        return cannotBeCompletedRowIndexes;
    }

    private bool IsThereEnoughCubeToRowMatchInInterval(int firstBorder, int secondBorder) {
        int blueCubeCount = 0;
        int yellowCubeCount = 0;
        int greenCubeCount = 0;
        int redCubeCount = 0;

        for (int x = firstBorder + 1; x < secondBorder; x++) {
            for (int y = 0; y < Cols; y++) {
                switch (Cells[y, x].Item.ItemType) {
                    case ItemType.BlueCube:
                        blueCubeCount++;
                        break;
                    case ItemType.RedCube:
                        redCubeCount++;
                        break;
                    case ItemType.YellowCube:
                        yellowCubeCount++;
                        break;
                    case ItemType.GreenCube:
                        greenCubeCount++;
                        break;
                }
            }
        }

        return (greenCubeCount >= Cols || blueCubeCount >= Cols || redCubeCount >= Cols || yellowCubeCount >= Cols);
    }

    public static void CellsFadeOut() {
        for (int y = 0; y < Rows; y++) {
            for (int x = 0; x < Cols; x++) {
                Cell cell = Cells[x, y];
                cell.StartCoroutine(cell.FadeOutAnimation());
            }
        }
    }
}
