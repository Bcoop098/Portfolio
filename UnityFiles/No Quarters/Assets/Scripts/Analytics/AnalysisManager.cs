using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameAnalyticsSDK;

public class AnalysisManager : MonoBehaviour
{
    //Place in tool box

    /*  
        Levels
        - Coins collected in a level
        - Time spent in a level
        - Enemies killed in a level
        - Start, complete, or fail for each level
        - Start, complete, or fail for each boss

        Inbetween
        - Secret Shop left
        - Secret Shop purchased (item)
        - Is the game being played as a co-op or single player experience

        Moar data???
     */

    //Data for storage
    float coinsCollected = 0f;
    float timeInLevel = 0f;
    int enemiesKilled = 0;

    private void Start()
    {
        MasterStart();
    }

    private void Awake()
    {
        Debug.developerConsoleVisible = true;
    }

    void MasterStart()
    {
        GameAnalytics.Initialize();
        //GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, "Menu");
    }

    //void User() {}

    //Levels
    public void StartLevel(string levelName)
    {
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, levelName);
    }

    public void CompleteLevel(string levelName)
    {
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, levelName, enemiesKilled);
        GameAnalytics.NewResourceEvent(GAResourceFlowType.Source, "Coins", (coinsCollected * 10f), "Reward", "DroppedCoins");
        coinsCollected = 0f;
        enemiesKilled = 0;
    }

    public void Death(string levelName)
    {
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Fail, levelName);
    }

    //The Shop
    public void EnterTheShop()
    {
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, "SecretShop");
    }

    public void ExitTheShop()
    {
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, "SecretShop");
    }

    //Secret Coins
    public void ItemGot(string itemName)
    {
        GameAnalytics.NewResourceEvent(GAResourceFlowType.Sink, "SecretCoins", 1, "Boon", itemName);
    }

    public void SecretCoinGet(int val)
    {
        GameAnalytics.NewResourceEvent(GAResourceFlowType.Source, "SecretCoins", val, "Reward", "SecretCoin");
    }

    //In Level
    public void CollectACoin(float amount)
    {
        coinsCollected = amount;
    }

    public void KillAnEnemy()
    {
        ++enemiesKilled;
    }
}
