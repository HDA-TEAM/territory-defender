namespace Common.Loading.Scripts
{
    public enum ESceneIdentified
    {
        LoadingScene = 0,
        Home = 1,
        GamePlay = 2,
        
    }
    public static class SceneIdentified
    {
        public static int GetSceneIndex(ESceneIdentified eSceneIdentified){
            switch (eSceneIdentified)
            {
                case ESceneIdentified.LoadingScene : return 0;
                case ESceneIdentified.Home : return 1;
                case ESceneIdentified.GamePlay : return 2;
                default: return 0;
            }
        }
        public static string GetSceneName(ESceneIdentified eSceneIdentified){
            switch (eSceneIdentified)
            {
                case ESceneIdentified.LoadingScene : return "LoadingScene";
                case ESceneIdentified.Home : return "HomeScene";
                case ESceneIdentified.GamePlay : return "GamePlay";
                default: return "0";
            }
        }
        public static string GetScenePath(ESceneIdentified eSceneIdentified){
            switch (eSceneIdentified)
            {
                case ESceneIdentified.LoadingScene : return "Assets/Scenes/LoadingScene.unity";
                case ESceneIdentified.Home : return "Assets/Scenes/HomeScene.unity";
                case ESceneIdentified.GamePlay : return "Assets/Scenes/GamePlay.unity";
                default: return "0";
            }
        }
    }
}
