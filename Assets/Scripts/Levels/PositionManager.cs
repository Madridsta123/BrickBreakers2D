using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]

public class PositionManager : MonoBehaviour, IBullet
{
    #region Variables
    public static PositionManager posInstance;

    [Header("Collision Settings")]
    public int collisionCount;
    public int totalBullets;

    [Header("Player Settings")]
    public GameObject player;
    public GameObject playerClone;
    public float moveSpeed;

    [Header("Bricks Settings")]
    public GameObject brickHolder;
    public float brickLength;
    public List<Transform> activeBricks = new();

    private const int firstCollision = 1;

    private Vector2 brickNewPos;
    private Vector2 playerNewPos;
    private List<IBrick> bricksList = new();
    #endregion

    #region In-Built Methods
    private void Awake()
    {
        playerNewPos.y = player.transform.position.y;
        playerClone.SetActive(false);

        EventsManager.eventInstance.BallHitGround += BallHittingGround;

        if (posInstance != null)
        {
            Destroy(gameObject);
            return;
        }
        posInstance = this;        
    }

    private void Start()
    {
        PopulateBrickList();
        Debug.Log(bricksList.Count);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullets"))
        {
            if (collisionCount == firstCollision)
            {
                playerClone.SetActive(true);
                playerNewPos.x = collision.transform.position.x;
                playerClone.transform.position = playerNewPos;
            }
            CheckForCollisionCount();
        }
    }

    private void OnDestroy()
    {
        EventsManager.eventInstance.BallHitGround -= BallHittingGround;
    }
    #endregion

    #region Custom Methods
    private void PopulateBrickList()
    {
        brickHolder = BrickHolderManager.brkHolderInstance.gameObject;

        if (bricksList != null)
        {
            bricksList.AddRange(brickHolder.GetComponentsInChildren<IBrick>());
        }
        else
        {
            bricksList = new();
        }

        if (activeBricks != null && bricksList != null && bricksList.Count > 0)
        {
            foreach (IBrick brick in bricksList)
            {
                if (brick != null && brick.transform.gameObject.activeInHierarchy)
                {
                    activeBricks.Add(brick.transform);
                }
            }
        }
        else
        {
            activeBricks = new();
        }
    }

    public void BallHittingGround()
    {
        collisionCount++;
        CheckForCollisionCount();
    }

    public void CheckForCollisionCount()
    {
        if (collisionCount == totalBullets)
        {
            MovePlayer();
            MoveBricks();
            collisionCount = 0;
        }
        else
        {
            Debug.Log("collisionCount != totalBullets");
        }
    }

    private void MovePlayer()
    {
        playerClone.SetActive(false);
        player.transform.position = playerNewPos;
        collisionCount = 0;
    }

    private void MoveBricks()
    {
        foreach (Transform brickTRF in activeBricks)
        {
            brickNewPos.x = brickTRF.position.x;
            brickNewPos.y = brickTRF.position.y - brickLength;
            brickTRF.position = brickNewPos;
        }
    }

    public void RemoveBricks(Transform removeBrick)
    {
        activeBricks.Remove(removeBrick);
    }
    #endregion
}
