using System;
using System.Collections.Generic;
using UnityEngine;

public class LevelData {
    public LevelData(LevelName levelName) {
        levelInformation = Resources.Load<TextAsset>("Levels/" + levelName).ToString();
        this.levelName = levelName;
    }

    public LevelName levelName;

    private string levelInformation;
    private string levelNumberStr;
    private string gridWidthStr;
    private string gridHeightStr;
    private string moveCountStr;
    private string gridDataStr;

    public ItemType[,] GridData { get; protected set; }
    protected List<ItemType> levelItemTypeList = new List<ItemType>();

    public void Initialize() {
        ReadLevelFile(levelInformation);
        GridData = new ItemType[Grid.Cols, Grid.Rows];

        int gridNumber = 0;
        for (var x = 0; x < Grid.Rows; x++) {
            for (var y = 0; y < Grid.Cols; y++) {
                GridData[y, x] = levelItemTypeList[gridNumber];
                gridNumber++;
            }
        }
    }

	protected void ReadLevelFile(string levelInformation) {
        string[] lines = levelInformation.Split('\n');
        string[] stringSeparator = new string[] { ": " };

        levelNumberStr = lines[0].Split(stringSeparator, StringSplitOptions.None).GetValue(1).ToString();
        gridWidthStr = lines[1].Split(stringSeparator, StringSplitOptions.None).GetValue(1).ToString();
        gridHeightStr = lines[2].Split(stringSeparator, StringSplitOptions.None).GetValue(1).ToString();
        moveCountStr = lines[3].Split(stringSeparator, StringSplitOptions.None).GetValue(1).ToString();
        gridDataStr = lines[4].Split(stringSeparator, StringSplitOptions.None).GetValue(1).ToString();

        AssignGridSize();
        AssignMoveCount();
        AssignGridData();
    }

    public void AssignGridSize() {
        int gridWidth = int.Parse(gridWidthStr);
        int gridHeight = int.Parse(gridHeightStr);

        Grid.Cols = gridWidth;
        Grid.Rows = gridHeight;
        Grid.Cells = new Cell[gridWidth, gridHeight];
        Grid.CellsBackground = new CellBackground[gridWidth, gridHeight];
    }

    public void AssignMoveCount() {
        int moveCount = int.Parse(moveCountStr);

        MoveCounter.RemainingMoveCount = moveCount;
    }

    private void AssignGridData() {
        int dataCount = gridDataStr.Split(',').Length;

        for (int i = 0; i < dataCount; i++) {
            switch (gridDataStr.Split(',').GetValue(i).ToString()) {
                case "b":
                    levelItemTypeList.Add(ItemType.BlueCube);
                    break;
                case "g":
                    levelItemTypeList.Add(ItemType.GreenCube);
                    break;
                case "r":
                    levelItemTypeList.Add(ItemType.RedCube);
                    break;
                case "y":
                    levelItemTypeList.Add(ItemType.YellowCube);
                    break;
            }
        }
    }

    public int GetMoveCount() {
        int moveCount = int.Parse(moveCountStr);

        return moveCount;
    }
}