using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectZoneChanges : MonoBehaviour
{
    public int zoneID = 0;
    [SerializeField] bool canMove = false;
    [SerializeField] int playersReady = 0;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            playersReady++;
            if(playersReady == Toolbox.Instance.GetObject<PlayerData>("PlayerData").getNumberOfPlayers())
            {
                Toolbox.Instance.GetObject<PixelLord>("PixelLord").UpdatePlayerQuadrant(zoneID);
                CameraController.controllerInstance.NewPositionCamera(zoneID);
                playersReady = 0;
            }
        }
        if(collision.gameObject.CompareTag("Enemy"))
        {
            Toolbox.Instance.GetObject<PixelLord>("PixelLord").EnemyKilled(collision.GetComponent<BaseHuman>().GetID(), collision.GetComponent<BaseHuman>().GetQuadrant());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //playersReady--;
        }
    }
}
