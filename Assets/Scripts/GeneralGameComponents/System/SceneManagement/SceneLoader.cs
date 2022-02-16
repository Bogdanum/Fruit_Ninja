using UnityEngine.SceneManagement;

public static class SceneLoader
{
   public static void LoadScene(SceneEnums.Scene scene)
   {
      SceneManager.LoadScene((int) scene);
   }

   public static void LoadSceneAsync(SceneEnums.Scene scene)
   {
      SceneManager.LoadSceneAsync((int) scene);
   }
}
