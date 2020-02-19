using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    [SerializeField]
    int playerHealth = 100;
    int player2Health = 100;

    [SerializeField]
    int weaponDamage = 25;
    int weapon2Damage = 25;

    //This is used for the frame by which the player cannot hit hit the enemy, and player can only swing within said frame.
    [SerializeField]
    float globalInvincibilityFrame = 1.0f;

    [SerializeField]
    float moveSpd = 2f;
    [SerializeField]
    float move2Spd = 2f;

    float numberOfPlayers = 1;

    //Getters

    public float getInitTimer() { return globalInvincibilityFrame; }

    public float getNumberOfPlayers() { return numberOfPlayers; }


    //Player Data
    public int getPlayerHealth(int playerNum)
    {
        switch(playerNum)
        {
            case 0:
                return playerHealth;

            case 1:
                return player2Health;
        }
        return playerHealth;
    }
    public int getWeaponDamage(int playerNum)
    {
        switch (playerNum)
        {
            case 0:
                return weaponDamage;

            case 1:
                return weapon2Damage;
        }
        return weaponDamage;
    }

    public float getMoveSpd(int playerNum)
    {
        switch (playerNum)
        {
            case 0:
                return moveSpd;

            case 1:
                return move2Spd;
        }
        return moveSpd;
    }


    //Setters

    public void setOfPlayersChosen(int num)
    {
        numberOfPlayers = num;
    }

    public void setPlayerHealth(int newHealth, int playerNum)
    {
        switch(playerNum)
        {
            case 0:
                playerHealth = newHealth;
                break;

            case 1:
                player2Health = newHealth;
                break;
        }
    }
    public void setWeaponDamage(int newDamage, int playerNum)
    {
        switch (playerNum)
        {
            case 0:
               weaponDamage = newDamage;
                break;

            case 1:
                weapon2Damage = newDamage;
                break;
        }
    }

    public void setMoveSpd(float newSpd, int playerNum)
    {
        switch (playerNum)
        {
            case 0:
                moveSpd = newSpd;
                break;

            case 1:
                move2Spd = newSpd;
                break;
        }
    }

    //Updates
    public void updateData()
    {
        //If we have two players
        if (GameObject.Find("PlayerUnitP1"))
        {      
            //Toolbox.Instance.GetObject<PlayerData>("PlayerData").setPlayerHealth(GameObject.Find("PlayerUnitP1").GetComponent<PlayerControllerV2>().getCurrentHealth(), 0);
            Toolbox.Instance.GetObject<PlayerData>("PlayerData").setWeaponDamage(GameObject.Find("PlayerUnitP1").GetComponent<PlayerControllerV2>().getCurrentWeaponDmg(), 0);
            Toolbox.Instance.GetObject<PlayerData>("PlayerData").setMoveSpd(GameObject.Find("PlayerUnitP1").GetComponent<PlayerControllerV2>().getCurrentSpeed(), 0);
            
        }
        if(GameObject.Find("PlayerUnitP2"))
        {
            //Toolbox.Instance.GetObject<PlayerData>("PlayerData").setPlayerHealth(GameObject.Find("PlayerUnitP2").GetComponent<PlayerControllerV2>().getCurrentHealth(), 1);
            Toolbox.Instance.GetObject<PlayerData>("PlayerData").setWeaponDamage(GameObject.Find("PlayerUnitP2").GetComponent<PlayerControllerV2>().getCurrentWeaponDmg(), 1);
            Toolbox.Instance.GetObject<PlayerData>("PlayerData").setMoveSpd(GameObject.Find("PlayerUnitP2").GetComponent<PlayerControllerV2>().getCurrentSpeed(), 1);
        }
    }

    //Used to update specific stuff
    public void updateHealth(int newHealth, int playerNum)
    {
        //If we have two players
        if (GameObject.Find("PlayerUnitP1"))
        {
            Toolbox.Instance.GetObject<PlayerData>("PlayerData").setPlayerHealth(newHealth, playerNum);        
        }
        if (GameObject.Find("PlayerUnitP2"))
        {
            Toolbox.Instance.GetObject<PlayerData>("PlayerData").setPlayerHealth(newHealth, playerNum);
        }
    }


    //Used to update specific stuff
    public void updateWeaponDamage(int newWeaponDMg, int playerNum)
    {
        //If we have two players
        if (GameObject.Find("PlayerUnitP1"))
        {
            Toolbox.Instance.GetObject<PlayerData>("PlayerData").setWeaponDamage(newWeaponDMg, playerNum);
        }
        if (GameObject.Find("PlayerUnitP2"))
        {
            Toolbox.Instance.GetObject<PlayerData>("PlayerData").setWeaponDamage(newWeaponDMg, playerNum);
        }
    }


    //Used to update specific stuff
    public void updateMoveSpeed(int newMoveSpeed, int playerNum)
    {
        //If we have two players
        if (GameObject.Find("PlayerUnitP1"))
        {
            Toolbox.Instance.GetObject<PlayerData>("PlayerData").setMoveSpd(newMoveSpeed, playerNum);
        }
        if (GameObject.Find("PlayerUnitP2"))
        {
            Toolbox.Instance.GetObject<PlayerData>("PlayerData").setMoveSpd(newMoveSpeed, playerNum);
        }
    }
}
