using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionRespawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] minionArray;
    [SerializeField]
    private List<GameObject> activeMinions;

    [SerializeField]
    GameObject boss;

    private int respawnCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        minionArray = GameObject.FindGameObjectsWithTag("MeterMinion");
        InitMinions();

    }

    // Update is called once per frame
    void Update()
    {

    }

    void InitMinions()
    {
        activeMinions = new List<GameObject>();
        for (int i = 0; i < minionArray.Length; i++)
        {
            activeMinions.Add(minionArray[i]);
        }
    }

    public void KillMinion(GameObject minion)
    {
        for (int i = 0; i < activeMinions.Count; i++)
        {
            if (activeMinions[i] == minion)
            {
                Toolbox.Instance.GetObject<AnalysisManager>("AnalysisManager").KillAnEnemy();
                activeMinions.RemoveAt(i);
                break;
            }
        }
        if (activeMinions.Count == 0)
        {
            if (respawnCount < 3)
            {
                respawnCount++;
                StartCoroutine(ResetMinions());
            }
            else
                boss.GetComponent<MeterMaidBoss>().setEnrageMaid();
        }
    }

    IEnumerator ResetMinions()
    {
        yield return new WaitForSeconds(1);
        for (int i = 0; i < minionArray.Length; i++)
        {
            minionArray[i].GetComponent<BaseMeterMinion>().Respawn();
            minionArray[i].GetComponent<MeterMinionMove>().attack = true;
            activeMinions.Add(minionArray[i]);
        }
    }
}
