using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableScript : MonoBehaviour
{

    [SerializeField]
    private float tableHealth = 5f;

    [SerializeField]
    GameObject Barista;

    bool canBeDamaged = true;

    float invincibilityFrame = 0f;

    private void Start()
    {
        invincibilityFrame = Toolbox.Instance.GetObject<PlayerData>("PlayerData").getInitTimer();
        Barista = GameObject.FindGameObjectWithTag("Boss");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (tableHealth <= 0.0f)
        {
            Barista.GetComponent<BaristaScript>().KillTable(gameObject);
            Destroy(gameObject);
        }
    }

    public void TableDamage(int damage)
    {
        if (canBeDamaged)
        {
            SetDamagedStatus(false);
            tableHealth -= damage;
            StartCoroutine(InvincibilityFrame());
        }
        
    }

    void SetDamagedStatus(bool canHit)
    {
        canBeDamaged = canHit;
    }
    IEnumerator InvincibilityFrame()
    {
        yield return new WaitForSeconds(invincibilityFrame);
        SetDamagedStatus(true);
    }
}
