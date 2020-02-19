using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMouseMovement : MonoBehaviour
{
    public GameObject nexus;

    private GameObject portal = null;

    private bool isMoving = false;

    private PlayerManager playerManager;

    private void Awake()
    {
        playerManager = GameObject.Find("PlayerManager").GetComponent<PlayerManager>();
    }

    private void OnMouseDrag()
    {
        if (!gameObject.GetComponent<PlayerScript>().IsDead() && !playerManager.IsUsingBoard())
        {
            isMoving = true;
            Vector3 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            newPosition.z = transform.position.z;
            transform.position = newPosition;
            gameObject.GetComponent<PlayerScript>().SetIsOnNexus(false);
        }
    }

    private void OnMouseUp()
    {
        Vector3 newPosition;

        isMoving = false;

        if (!playerManager.IsUsingBoard())
        {
            if (portal != null)
            {
                newPosition = portal.transform.position;
                newPosition.z = transform.position.z;
                transform.position = newPosition;
                portal = null;
                AudioManager.instance.PlayDeployed(gameObject.name);
                gameObject.GetComponent<PlayerScript>().SetIsOnNexus(false);
            }
            else
            {
                newPosition = nexus.transform.position;
                newPosition.z = transform.position.z;
                transform.position = newPosition;
                gameObject.GetComponent<PlayerScript>().SetIsOnNexus(true);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Portal" && !playerManager.IsUsingBoard())
        {
            portal = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Portal" && !playerManager.IsUsingBoard())
        {
            portal = null;
        }
    }

    public bool IsMoving()
    {
        return isMoving;
    }
}