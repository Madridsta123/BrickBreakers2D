using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]

public class LaserBrick : MonoBehaviour, IBrick
{
    [Header("Settings")]
    public Transform pointA;
    public Transform pointB;

    public bool canDestroy = false;

    Transform IBrick.transform { get => transform; }

    private void Update()
    {
        FoundBricks();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullets"))
        {
            canDestroy = true;
        }
    }

    private void FoundBricks()
    {
        if (canDestroy)
        {
            gameObject.SetActive(false);

            RaycastHit2D[] hitAlls = Physics2D.RaycastAll(pointB.position, (pointA.position - pointB.position).normalized);
            foreach (RaycastHit2D hitAll in hitAlls)
            {
                if (hitAll.collider != null)
                {
                    if (hitAll.collider.gameObject.CompareTag("Bricks"))
                    {
                        PositionManager.posInstance.RemoveBricks(transform);
                        hitAll.collider.gameObject.GetComponent<BrickMechanism>().currentHealth = 0;
                    }
                }
            }
        }
        else
        {
            Debug.Log("canDestroy is false");
        }
    }
}
