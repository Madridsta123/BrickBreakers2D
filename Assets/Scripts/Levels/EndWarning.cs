using UnityEngine;

public class EndWarning : MonoBehaviour
{
    public Animator blinkAnim;
    public GameObject childObject;

    private void Awake()
    {
        childObject.SetActive(false);
        blinkAnim.SetBool("canBlink", false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bricks") || collision.gameObject.CompareTag("SpecialBricks"))
        {
            childObject.SetActive(true);
            blinkAnim.SetBool("canBlink", true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bricks") || collision.gameObject.CompareTag("SpecialBricks"))
        {
            childObject.SetActive(false);
            blinkAnim.SetBool("canBlink", false);
        }
    }
}
