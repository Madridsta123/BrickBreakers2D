using UnityEngine;

public class BrickHolderManager : MonoBehaviour
{
    public static BrickHolderManager brkHolderInstance;

    private void Awake()
    {
        if (brkHolderInstance != null)
        {
            Destroy(gameObject);
            return;
        }
        brkHolderInstance = this;
    }
}
