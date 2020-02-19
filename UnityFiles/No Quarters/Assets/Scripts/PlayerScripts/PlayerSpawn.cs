using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        for(int i = 1; i < Toolbox.Instance.GetObject<PlayerData>("PlayerData").getNumberOfPlayers() + 1; i++)
        {
            GameObject Player = Resources.Load("PlayerUnitP" + i.ToString()) as GameObject;
            Instantiate(Player);
        }
    }
}
