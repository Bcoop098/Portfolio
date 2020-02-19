using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseMeterMinion : MonoBehaviour
{
    public int health = 50;
    public int respawnHealth = 50;

    [SerializeField]
    GameObject respawner;

    [SerializeField]
    int enemyCoinAmt = 0;

    bool alive = true;
    bool spawned = false;

    bool canBeDamaged = true;

    public int iD = 0;
    public int quadrant = 0;

    [SerializeField]
    Vector3 respawnPos;

    [SerializeField]
    int typeInt = 0;//This will determine if its type is 0(commoner), 1(criminal, 2(guard), 3(merchant)

    float invincibilityFrames = 1f;

    private void Start()
    {
        invincibilityFrames = Toolbox.Instance.GetObject<PlayerData>("PlayerData").getInitTimer();
        respawner = GameObject.FindGameObjectWithTag("Respawner");
    }
    public void SetDamagedStatus(bool canHit)
    {
        canBeDamaged = canHit;
    }

    public void TakeDamage(int incomingDamage)
    {
        if (health <= 0)
        {
            canBeDamaged = false;
            gameObject.SetActive(false);
            respawner.GetComponent<MinionRespawner>().KillMinion(gameObject);
            alive = false;
            ResetStats();
        }

        if (canBeDamaged && gameObject.activeInHierarchy)
        {
            SetDamagedStatus(false);
            health -= incomingDamage;
            StartCoroutine(InvincibilityFrame());
        }

        
    }

    IEnumerator InvincibilityFrame()
    {
        yield return new WaitForSeconds(invincibilityFrames);
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

    private void ResetStats()
    {
        health = respawnHealth;
        SetDamagedStatus(true);
        alive = true;
    }

    public void Respawn()
    {
        gameObject.SetActive(true);
        Vector2 newPos = respawnPos;
        transform.position = newPos;
    }
}
