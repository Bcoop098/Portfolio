using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParkingMeter : MonoBehaviour
{
    [SerializeField]
    float valRem = 0.10f;
    CoinManager cm;

    bool canDeposit = false;
    int IDval = 0;
    [SerializeField]
    float depositTime = 0.5f;

    private void Awake()
    {
        cm = GameObject.FindGameObjectWithTag("CoinManager").GetComponent<CoinManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Enter");
        if (collision.gameObject.tag == "Player")
        {
            IDval = collision.GetComponentInParent<PlayerControllerV2>().controllerID;
            canDeposit = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("Exit");
        if (collision.gameObject.tag == "Player")
        {
            IDval = 0;
            canDeposit = false;
        }
    }

    private void Update()
    {
        if (canDeposit && Input.GetButton("XBut" + IDval) && Round(cm.GetCoins(), 2) >= valRem)
        {
            StartCoroutine(Deposit(depositTime));
            cm.RemoveCoins(valRem);
            cm.IncreaseTime();
        }
    }

    private IEnumerator Deposit(float timing)
    {
        canDeposit = false;
        yield return new WaitForSeconds(timing);
        canDeposit = true;
    }

    private float Round(float value, int decimalPoints)
    {
        float mult = Mathf.Pow(10f, (float)decimalPoints);
        return Mathf.Round(value * mult) / mult;
    }

    public float GetValRem() { return valRem; }

    public CoinManager GetManager() { return cm; }
}
