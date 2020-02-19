using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TentacleTracker : MonoBehaviour
{
    [SerializeField]
    private GameObject[] tentacleArray;
    [SerializeField]
    private List<GameObject> activeTentacles;

    [SerializeField]
    GameObject boss;

    private bool alreadyBuiltArray = false;

    private int respawnCount = 0;

    void Start()
    {
        

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (boss.GetComponent<MeterMaidBoss>().checkEnrage4() && alreadyBuiltArray == false)
        {
            alreadyBuiltArray = true;
            tentacleArray = GameObject.FindGameObjectsWithTag("Tentacle");
            InitTentacles();
        }

    }

    void InitTentacles()
    {
        activeTentacles = new List<GameObject>();
        for (int i = 0; i < tentacleArray.Length; i++)
        {
            activeTentacles.Add(tentacleArray[i]);
        }
    }

    public void KillTentacle(GameObject tentacle)
    {
        for (int i = 0; i < activeTentacles.Count; i++)
        {
            if (activeTentacles[i] == tentacle)
            {
                activeTentacles.RemoveAt(i);
                break;
            }
        }
        if (activeTentacles.Count == 0)
        {
            boss.GetComponent<MeterMaidBoss>().makePissed();
        }
    }

 
}
