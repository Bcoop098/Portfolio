using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinDropTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<CoinDrop>().SpawnCoin(gameObject);
    }
}
