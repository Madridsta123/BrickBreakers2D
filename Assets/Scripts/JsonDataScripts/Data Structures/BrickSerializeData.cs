using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class BrickSerializeData : ScriptableObject //2
{
    public List<BrickDataser> bData = new();

    public string GetBrickName(string gameObjName)
    {
        string brickName = string.Empty;
        if(bData != null && bData.Count>0)
        {
            BrickDataser selectedBrick = bData.Find(x => gameObjName.Contains(x.brickPrefab.name));
            //BrickDataser selectedBrick = null;
            //for (int i = 0; i < bData.Count; i++)
            //{
            //    if(gameObjName.Contains(bData[i].brickPrefab.name))
            //    {
            //        selectedBrick = bData[i];
            //        break;
            //    }
            //}

            if (selectedBrick != null)
            {
                brickName = selectedBrick.brickName;
            }
        }
        return brickName;
    }

    public GameObject GetPrefabWithName(string brickNamePara)
    {
        GameObject prefab = null;
        if(bData != null && bData.Count > 0)
        {
            //prefab = bData.Find(x => x.brickName == brickNamePara);
            BrickDataser selectedBrick = bData.Find(x => brickNamePara.Contains(x.brickPrefab.name));

            if(selectedBrick != null)
            {
                prefab = selectedBrick.brickPrefab;
            }
        }
        return prefab;
    }
}
[System.Serializable]
public class BrickDataser
{
    public string brickName;
    public GameObject brickPrefab;
}
