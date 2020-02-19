using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeterMaid : MonoBehaviour
{
    [SerializeField]
    private int maidDamage = 5;

    [SerializeField]
    private float maidSpeed = 0.05f;

    [SerializeField]
    private Vector3 knockBack = new Vector3(-3,0,0);

    //bool attacking = false;

    GameObject targetPlayer;

    void FixedUpdate()
    {
        transform.position = new Vector3(transform.position.x - maidSpeed, transform.position.y, transform.position.z);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            targetPlayer = collision.gameObject;
            targetPlayer.GetComponentInParent<PlayerControllerV2>().TakeDamage(maidDamage);

            targetPlayer.transform.position += knockBack;

            if (targetPlayer.transform.position.x < Toolbox.Instance.GetObject<PixelLord>("PixelLord").GetCurrentPlayerQuadrant().x)
            {
                targetPlayer.transform.position += knockBack;
                Toolbox.Instance.GetObject<PixelLord>("PixelLord").UpdatePlayerQuadrant(Toolbox.Instance.GetObject<PixelLord>("PixelLord").GetPlayerQuadrant()-1);
                CameraController.controllerInstance.NewPositionCamera(Toolbox.Instance.GetObject<PixelLord>("PixelLord").GetPlayerQuadrant());
            }
                      
        }

        if(collision.gameObject.transform.name == "PossibleLossTrigger")
        {
            ParkingMeter parkingMeter = GameObject.Find("Meter").GetComponent<ParkingMeter>();
            CoinManager coinManager = GameObject.Find("HUD").GetComponent<CoinManager>();
            MeterManager meterManage = GameObject.Find("MeterManager").GetComponent<MeterManager>();

            if (coinManager.GetCoins() < parkingMeter.GetValRem())
            {
                Toolbox.Instance.GetObject<AnalysisManager>("AnalysisManager").Death(Toolbox.Instance.GetObject<SceneManagement>("SceneManagement").GetCurrentLevelString());
                Debug.Log("LOST, restarting level");
                Toolbox.Instance.GetObject<SceneManagement>("SceneManagement").RetryLevel();
            }
            else
            {
                if (gameObject != null)
                {
                    parkingMeter.GetManager().RemoveCoins(parkingMeter.GetValRem());
                    parkingMeter.GetManager().IncreaseTime();
                    Destroy(gameObject);
                    meterManage.SetIsActive(false);
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            targetPlayer = null;
        }
    }

}
