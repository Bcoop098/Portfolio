using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinManager : MonoBehaviour
{
    [SerializeField]
    Text Timer;
    [SerializeField]
    Text CoinPurse;

    GameObject parkingMeter;

    [SerializeField]
    int timeBump = 10;

    float coins = 0f;
    int timer = 15;

    // Start is called before the first frame update
    void Start()
    {
        parkingMeter = GameObject.FindGameObjectWithTag("ParkingMeter");
        InvokeRepeating("TimerShift", 1f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        CoinPurse.text = coins.ToString("F2");

        if (timer <= 10)
        {
            Timer.color = Color.red;
        }
        else
        {
            Timer.color = Color.white;
        }

        if (timer % 60 < 10)
        {
            Timer.text = (timer / 60) + ":0" + (timer % 60);
        }
        else
        {
            Timer.text = (timer / 60) + ":" + (timer % 60);
        }
    }

    public float GetCoins()
    {
        return coins;
    }

    public void AddCoins(float val)
    {
        coins += val;
    }

    public void RemoveCoins(float val)
    {
        coins -= val;
    }

    void TimerShift()
    {
        --timer;
        if (timer < 0)
        {
            timer = 0;
        }
    }

    public void IncreaseTime()
    {
        timer += timeBump;
    }

    public int getTime()
    {
        return timer;
    }
}
