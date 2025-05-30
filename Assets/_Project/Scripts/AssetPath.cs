namespace _Project.Scripts
{
    public static class AssetPath
    {
        public const string UIRootPath = "UI/UI-Root";
        public const string CurtainPath = "UI/Curtain";
        
        public const string TowerPath = "GameEntities/Tower";

#if UNITY_EDITOR
        public const string CheatManager = "Infrastructure/CheatManager";
#endif
    }
}
