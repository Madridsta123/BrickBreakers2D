using UnityEngine;
using TMPro;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(SpriteRenderer))]

public class BrickMechanism : MonoBehaviour, IBrick
{
    [Header("Component References")]
    public Rigidbody2D rBody;
    public SpriteRenderer objectSprite;

    [Header("Health Settings")]
    public int[] healths;

    [Header("UI Settings")]
    public TextMeshProUGUI healthText;
    public float mediumTextSize = 0.6f;
    public float lowTextSize = 0.7f;

    [Header("Color Settings")]
    public Color mediumColor;
    public Color lowColor;

    internal int currentHealth;

    private const int midHealthBar = 10;
    private const int lowHealthBar = 5;

    Transform IBrick.transform { get => transform; }

    private void Start()
    {
        //Setting Health of a Brick from Healths Array
        int index = Random.Range(0, healths.Length);
        currentHealth = healths[index];

        rBody.gravityScale = 0;
    }

    private void Update()
    {
        HealthSettings();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullets"))
        {
            currentHealth--;
        }
    }

    private void HealthSettings()
    {
        healthText.text = currentHealth.ToString();

        if (currentHealth <= 0)
        {
            PositionManager.posInstance.RemoveBricks(transform);
            gameObject.SetActive(false);
        }
        if (currentHealth <= midHealthBar)
        {
            objectSprite.color = mediumColor;
        }
        if (currentHealth <= lowHealthBar)
        {
            objectSprite.color = lowColor;
        }
    }
}
