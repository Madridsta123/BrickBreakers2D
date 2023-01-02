using UnityEngine;

public class BricksSpawner : MonoBehaviour
{
    [Header("Brick Settings")]
    public bool isSmallGrid;

    [Header("Basic Bricks Setting")]
    public GameObject basicBrick;
    public int[] basicSpawnPoints;

    [Header("Laser Bricks Setting")]
    public GameObject laserBrick;
    public int[] laserSpawnPoints;

    [Header("Rotating Bricks Setting")]
    public GameObject rotateBrick;
    public int[] rotatingSpawnPoints;

    private TextAsset filePath;
    private GridPositionList tilePos;
    private Vector2 brickPos;
    private Vector3 newBrickSize;

    private const float smallBrickSize = 0.5f;
    private const float bigBrickSize = 0.65f;

    [ContextMenu("GenerateBricks")]
    public void SpawnBricks()
    {
        ReadJsonData();
        SpawnBasic();
        SpawnLaser();
        SpawnRotating();
    }

    private void ReadJsonData()
    {
        if (isSmallGrid)
        {
            filePath = Resources.Load<TextAsset>("GridFiles/SmallGridData");
            newBrickSize = new Vector3(bigBrickSize, bigBrickSize, bigBrickSize);
        }
        else
        {
            filePath = Resources.Load<TextAsset>("GridFiles/BigGridData");
            newBrickSize = new Vector3(smallBrickSize, smallBrickSize, smallBrickSize);
        }
        tilePos = JsonUtility.FromJson<GridPositionList>(filePath.ToString());
    }

    private void SpawnBasic()
    {
        basicBrick.transform.localScale = newBrickSize;
        foreach (int x in basicSpawnPoints)
        {
            brickPos = tilePos.gridPositions[x];
            Instantiate(basicBrick, brickPos, Quaternion.identity);
        }
    }

    private void SpawnLaser()
    {
        laserBrick.transform.localScale = newBrickSize;
        foreach (int x in laserSpawnPoints)
        {
            brickPos = tilePos.gridPositions[x];
            Instantiate(laserBrick, brickPos, Quaternion.identity);
        }
    }

    private void SpawnRotating()
    {
        rotateBrick.transform.localScale = newBrickSize;
        foreach (int x in rotatingSpawnPoints)
        {
            brickPos = tilePos.gridPositions[x];
            Instantiate(rotateBrick, brickPos, Quaternion.identity);
        }
    }

}
