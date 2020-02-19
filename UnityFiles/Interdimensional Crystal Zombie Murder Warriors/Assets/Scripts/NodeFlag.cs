using UnityEngine;

public class NodeFlag
{
    private double id;
    private Vector3 location;
    private string character;

    public NodeFlag(double _id, Vector3 _location, string _character)
    {
        id = _id;
        location = _location;
        character = _character;
        return;
    }

    public double Id
    {
        set
        {
            id = value;
        }
        get
        {
            return id;
        }
    }

    public Vector3 Location
    {
        set
        {
            location = value;
        }
        get
        {
            return location;
        }
    }

    public string Character
    {
        set
        {
            character = value;
        }
        get
        {
            return character;
        }
    }
}