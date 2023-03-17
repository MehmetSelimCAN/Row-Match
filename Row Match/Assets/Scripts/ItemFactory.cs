public static class ItemFactory {
    public static Item CreateItem(Cell cell, ItemType itemType) {
        var item = cell.gameObject.AddComponent<Item>();
        item.PrepareItem(itemType);

        return item;
    }
}
