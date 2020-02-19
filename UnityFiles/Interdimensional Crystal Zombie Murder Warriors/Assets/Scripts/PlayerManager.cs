﻿﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using System;
using System.Linq;

public class PlayerManager : MonoBehaviour
{
    private readonly String portName = "COM6";

    private readonly int baudRate = 9600;

    [SerializeField]
    private List<GameObject> portals;

    [SerializeField]
    private List<GameObject> characters;

    [SerializeField]
    private GameObject Nexus;

    // There are 20 possible combinations of inputs.
    private int [] flags = new int[20];

    private List<int> currentFlags;

    private Dictionary<int, NodeFlag> nodeFlags;

    private bool usingBoard = false;

    // Create a serial port with the arduino port "COM#"
    private SerialPort serialPort;

    private void Awake()
    {
        serialPort = new SerialPort();

        String[] ports = SerialPort.GetPortNames();

        for (int currentPort = 0; currentPort < ports.Length; currentPort++)
        {
            if (ports[currentPort] == portName)
            {
                serialPort.PortName = portName;

                serialPort.BaudRate = baudRate;

                // Open the serial port.
                serialPort.Open();

                usingBoard = true;
                
                serialPort.ReadTimeout = 25;
                break;
            }
        }

        // Initialize the dictionary.
        nodeFlags = new Dictionary<int, NodeFlag>();

        // Create empty list of current flags.
        currentFlags = new List<int>();

        int portalCounter = 0;

        // Add all the portals and characters to the list and keep track of the flags.
        for (int counter = 0; counter < flags.Length; counter++)
        {
            // Calculate the unique flag id.
            int flag = (int)Mathf.Pow(2, counter);

            // Add the flag id for future reference.
            flags[counter] = flag;

            int characterIndex = counter % characters.Count;

            // Get the portals location. The same portal is used. Ex with 3 characters: portal1/char1, portal1/char2, portal1/char3, portal2/char1
            Vector3 location = portals[portalCounter].transform.position;

            // Get the character for the current portal. None if at '6th' character.
            string character = characters[characterIndex].name;//(characterIndex == 5 ? "None" : characters[counter % characters.Count].name);

            // Create the node info for the current flag id.
            NodeFlag newNode = new NodeFlag(flag, location, character);

            nodeFlags.Add(flag, newNode);

            if (characterIndex == 3)
            {
                portalCounter++;
            }
        }
    }

    private void Start()
    {
        for (int counter = 0; counter < flags.Length; counter++)
        {
            int key = flags[counter];
            double id = nodeFlags[key].Id;
            Vector3 location = nodeFlags[key].Location;
            string character = nodeFlags[key].Character;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (usingBoard)
        {
            CheckPort();
        }
    }

    private void PlaceCharacter()
    {
        List<GameObject> nexusCharacters = new List<GameObject>();
        List<GameObject> nodeCharacters = new List<GameObject>();

        // Place each character back on the nexus for a frame before moving them back.
        // Guarantees only characters placed on the board are the only ones in play.
        foreach (GameObject character in characters)
        {
            Vector3 newPosition = Nexus.transform.position;
            newPosition.z = character.transform.position.z;
            character.transform.position = newPosition;
            nexusCharacters.Add(character);
        }

        if (currentFlags.Count != 0)
        {
            for (int counter = 0; counter < currentFlags.Count; counter++)
            {
                NodeFlag nodeFlag = nodeFlags[currentFlags[counter]];

                foreach (GameObject character in characters)
                {
                    if (nodeFlag.Character == character.name)
                    {
						Vector3 newPosition = nodeFlag.Location;
						newPosition.z = character.transform.position.z;
                        character.transform.position = newPosition;
                        character.GetComponent<PlayerScript>().SetIsOnNexus(false);

                        foreach (GameObject nexusCharacter in nexusCharacters)
                        {
                            if (nexusCharacter == character)
                            {
                                nexusCharacters.Remove(nexusCharacter);
                                nodeCharacters.Add(character);
                                break;
                            }
                        }
                    }
                }
            }
        }

        if (nexusCharacters.Count != 0)
        {
            foreach (GameObject nexusCharacter in nexusCharacters)
            {
                nexusCharacter.GetComponent<PlayerScript>().SetIsOnNexus(true);
            }
        }

        if (nodeCharacters.Count != 0)
        {
            foreach (GameObject nodeCharacter in nodeCharacters)
            {
                nodeCharacter.GetComponent<PlayerScript>().SetIsOnNexus(false);
            }
        }
    }

    private void UnpackFlag(string _flags)
    {
        int flag = Convert.ToInt32(_flags);

        // Clear all flags before adding new current flags.
        currentFlags.Clear();

        for (int counter = flags.Length - 1; counter >= 0; counter--)
        {
            // The flag count is the same or less than the current flag tag.
            if (flags[counter] <= flag)
            {
                // Subtract the current flag tag from the flags.
                flag -= flags[counter];

                // Add the current flag to the list for reference.
                currentFlags.Add(flags[counter]);
            }
        }
        PlaceCharacter();
        return;
    }

    private void CheckPort()
    {
        if (serialPort.IsOpen)
        {
            try
            {
                UnpackFlag(serialPort.ReadLine());
            }

            catch (System.Exception)
            {

            }
        }
    }

    private void OnApplicationQuit()
    {
        if (serialPort.IsOpen)
        {
            serialPort.Close();
        }
    }

    public bool IsUsingBoard()
    {
        return usingBoard;
    }
}