using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ProgressData
{
    bool[] activeteLevels;
    public int secretShopCoins = 0;

    public void AddSecretShopCoins(int amount)
    {
        secretShopCoins += amount;
    }

    public void RemoveSecretShopCoins(int amount)
    {
        secretShopCoins -= amount;
    }
}
