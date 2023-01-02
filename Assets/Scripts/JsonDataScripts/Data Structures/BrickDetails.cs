using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BrickDetails //1
{
    public string bName;
    public Vector2 pos;

    public BrickDetails(string brName, float x, float y)
    {
        bName = brName;
        pos = new Vector2(x, y);
    }

    public BrickDetails(string brName, Vector3 pos3)
    {
        bName = brName;
        pos = new Vector2(pos3.x, pos3.y);
    }
}
