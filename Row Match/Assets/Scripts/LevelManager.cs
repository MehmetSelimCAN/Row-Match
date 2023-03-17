using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

    [SerializeField] private Grid Grid;

	public static LevelData levelData;

    private void Awake() {
        levelData.AssignGridSize();
        PrepareGrid();
        PrepareLevel();
    }

    private void PrepareGrid() {
        Grid.Prepare();
    }

    private void PrepareLevel() {
        for (var x = 0; x < levelData.GridData.GetLength(0); x++) {
			for (var y = 0; y < levelData.GridData.GetLength(1); y++) {
				var cell = Grid.Cells[x, y];

				var itemType = levelData.GridData[x, y];
				cell.InsertItem(itemType);
			}
		}
	}

}
