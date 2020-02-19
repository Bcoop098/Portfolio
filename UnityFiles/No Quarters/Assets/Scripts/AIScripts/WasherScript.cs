using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WasherScript : MonoBehaviour
{
    [SerializeField]
    private float machineHealth = 75f;

    bool canBeDamaged = true;

    [SerializeField]
    GameObject Cleaner;

    [SerializeField]
    private GameObject water;

    [SerializeField]
    float waterTimer = 5f;


    float invincibilityFrame = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        water.GetComponent<WaterScript>().setWaterPos(transform.position);
        water.GetComponent<WaterScript>().disableWater();
        InvokeRepeating("FireWater", waterTimer, waterTimer);
        Cleaner = GameObject.FindGameObjectWithTag("Boss2");
        invincibilityFrame = Toolbox.Instance.GetObject<PlayerData>("PlayerData").getInitTimer();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (machineHealth <= 0.0f)
        {
            Cleaner.GetComponent<CleanerScript>().KillMachine(gameObject);
            water.GetComponent<WaterScript>().disableWater();
            CancelInvoke();
            //CleanerScript.cleaner.KillMachine(gameObject);
            gameObject.SetActive(false);
        }
    }

    void FireWater()
    {
        water.GetComponent<WaterScript>().enableWater();
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
