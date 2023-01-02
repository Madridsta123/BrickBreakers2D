using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropMechanism : MonoBehaviour, IBullet
{
    [Header("References")]
    public Button dropButton;
    public GameObject levelManager;
    public PositionManager posManager;

    [Header("Bullet Settings")]
    public int totalBullets;

    private int collisionCount;
    private List<Transform> bricksInScene = new();

    private void Awake()
    {
        EventsManager.eventInstance.BallHitGround += BallHittingGround;
    }

    private void Start()
    {
        bricksInScene = posManager.activeBricks;

        dropButton.onClick.AddListener(() =>
        {
            DropBallMechanics();
        }
        );
    }

    private void OnDestroy()
    {
        EventsManager.eventInstance.BallHitGround -= BallHittingGround;
    }

    private void DropBallMechanics()
    {
        for (int i = 0; i < bricksInScene.Count; i++)
        {
            bricksInScene[i].gameObject.GetComponent<BoxCollider2D>().enabled = false;
            bricksInScene[i].gameObject.GetComponentInChildren<BoxCollider2D>().enabled = false;
        }
        EventsManager.eventInstance.DropBullets();
        CheckForCollisionCount();
        dropButton.interactable = false;
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
            bricksInScene = posManager.activeBricks;
            for (int i = 0; i < bricksInScene.Count; i++)
            {
                bricksInScene[i].gameObject.GetComponent<BoxCollider2D>().enabled = true;
                bricksInScene[i].gameObject.GetComponentInChildren<BoxCollider2D>().enabled = true;
            }
            collisionCount = 0;
            dropButton.interactable = true;
        }
        else
        {
            Debug.Log("collision count not equal to total bullets");
        }
    }
}
