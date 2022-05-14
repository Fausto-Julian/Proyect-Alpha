using System.Collections.Generic;
using UnityEngine;

public class PoolGeneric
{
    private List<GameObject> availables = new List<GameObject>();
    private List<GameObject> inUse = new List<GameObject>();

    public int AvailablesCount => availables.Count;

    public GameObject GetorCreate()
    {
        if (availables.Count > 0)
        {
            var obj = availables[0];
            availables.RemoveAt(0);
            inUse.Add(obj);
            obj.SetActive(true);
            return obj;
        }
        return null;
    }

    public void InUseToAvailables(GameObject poolEntry)
    {
        poolEntry.SetActive(false);
        inUse.Remove(poolEntry);
        availables.Add(poolEntry);
    }
}