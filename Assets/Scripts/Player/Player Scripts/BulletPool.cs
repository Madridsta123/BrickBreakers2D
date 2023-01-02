using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    #region Variables
    public static BulletPool instance;

    private List<GameObject> pooledObject = new();

    public GameObject objectToPool;
    public int amountToPool;
    #endregion

    #region In-Built Methods
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        for (int i = 0; i < amountToPool; i++)
        {
            GameObject obj = Instantiate(objectToPool);
            obj.transform.parent = transform;
            obj.SetActive(false);
            pooledObject.Add(obj);
        }
    }
    #endregion

    #region Custom Methods
    public GameObject GetPooledObjects()
    {
        for (int i = 0; i < pooledObject.Count; i++)
        {
            if (!pooledObject[i].activeInHierarchy)
            {
                return pooledObject[i];
            }
        }
        return null;
    }
    #endregion
}
