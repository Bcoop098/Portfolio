using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.IO;

public class ProgressManager : MonoBehaviour
{
    //includes all scenes in the build
    public bool[] activateLevels = new bool[12];

    int secretShopCoins = 0;

    bool newGame = true;

    public void AddSecretShopCoins(int amount)
    {
        Toolbox.Instance.GetObject<AnalysisManager>("AnalysisManager").SecretCoinGet(amount);
        secretShopCoins += amount;
    }

    public void RemoveSecretShopCoins(int amount)
    {
        secretShopCoins -= amount;
    }

    public void ActivateLevel(int index)
    {
        activateLevels[index] = true;
    }

    public void DeActivateLevel(int index)
    {
        activateLevels[index] = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        activateLevels[0] = true;
        activateLevels[1] = true;
        activateLevels[2] = true;

        for (int i = 3; i < activateLevels.Length;i++)
        {
            activateLevels[i] = false;
        }
        activateLevels[11] = true;
    }

    public bool GetNewGameStatus() { return newGame; }

    public void SetNewGameStatus(bool status) { newGame = status; }

    public int GetSecretCoinTotal()
    {
        return secretShopCoins;
    }
}
