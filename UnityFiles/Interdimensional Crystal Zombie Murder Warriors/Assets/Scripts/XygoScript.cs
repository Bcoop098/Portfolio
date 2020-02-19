using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XygoScript : MonoBehaviour
{
    private RingExpand ringExpand;


    private void Awake()
    {
        ringExpand = gameObject.GetComponentInChildren<RingExpand>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 nexusPosition = GameObject.Find("Nexus").GetComponent<Transform>().position;
        nexusPosition.z = transform.position.z;
        if (!gameObject.GetComponent<PlayerMouseMovement>().IsMoving() && transform.position != nexusPosition)
        {
            ringExpand.ChangeExpand(true);
        }
        else
        {
            ringExpand.ChangeExpand(false);
        }
    }
}
