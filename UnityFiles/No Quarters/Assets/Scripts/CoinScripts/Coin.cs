using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField]
    float val = 0.05f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Toolbox.Instance.GetObject<AnalysisManager>("AnalysisManager").CollectACoin(val);
            GameObject.FindGameObjectWithTag("CoinManager").GetComponent<CoinManager>().AddCoins(val);
            Destroy(gameObject);
        }
    }
}
