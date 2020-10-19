using UnityEngine;
using System.Collections;

public class EmptyGameObject 
{

    public static GameObject emptyGameObject = new GameObject();// (GameObject)Resources.Load ("EmptyGameObject");

    static EmptyGameObject()
    {
        Object.DontDestroyOnLoad(emptyGameObject);
    }

    public static GameObject emptyTree = (GameObject)Resources.Load ("EmptyTree");

}

