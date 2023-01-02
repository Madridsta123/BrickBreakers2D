using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(BoxCollider2D))]

public class LevelManager : MonoBehaviour
{
    #region Variables
    [Header("Panel Settings")]
    public GameObject winPanel;
    public GameObject lostPanel;

    [Header("Script References")]
    public PlayerManager playerManager;
    public AimManager aimManager;

    private bool brickAtEnd;
    private List<Transform> activeBricks = new();
    #endregion

    #region In-Built Methods
    private void Start()
    {
        winPanel.SetActive(false);
        lostPanel.SetActive(false);
        playerManager.enabled = true;
        aimManager.enabled = true;

        if (activeBricks != null)
        {
            activeBricks = PositionManager.posInstance.activeBricks;
        }
        else
        {
            activeBricks = new();
        }
    }

    private void Update()
    {
        if (brickAtEnd || activeBricks.Count == 0)
        {
            PanelUpdate();
        }
        else
        {
            Debug.Log($"brickAtEnd : {brickAtEnd}");
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bricks") || collision.gameObject.CompareTag("SpecialBricks"))
        {
            brickAtEnd = true;
        }
    }
    #endregion

    #region Custom Methods
    private void PanelUpdate()
    {
        if (activeBricks.Count != 0 && brickAtEnd)
        {
            Time.timeScale = 0;
            lostPanel.SetActive(true);
            playerManager.enabled = false;
            aimManager.enabled = false;
        }
        if (activeBricks.Count == 0)
        {
            Time.timeScale = 0;
            winPanel.SetActive(true);
            playerManager.enabled = false;
            aimManager.enabled = false;
        }
    }
    #endregion
}
