using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController controllerInstance;

    // Transforms to act as start and end markers for the journey.
    public Transform startMarker;
    public Transform endMarker;

    // Movement speed in units per second.
    public float speed = 1.0F;

    // Time when the movement started.
    private float startTime;

    // Total distance between the markers.
    private float journeyLength;

    bool canMove = false;

    int currentZone = 0;


    void Start()
    {
        controllerInstance = this;

        // Keep a note of the time the movement started.
        startTime = Time.deltaTime;

        // Calculate the journey length.
        journeyLength = Vector3.Distance(startMarker.position, endMarker.position);

}

    // Move to the target end position.
    void Update()
    {


        //CameraMoving();

        if(canMove)
        {
            // Distance moved equals elapsed time times speed..
            float distCovered = (Time.time - startTime) * speed;

            // Fraction of journey completed equals current distance divided by total distance.
            float fractionOfJourney = distCovered / journeyLength;

            // Set our position as a fraction of the distance between the markers.
            transform.position = Vector3.Lerp(startMarker.position, endMarker.position, fractionOfJourney);
        }
     

    }

    public void NewPositionCamera(int zone)
    {
        if (currentZone != zone)
        {
            canMove = true;
            startMarker = GameObject.Find("CameraPt" + currentZone).transform;
            switch (zone)
            {
                case 0:
                    endMarker = GameObject.Find("CameraPt" + 0).transform;
                    currentZone = 0;
                    break;

                case 1:
                    endMarker = GameObject.Find("CameraPt" +1).transform;
                    currentZone = 1;
                    break;

                case 2:
                    endMarker = GameObject.Find("CameraPt" + 2).transform;
                    currentZone = 2;
                    break;

                case 3:
                    endMarker = GameObject.Find("CameraPt" + 3).transform;
                    currentZone = 3;
                    break;
            }
        
            journeyLength = Vector3.Distance(startMarker.position, endMarker.position);
        }

         
    }
}
