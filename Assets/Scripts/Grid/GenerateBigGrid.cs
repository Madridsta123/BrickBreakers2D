using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GenerateBigGrid : GridVariables
{
    private BigGridPosition posList;

    [ContextMenu("Generate")]
    public void GenerateGrid()
    {
        GridGeneration();
        StoreGridData();
    }

    private void GridGeneration()
    {
        gridPos = new();
        Vector2 gridStart = startPos.position;

        float objWidth = prefabBrick.transform.localScale.x;
        float objHeight = prefabBrick.transform.localScale.y;

        for (int r = 0; r < rows; r++)
        {
            for (int c = 0; c < cols; c++)
            {
                gridPos.x = objWidth * r;
                gridPos.y = objHeight * c;

                GameObject instantiatedObj = Instantiate(prefabBrick, (gridStart + gridPos), Quaternion.identity);
                instantiatedObj.name = ((r * cols) + c).ToString();
            }
        }
    }

    private void StoreGridData()
    {
        if (posList == null)
        {
            posList = new BigGridPosition();
        }
        else
        {
            posList.gridPositions.Clear();
        }

        bricksInGrid = GameObject.FindGameObjectsWithTag("Grid");
        foreach (GameObject brickObj in bricksInGrid)
        {
            if (brickObj.activeInHierarchy)
            {
                posList.gridPositions.Add(brickObj.transform.position);
            }
            else
            {
                Debug.Log("Object Inactive");
            }
        }

        path = "Assets/Resources/GridFiles/BigGridData.json";
        jsonDataSTR = JsonUtility.ToJson(posList);

        using FileStream fs = new(path, FileMode.Create);
        using StreamWriter writer = new(fs);
        writer.Write(jsonDataSTR);
    }
}

[System.Serializable]
public class BigGridPosition
{
    public List<Vector2> gridPositions = new();
}
