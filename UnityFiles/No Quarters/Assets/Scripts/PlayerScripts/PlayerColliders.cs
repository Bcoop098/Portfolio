using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerColliders : MonoBehaviour
{
    public static PlayerColliders playerColInstance;
    // Start is called before the first frame update
    void Start()
    {
        playerColInstance = this;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        /*if (!collision.gameObject.CompareTag("ZoneChange"))
        {

            if (this.gameObject.name == "UpCollision")
            {

                PlayerManager.playerManagerInstance.SetCollisionStatus(0, true);
            }
            else if (this.gameObject.name == "DownCollision")
            {

                PlayerManager.playerManagerInstance.SetCollisionStatus(1, true);
            }
            else if (this.gameObject.name == "RightCollision")
            {

                PlayerManager.playerManagerInstance.SetCollisionStatus(2, true);
            }
            else if (this.gameObject.name == "LeftCollision")
            {

                PlayerManager.playerManagerInstance.SetCollisionStatus(3, true);
            }
        }*/
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        /*if (!collision.gameObject.CompareTag("ZoneChange"))
        {


            if (this.gameObject.name == "UpCollision")
            {
                PlayerManager.playerManagerInstance.SetCollisionStatus(0,false);
            }
            else if (this.gameObject.name == "DownCollision")
            {
                PlayerManager.playerManagerInstance.SetCollisionStatus(1, false);
            }
            else if(this.gameObject.name == "RightCollision")
            {
                PlayerManager.playerManagerInstance.SetCollisionStatus(2, false);
            }
            else if(this.gameObject.name == "LeftCollision")
            {
                PlayerManager.playerManagerInstance.SetCollisionStatus(3, false);
            }
        }*/
    }
}
