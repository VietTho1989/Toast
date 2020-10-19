using UnityEngine;
using System.Collections;

public class UpdateUtils
{

    public static T makeComponentUpdate<T, K>(K data, Transform transform) where T : UpdateBehavior<K> where K : Data
    {
        if (data != null)
        {
            T old = data.findCallBack<T>();
            if (old == null)
            {
                // Create new 
                T newUpdate = transform.gameObject.AddComponent<T>();
                {
                    newUpdate.setData(data);
                }
                return newUpdate;
            }
            else
            {
                Logger.Log("already have");
                return old;
            }
        }
        else
        {
            Logger.LogError("why data null: " + data);
        }
        Logger.LogError("Cannot make update: " + data + ", " + transform);
        return null;
    }

    public static T makeUpdate<T, K>(K data, Transform transform) where T : UpdateBehavior<K> where K : Data
    {
        if (data != null)
        {
            T old = data.findCallBack<T>();
            if (old == null)
            {
                // Create new 
                GameObject gameObject = GameObject.Instantiate<GameObject>(EmptyGameObject.emptyGameObject, transform, false);
                gameObject.name = "" + typeof(T);
                {
                    T newUpdate = gameObject.AddComponent<T>();
                    newUpdate.setData(data);
                    return newUpdate;
                }
            }
            else
            {
                Logger.Log("already have");
                return old;
            }
        }
        else
        {
            Logger.LogError("why data null: " + data);
        }
        Logger.LogError("Cannot make update: " + data + ", " + transform);
        return null;
    }

    public static T makeTreeUpdate<T, K>(K data, Transform transform) where T : UpdateBehavior<K> where K : Data
    {
        if (data != null)
        {
            T old = data.findCallBack<T>();
            if (old == null)
            {
                // Create new 
                GameObject gameObject = GameObject.Instantiate<GameObject>(EmptyGameObject.emptyTree, transform, false);
                gameObject.name = "" + typeof(T);
                {
                    T newUpdate = gameObject.AddComponent<T>();
                    newUpdate.setData(data);
                    return newUpdate;
                }
            }
            else
            {
                Logger.Log("already have");
                return old;
            }
        }
        else
        {
            Logger.LogError("why data null: " + data);
        }
        Logger.LogError("Cannot make update: " + data + ", " + transform);
        return null;
    }

}