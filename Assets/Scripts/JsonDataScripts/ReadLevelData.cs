using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ReadLevelData : MonoBehaviour //4
{
    #region Variables
    [Header("Player Settings")]
    public GameObject player;               //Set Bullet and Size
    public GameObject ballPrefab;           //Set Size
    public GameObject bottomBoundary;       //Set Bullet and bricks list
    public GameObject levelManager;         //Set Bullet Pool 
    public GameObject canavasDropBullet;    //Set Bullet Amount 
    public GameObject playerClone;          //Set player Size
    public TextMeshProUGUI stageNameText;   //Set Stage Name

    [Header("Brick Settings")]
    public GameObject basicPrefab;          //Set Health and Size
    public GameObject laserPrefab;          //Set Health and Size
    public GameObject rotatingPrefab;       //Set Health and Size
    public Transform brickHolder;           //Set All Bricks as Child

    [Header("Brick Data")]
    public BrickSerializeData brickData;
    public List<string> brickDataName = new();
    public List<Vector2> brickPosition = new();
    public List<GameObject> bricksObject = new();
    public JsonData jsonData;

    private GameObject brick;
    private int listCount;
    private string stageNum;
    private Vector3 brickSize;
    #endregion

    #region In-Built Methods
    public void Start()
    {
        brickHolder = BrickHolderManager.brkHolderInstance.gameObject.transform;

        ReadJsonData();
        SetPlayerValues();
        SetBrickValues();
        LoadBricks();
    }
    #endregion

    private void ReadJsonData()
    {
        stageNum = PlayerPrefs.GetString("stageNumber");
        var filePath = Resources.Load<TextAsset>($"StageFiles/Stage {stageNum}");       //var = TextAsset
        jsonData = JsonUtility.FromJson<JsonData>(filePath.ToString());
    }

    private void SetPlayerValues()
    {
        player.GetComponent<PlayerManager>().totalBullets = jsonData.ballAmt;
        player.transform.localScale = new Vector3(jsonData.ballSize, jsonData.ballSize, jsonData.ballSize);
        ballPrefab.transform.localScale = new Vector3(jsonData.ballSize, jsonData.ballSize, jsonData.ballSize);

        playerClone.transform.localScale = new Vector3(jsonData.ballSize, jsonData.ballSize, jsonData.ballSize);

        canavasDropBullet.GetComponent<DropMechanism>().totalBullets = jsonData.ballAmt;
        bottomBoundary.GetComponent<PositionManager>().totalBullets = jsonData.ballAmt;
        levelManager.GetComponent<BulletPool>().amountToPool = jsonData.ballPool;
        stageNameText.text = jsonData.stageName;
    }

    private void SetBrickValues()
    {
        basicPrefab.GetComponent<BrickMechanism>().healths = jsonData.brkLife;
        rotatingPrefab.GetComponent<BrickMechanism>().healths = jsonData.brkLife;
        bottomBoundary.GetComponent<PositionManager>().brickLength = jsonData.brickSize;

        brickSize = new Vector3(jsonData.brickSize, jsonData.brickSize, jsonData.brickSize);
        basicPrefab.transform.localScale = brickSize;
        laserPrefab.transform.localScale = brickSize;
        rotatingPrefab.transform.localScale = brickSize;
    }

    private void LoadBricks()
    {
        if (brickDataName == null || brickPosition == null || bricksObject == null)
        {
            brickDataName = new();
            brickPosition = new();
            bricksObject = new();
        }
        else
        {
            brickDataName.Clear();
            brickPosition.Clear();
            bricksObject.Clear();
        }

        for (int i = 0; i < jsonData.brickLayout.Count; i++)
        {
            brickDataName.Add(jsonData.brickLayout[i].bName);
            brickPosition.Add(jsonData.brickLayout[i].pos);
        }

        if (brickDataName.Count == brickPosition.Count)
        {
            listCount = brickDataName.Count;

            for (int i = 0; i < listCount; i++)
            {
                foreach (var brickDatas in brickData.bData)
                {
                    if (brickDataName[i] == brickDatas.brickName)
                    {
                        brick = brickDatas.brickPrefab;
                        GameObject clonedBricks = Instantiate(brick, brickPosition[i], Quaternion.identity);
                        clonedBricks.transform.parent = brickHolder;

                        bricksObject.Add(clonedBricks);
                    }
                }
            }
        }
        else
        {
            Debug.Log("brickDataName.Count != brickPosition.Count");
        }
    }
}
