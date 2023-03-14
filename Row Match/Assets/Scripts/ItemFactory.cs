using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemFactory : MonoBehaviour {

    public static ItemFactory Instance { get; private set; }

    private static readonly ItemType[] DefaultCubeArray = new[] {
            ItemType.GreenCube,
            ItemType.YellowCube,
            ItemType.BlueCube,
            ItemType.RedCube
        };

    private void Awake() {
        Instance = this;
    }

    public CubeItem CreateCubeItem(Cell cell, ItemType itemType) {
        var cubeItem = cell.gameObject.AddComponent<CubeItem>();
        cubeItem.PrepareCubeItem(itemType);

        return cubeItem;
    }

    public static ItemType GetRandomCubeItemType() {
        return GetRandomItemTypeFromArray(DefaultCubeArray);
    }

    private static ItemType GetRandomItemTypeFromArray(ItemType[] itemTypeArray) {
        return itemTypeArray[Random.Range(0, itemTypeArray.Length)];
    }
}
