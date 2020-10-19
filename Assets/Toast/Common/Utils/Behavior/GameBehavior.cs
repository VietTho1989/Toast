using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class GameBehavior<K> : MonoBehaviour, ValueChangeCallBack where K : Data
{

    public K data;

    public virtual void setData(K newData)
    {
        // before
        this.onBeforeSetData(newData);
        // set
        if (this.data != newData)
        {
            // remove old
            if (this.data != null)
            {
                this.data.removeCallBack(this);
            }
            // set new 
            {
                this.data = newData as K;
                if (this.data != null)
                {
                    this.data.addCallBack(this);
                }
            }
        }
        else
        {
            // Debug.LogError ("the same: " + this + ", " + data + ", " + newData);
        }
        // after
        if (this.data != null)
        {
            this.onAfterSetData();
        }
    }

    public virtual void onBeforeSetData(K newData)
    {

    }

    public virtual void onAfterSetData()
    {

    }

    public void setDataNull(Data removeData)
    {
        if (this.data == removeData)
        {
            this.data = null;
        }
        else
        {
            Logger.LogError("not correct removeData: " + removeData);
        }
    }

    #region implement callBacks

    public abstract void onAddCallBack<T>(T data) where T : Data;

    public abstract void onRemoveCallBack<T>(T data, bool isHide) where T : Data;

    public abstract void onUpdateSync<T>(WrapProperty wrapProperty, List<Sync<T>> syncs);

    #endregion

}