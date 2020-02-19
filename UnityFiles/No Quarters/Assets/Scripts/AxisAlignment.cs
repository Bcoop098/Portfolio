using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxisAlignment : MonoBehaviour
{
    GameObject closestPlayer;
    // Start is called before the first frame update
    void Start()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        float distance = 1000000;
        for(int i = 0; i < players.Length; i ++)
        {
            float playerdist = (this.gameObject.transform.position - players[i].gameObject.transform.position).magnitude;
            if (distance > playerdist)
            {
                distance = playerdist;
                closestPlayer = players[i];
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        float distance = 1000000;
        for (int i = 0; i < players.Length; i++)
        {
            float playerdist = (this.gameObject.transform.position - players[i].gameObject.transform.position).magnitude;
            if (distance > playerdist)
            {
                distance = playerdist;
                closestPlayer = players[i];
            }
        }
        Vector3 range = closestPlayer.transform.position - this.transform.position;
        range.x = 0.0f;
        range.z = 0.0f;
        this.gameObject.transform.Translate(range * Time.deltaTime);
    }
}
