using UnityEngine;

public class RotateBricks : MonoBehaviour
{
    public float rotSpeed;

    private void Update()
    {
        RotateMechanics();
    }

    private void RotateMechanics()
    {
        if (Time.timeScale != 0)
        {
            transform.Rotate(0, 0, rotSpeed);
        }
        else
        {
            Debug.Log("Time.timeScale = 0");
        }
    }
}
