using System.Collections.Generic;
using UnityEngine;

public class dontDestroyOnLoadManager : MonoBehaviour
{
    public static List<GameObject> DontDestroyOnLoadList = new List<GameObject>();

    public static void dontDestroyOnLoad(GameObject obj)
    {
        if (!DontDestroyOnLoadList.Contains(obj))
        {
            DontDestroyOnLoadList.Add(obj);
            DontDestroyOnLoad(obj);
        }
    }
}