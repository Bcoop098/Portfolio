using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseHuman : MonoBehaviour
{
    public int health = 50;
    public int respawnHealth = 50;

    [SerializeField]
    int enemyCoinAmt = 0;

    bool alive = true;
    bool spawned = false;

    bool canBeDamaged = true;

    public int iD = 0;
    public int quadrant = 0;

    [SerializeField]
    int typeInt = 0;//This will determine if its type is 0(commoner), 1(criminal, 2(guard), 3(merchant)

    float invincibilityFrame = 1.0f;

    private void Update()
    {
        
    }
    public void SetDamagedStatus(bool canHit)
    {
        canBeDamaged = canHit;
    }

    public void TakeDamage(int incomingDamage)
    {
        if (canBeDamaged)
        {
            SetDamagedStatus(false);
            health -= incomingDamage;
            StartCoroutine(InvincibilityFrame());
        }

        if (health <= 0)
        {
            gameObject.GetComponent<CoinDrop>().SpawnCoin(this.gameObject);
            Toolbox.Instance.GetObject<PixelLord>("PixelLord").EnemyKilled(iD, quadrant);
            gameObject.SetActive(false);
            alive = false;
            ResetStats();
        }
    }

    IEnumerator InvincibilityFrame()
    {
        yield return new WaitForSeconds(invincibilityFrame);
        SetDamagedStatus(true);
    }

    public int GetTypeID()
    {
        return typeInt;
    }

    public int GetCoinAmt()
    {
        return enemyCoinAmt;
    }

    public int GetID()
    {
        return iD;
    }

    public int GetQuadrant()
    {
        return quadrant;
    }

    public void SetQuadrant(int quadID)
    {
        quadrant = quadID;
    }

    public void SetID(int newID)
    {
        iD = newID;
    }

    protected void ResetStats()
    {
        health = respawnHealth;
        SetDamagedStatus(true);
        alive = true;
    }

    private void Start()
    {
        invincibilityFrame = Toolbox.Instance.GetObject<PlayerData>("PlayerData").getInitTimer();
    }
}
