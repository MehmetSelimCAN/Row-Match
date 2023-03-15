using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemFactory : MonoBehaviour {

    public static ItemFactory Instance { get; private set; }

    private static readonly ItemType[] DefaultItemArray = new[] {
            ItemType.GreenCube,
            ItemType.YellowCube,
            ItemType.BlueCube,
            ItemType.RedCube
        };

    private void Awake() {
        Instance = this;
    }

    public Item CreateItem(Cell cell, ItemType itemType) {
        var item = cell.gameObject.AddComponent<Item>();
        item.PrepareItem(itemType);

        return item;
    }

    public static ItemType GetRandomItemType() {
        return GetRandomItemTypeFromArray(DefaultItemArray);
    }

    private static ItemType GetRandomItemTypeFromArray(ItemType[] itemTypeArray) {
        return itemTypeArray[Random.Range(0, itemTypeArray.Length)];
    }
}
