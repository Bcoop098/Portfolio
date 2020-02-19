using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DryerScript : MonoBehaviour
{
    [SerializeField]
    private float machineHealth = 75f;

    bool canBeDamaged = true;

    [SerializeField]
    GameObject Cleaner;

    [SerializeField]
    private GameObject fire;

    [SerializeField]
    float fireTimer = 6.5f;


    float invincibilityFrame = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        fire.GetComponent<FireScript>().setFirePos(transform.position);
        fire.GetComponent<FireScript>().disableFire();
        InvokeRepeating("FireFire", fireTimer, fireTimer);
        Cleaner = GameObject.FindGameObjectWithTag("Boss2");
        invincibilityFrame = Toolbox.Instance.GetObject<PlayerData>("PlayerData").getInitTimer();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (machineHealth <= 0.0f)
        {
            Cleaner.GetComponent<CleanerScript>().KillMachine(gameObject);
            fire.GetComponent<FireScript>().disableFire();
            CancelInvoke();
            //CleanerScript.cleaner.KillMachine(gameObject);
            gameObject.SetActive(false);
        }
    }

    void FireFire()
    {
        fire.GetComponent<FireScript>().enableFire();
    }

    public void MachineDamage(int damage)
    {
        if (canBeDamaged)
        {
            SetDamagedStatus(false);
            machineHealth -= damage;
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
