using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseSceneScript : MonoBehaviour
{
   public void GoToMainMenu()
   {
        Toolbox.Instance.GetObject<SceneManagement>("SceneManagement").LoadByName("MainMenu");
   }

   public void Retry()
   {
        Toolbox.Instance.GetObject<SceneManagement>("SceneManagement").RetryLevel();
   }
}
