using UnityEngine;
using System.IO;
using System.Collections.Generic;

[System.Serializable]
public class GridPositionList
{
    public List<Vector2> gridPositions = new();
}

public class GenerateGrid : MonoBehaviour
{
    #region Variables
    [Header("Grid Settings")]
    public int rows;     //9
    public int columns;     //Short-14, Long-24
    public GameObject objToSize;

    [Header("References")]
    public Transform startPos;
    public Transform endPos;

    //Short Grid 
    //startPos = -2,-3.5
    //endPos = 2,3.5

    //Long Grid 
    //startPos = -2,-1.5
    //endPos = 2,10

    //New Grid
    //startPos = -2.5, -3.5
    //endPos = 2.5, 13

    private Vector2 gridPos;
    private string path;
    private string jsonString;
    private GameObject[] gridPosition;
    private GridPositionList posList;
    #endregion

    [ContextMenu("Generate")]
    public void Grid()
    {
        #region Logic
        gridPos = new Vector2();
        Vector2 gridStart = startPos.position;
        Vector2 gridEnd = endPos.position;

        float objWidth = objToSize.transform.localScale.x;
        float objHeight = objToSize.transform.localScale.y;

        //float startX = startPos.position.x;
        //float endX = endPos.position.x;

        //float startY = startPos.position.y;
        //float endY = endPos.position.y;

        //float endWidth = startX + (objWidth * columns);
        //float endHeight = startY + (objHeight * rows);

        for (int r = 0; r < rows; r++)
        {
            for (int c = 0; c < columns; c++)
            {
                gridPos.x = objWidth * r;
                gridPos.y = objHeight * c;

                objToSize.transform.position = gridPos + gridStart;
                Instantiate(objToSize, objToSize.transform.position, Quaternion.identity);
                objToSize.name = ((r * columns) + c).ToString();
            }
        }
        #endregion

        #region StoreData
        if (posList == null)
        {
            posList = new GridPositionList();
        }
        else
        {
            posList.gridPositions.Clear();
        }

        path = Application.streamingAssetsPath + "/gridPositionData.json";

        gridPosition = GameObject.FindGameObjectsWithTag("Bricks");
        foreach (GameObject grid in gridPosition)
        {
            if (grid.gameObject.activeInHierarchy)
            {
                posList.gridPositions.Add(grid.transform.position);
            }
            else
            {
                Debug.Log("Grid Inactive");
            }

            jsonString = JsonUtility.ToJson(posList);
            File.WriteAllText(path, jsonString);
        }
        #endregion
    }
}

