using UnityEngine;

public class GridVariables : MonoBehaviour
{
    [Header("Grid Settings")]
    public int rows;
    public int cols;
    public GameObject prefabBrick;

    [Header("References")]
    public Transform startPos;
    public Transform endPos;

    protected Vector2 gridPos;
    protected GameObject[] bricksInGrid;

    protected string path;
    protected string jsonDataSTR;
}
