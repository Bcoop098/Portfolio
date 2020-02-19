using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardRespawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] guardArray;
    [SerializeField]
    private List<GameObject> activeGuards;

    [SerializeField]
    GameObject boss;

    private int respawnCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        guardArray = GameObject.FindGameObjectsWithTag("BankGuard");
        InitGuards();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void InitGuards()
    {
        activeGuards = new List<GameObject>();
        for (int i = 0; i < guardArray.Length; i++)
        {
            activeGuards.Add(guardArray[i]);
        }
    }

    public void KillGuard(GameObject guard)
    {
        for (int i = 0; i < activeGuards.Count; i++)
        {
            if (activeGuards[i] == guard)
            {
                Toolbox.Instance.GetObject<AnalysisManager>("AnalysisManager").KillAnEnemy();
                activeGuards.RemoveAt(i);
                break;
            }
        }
        if (activeGuards.Count == 0)
        {
            if (respawnCount < 3)
            {
                respawnCount++;
                StartCoroutine(ResetGuards());
            }
            else
                boss.GetComponent<BankerScript>().setEnrage();
        }
    }

    IEnumerator ResetGuards()
    {
        yield return new WaitForSeconds(1);
        for (int i = 0; i < guardArray.Length; i++)
        {
            guardArray[i].GetComponent<BaseBossGuard>().Respawn();
            guardArray[i].GetComponent<BossFightGuardScript>().attack = true;
            activeGuards.Add(guardArray[i]);
        }
    }
}
