using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class JsonData //3
{
    public List<BrickDetails> brickLayout;
    public string stageName;
    public int ballAmt;
    public int ballPool;
    public int[] brkLife;
    public float ballSize;
    public float brickSize;

    public JsonData()
    {
        brickLayout = new();
    }
}

public class GenerateLevelData : MonoBehaviour
{
    [Header("Misc Setting")]
    public JsonData jsonData;
    public BrickSerializeData namePrefabRef;
    public List<GameObject> brickParents = new();

    [Header("Level Data")]
    public string stageName;
    public int ballAmt;
    public int[] brickHealth;
    public const int ballPool = 10;
    [Tooltip("Should be either 0.25 or 0.15")]
    public float ballSize;
    [Tooltip("Should be either 0.65 or 0.5")]
    public float brickSize;

    private List<IBrick> bricks = new();
    private string jsonDataStr;

    [ContextMenu("Generate")]
    public void Generate()
    {
        AddDataToVar();
        ConvertDataToJSONString();
        WriteJsonToFile();
    }

    private void AddDataToVar()
    {
        foreach (GameObject brickParent in brickParents) // we are taking the child of the element in the list where element is "brickParent".
        {
            bricks.AddRange(brickParent.GetComponentsInChildren<IBrick>());
        }

        if (jsonData == null)
        {
            jsonData = new JsonData();
        }
        else
        {
            jsonData.brickLayout.Clear();
            jsonData.ballAmt = 0;
        }

        foreach (IBrick brick in bricks)
        {
            jsonData.brickLayout.Add(new BrickDetails(namePrefabRef.GetBrickName(brick.transform.name), brick.transform.position));
        }
    }

    private void ConvertDataToJSONString()
    {
        jsonData.stageName = stageName;
        jsonData.ballAmt = ballAmt;
        jsonData.brkLife = brickHealth;
        jsonData.ballPool = ballPool + ballAmt;
        jsonData.ballSize = ballSize;
        jsonData.brickSize = brickSize;
        jsonDataStr = JsonUtility.ToJson(jsonData);
    }

    private void WriteJsonToFile()
    {
        if (string.IsNullOrEmpty(jsonData.stageName))
        {
            jsonData.stageName = "stage 0";
        }

        //string path = Application.streamingAssetsPath + "/StageFiles/" + jsonData.stageName + ".json";
        //string path = $"/Assets/Resources/StageFiles/{stageName}.json";
        //File.WriteAllText(path, jsonDataStr);

        string path = $"Assets/Resources/StageFiles/{jsonData.stageName}.json";

        using FileStream fs = new(path, FileMode.Create);
        {
            using StreamWriter writer = new(fs);
            {
                writer.Write(jsonDataStr);
            }
        }
    }
}
