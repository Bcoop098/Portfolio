using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLevelData : MonoBehaviour
{
    public static EnemyLevelData enemyLevelDataInstance;

    public LevelDataObject levelData;

    // Start is called before the first frame update
    void Awake()
    {
        Toolbox.Instance.GetObject<PixelLord>("PixelLord").SetData(levelData);
        Toolbox.Instance.GetObject<AudioManager>("AudioManager").SetAudioSource(GameObject.FindObjectOfType<AudioSource>());
        Toolbox.Instance.GetObject<AudioManager>("AudioManager").SetAudioClip("levelTunes");
        Toolbox.Instance.GetObject<AudioManager>("AudioManager").PlayAudio();
    }
}
