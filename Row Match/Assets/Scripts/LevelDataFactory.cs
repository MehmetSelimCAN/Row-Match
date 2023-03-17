public static class LevelDataFactory {
    public static LevelData CreateLevelData(LevelName levelName) {
        LevelData levelData = new LevelData(levelName);
        levelData.Initialize();
        return levelData;
    }
}
