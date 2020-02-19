using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinDrop : MonoBehaviour
{
    [SerializeField]
    GameObject coin;

    [SerializeField]
    int coinsDroppedMin = 1;
    [SerializeField]
    int coinsDroppedMax = 3;

    public void SpawnCoin(GameObject dropper)
    {
        int coinsDropped = Random.Range(coinsDroppedMin, (coinsDroppedMax + 1));

        for (int i = 0;  i < coinsDropped; ++i)
        {
            Instantiate(coin, dropper.transform.position, dropper.transform.rotation);
        }
    }
}
