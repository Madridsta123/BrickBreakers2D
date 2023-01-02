using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]

public class BulletManager : MonoBehaviour
{
    [Header("Speed Settings")]
    public float bulletSpeed;
    public float downForce;
    public float maxSpeed;

    [Header("Speed Settings")]
    public float decreaseTimeBy;
    public float activeTime;

    private float intitalTime;
    private Rigidbody2D rBody;

    private const int gravityValue = 10;

    private void OnEnable()
    {
        rBody = GetComponent<Rigidbody2D>();
        rBody.gravityScale = 0;
        intitalTime = activeTime;
        EventsManager.eventInstance.DropBullets += MoveObjectDown;
    }

    private void FixedUpdate()
    {
        TimeSettings();
    }

    private void OnDisable()
    {
        activeTime = intitalTime;
        EventsManager.eventInstance.DropBullets -= MoveObjectDown;
    }

    public void Initialize(Vector2 dirToFire)
    {
        rBody.velocity = dirToFire * bulletSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            EventsManager.eventInstance.BallHitGround();
            gameObject.SetActive(false);
        }
    }
    private void MoveObjectDown()
    {
        rBody.gravityScale = gravityValue;
    }

    private void TimeSettings()
    {
        activeTime -= decreaseTimeBy * Time.deltaTime;
        if (activeTime <= 0 || rBody.velocity == Vector2.zero)
        {
            rBody.AddForce(Vector2.down * downForce, ForceMode2D.Impulse);
            activeTime = intitalTime;
            bulletSpeed = maxSpeed;
        }
        else
        {
            Debug.Log("bullet body is moving");
        }
    }
}
