using UnityEngine.SceneManagement;

public static class SceneController
{
   public static void LoadScene(SceneEnums.Scene scene)
   {
      SceneManager.LoadScene((int)scene);
   }
}
