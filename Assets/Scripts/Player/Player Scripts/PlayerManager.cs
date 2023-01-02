using System.Collections;
using UnityEngine;
using TMPro;
using System.Collections.Generic;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour, IBullet
{
    #region Variables
    public static PlayerManager playerInstance;

    [Header("Time Settings")]
    public float bulletTime;
    public float offsetAngle;

    [Header("Shoot Settings")]
    public int totalBullets;
    public bool canShoot;
    public GameObject levelManager;

    [Header("Limit Rotation")]
    public float maxRotationZ;
    public float minRotationZ;

    [Header("Aim Settings")]
    public LineRenderer aimRenderer;
    public Gradient aimOffGradient;
    public Gradient aimOnGradient;
    public float rotationValue;

    [Header("HUD Settings")]
    public TextMeshProUGUI amountText;
    public Button dropButton;

    [HideInInspector] public int bulletAmount;
    [HideInInspector] public bool shooting;

    private const int rightAngle = 90;
    private const int fullAngle = 360;
    private const int straightAngle = 180;

    public bool touchCheck { get; private set; }
    public Touch fingerTouch { get; private set; }

    private List<BulletManager> bullets = new();
    private Vector3 fingerPosition;
    private Vector2 shootDirection;
    private Vector3 aimAngle;
    private float rotationZ;
    private int collisionCount;
    private GameObject bullet;
    private BulletManager bulletManager;
    //private const float currentAimPathValue = 0.075f;
    #endregion

    #region In-Built Methods
    private void Awake()
    {
        bulletAmount = totalBullets;
        dropButton.interactable = false;
        aimRenderer.enabled = true;

        if (playerInstance != null)
        {
            Destroy(gameObject);
            return;
        }
        playerInstance = this;

        EventsManager.eventInstance.BallHitGround += BallHittingGround;
    }

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            fingerTouch = Input.GetTouch(0);
            if (fingerTouch.phase == TouchPhase.Began)
            {
                touchCheck = true;
            }
            if (fingerTouch.phase == TouchPhase.Ended)
            {
                touchCheck = false;
            }
        }

        ShootCondition();
        SetPlayerRotation();
        AimMechanism();

        if (!shooting && canShoot && !touchCheck)
        {
            StartCoroutine(ShootProjectiles());
            dropButton.interactable = false;
        }
        else
        {
            StopAllCoroutines();
            Debug.Log("Cannot shoot");
        }

        amountText.text = bulletAmount.ToString();
    }

    private void OnDestroy()
    {
        EventsManager.eventInstance.BallHitGround -= BallHittingGround;
    }
    #endregion

    #region Custom Methods
    private void ShootCondition()
    {
        if (!shooting)
        {
            bullets.AddRange(levelManager.GetComponentsInChildren<BulletManager>());
            canShoot = (bullets.Count == 0);
        }
        else
        {
            Debug.Log("Is shooting");
        }
    }

    private void AimMechanism()
    {
        AimLimit();
        if (canShoot)
        {
            fingerPosition = Camera.main.ScreenToWorldPoint(fingerTouch.position);
            shootDirection = fingerPosition - transform.position;
            rotationZ = Mathf.Atan2(shootDirection.y, shootDirection.x) * Mathf.Rad2Deg - rightAngle;
            var finalRotation = rotationZ - offsetAngle;
            transform.rotation = Quaternion.Euler(0, 0, finalRotation);
        }
        else
        {
            Debug.Log($"canShoot: {canShoot}");
        }
    }

    IEnumerator ShootProjectiles()
    {
        shootDirection = transform.up;
        shooting = true;
        int i = 0;
        while (i < totalBullets)
        {
            this.enabled = false;
            aimRenderer.colorGradient = aimOffGradient;
            bullet = BulletPool.instance.GetPooledObjects();
            if (bullet != null)
            {
                bullet.transform.position = transform.position;
                bullet.SetActive(true);
                bulletManager = bullet.GetComponent<BulletManager>();
                if (bulletManager != null)
                {
                    bulletManager.Initialize(shootDirection);
                }
                yield return new WaitForSeconds(bulletTime);
                //yield return new WaitForSeconds(Random.Range(0.01666f, 0.0333f));
                i++;
                bulletAmount--;
            }
            this.enabled = true;
            dropButton.interactable = true;
        }
    }

    private void SetPlayerRotation()
    {
        aimAngle = transform.eulerAngles;
        if (aimAngle.z > straightAngle)
        {
            aimAngle.z -= fullAngle;
        }
        else
        {
            Debug.Log("aimAngle.z < straightAngle");
        }
        aimAngle.z = Mathf.Clamp(aimAngle.z, minRotationZ, maxRotationZ);
        transform.eulerAngles = aimAngle;
    }

    private void AimLimit()
    {
        if (Input.touchCount > 0 && fingerTouch.position.y > rotationValue)
        {
            canShoot = (bullets.Count == 0);
            aimRenderer.enabled = true;
        }
        else
        {
            aimRenderer.enabled = false;
            canShoot = false;
        }
    }

    public void BallHittingGround()
    {
        collisionCount++;
        CheckForCollisionCount();
    }

    public void CheckForCollisionCount()
    {
        //Checking if no. of collisions are equal to bullet amount
        if (collisionCount == totalBullets)
        {
            bulletAmount = totalBullets;
            shooting = false;
            aimRenderer.colorGradient = aimOnGradient;
            collisionCount = 0;
        }
        else
        {
            Debug.Log("collisionCount != totalBullets");
        }
    }
    #endregion
}