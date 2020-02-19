using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTriggerScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Toolbox.Instance.GetObject<PlayerData>("PlayerData").updateData();
            Toolbox.Instance.GetObject<PixelLord>("PixelLord").SetGameStatus(false);

            //Complete this level
            Toolbox.Instance.GetObject<AnalysisManager>("AnalysisManager").CompleteLevel(Toolbox.Instance.GetObject<SceneManagement>("SceneManagement").GetCurrentLevelString());

            //Enable the next level
            Toolbox.Instance.GetObject<ProgressManager>("ProgressManager").ActivateLevel(Toolbox.Instance.GetObject<SceneManagement>("SceneManagement").GetCurrentLevel() + 1);

            //Load the next level
            Toolbox.Instance.GetObject<SceneManagement>("SceneManagement").LoadByIndex(Toolbox.Instance.GetObject<SceneManagement>("SceneManagement").GetCurrentLevel() + 1);
        }
    }

}
