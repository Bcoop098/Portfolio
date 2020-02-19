using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeterManager : MonoBehaviour
{
    [SerializeField]
    GameObject meterMaid;
    CoinManager coinMan;

    private GameObject currentMaid;

    bool isActive = false;
    // Start is called before the first frame update
    void Start()
    {
        coinMan = GameObject.FindGameObjectWithTag("CoinManager").GetComponent<CoinManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (coinMan.getTime() == 0)
        {
            if (!isActive)
            {
                currentMaid = Instantiate(meterMaid, new Vector3(70,0,0), Quaternion.identity);
                isActive = true;
            }
        }
        else
        {
            if(currentMaid != null)
            {
                Destroy(currentMaid);
                isActive = false;
            }         
        }

    }

    public bool GetIsActive() { return isActive; }

    public void SetIsActive(bool state) { isActive = state; }
}
