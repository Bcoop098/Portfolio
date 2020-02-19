using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineShoot : MonoBehaviour
{
    [SerializeField]
    private float machineHealth = 75f;

    bool canBeDamaged = true;

    [SerializeField]
    GameObject Cleaner;

    [SerializeField]
    private GameObject water;

    [SerializeField]
    private GameObject fire;

    [SerializeField]
    float waterTimer = 5f;
    [SerializeField]
    float fireTimer = 6.5f;

    [SerializeField]
    bool isWasher = true;

    [SerializeField]
    bool isDryer = false;

    float invincibilityFrame = 1.0f;

    void Start()
    {
        if (isWasher)
        {
            water.GetComponent<WaterScript>().disableWater();
            InvokeRepeating("FireWater", waterTimer, waterTimer);
        }
        else 
        {
            fire.GetComponent<FireScript>().disableFire();
            InvokeRepeating("FireFire",fireTimer,fireTimer);
        }
        Cleaner = GameObject.FindGameObjectWithTag("Boss2");
        invincibilityFrame = Toolbox.Instance.GetObject<PlayerData>("PlayerData").getInitTimer();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (machineHealth <= 0.0f)
        {
            Cleaner.GetComponent<CleanerScript>().KillMachine(gameObject);
            //CleanerScript.cleaner.KillMachine(gameObject);
            gameObject.SetActive(false);
        }
    }

    void FireWater()
    {
        water.GetComponent<WaterScript>().enableWater();
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
