using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

public class VD<T> : WrapProperty where T : Data
{

    #region uid

    /** Id count*/
    public uint i = 0;

    public uint makeId()
    {
        i++;
        return i;
    }

    public override void setIndex(uint i)
    {
        this.i = i;
    }

    public override uint getIndex()
    {
        return this.i;
    }

    #endregion

    #region Constructor

    public VD(Data parent, byte name, T defaultValue) : base(parent, name)
    {
        this.v = defaultValue;
    }

    public override System.Type getValueType()
    {
        return typeof(T);
    }

    public override object getValue()
    {
        return this.v;
    }

    public override object getValue(int index)
    {
        if (index == 0)
        {
            return this.v;
        }
        else
        {
            return null;
        }
    }

    public override int getValueCount()
    {
        return 1;
    }

    #region Process Value

    public override void clear()
    {

    }

    public override void addValue(object value, bool needOrder = false)
    {
        if (value is T)
        {
            T t = (T)value;
            this.v = t;
        }
        else
        {
            Logger.LogError("why wrong value type: " + value + ", " + this);
        }
    }

    public override void insertValue(object value, int index)
    {
        if (index == 0)
        {
            this.addValue(value);
        }
        else
        {
            Logger.LogError("index error: " + this);
        }
    }

    public override void removeValue(object value)
    {
        if (value is T)
        {
            T t = (T)value;
            if (object.Equals(this.v, t))
            {
                this.v = default(T);
            }
            else
            {
                Logger.LogError("not the same");
            }
        }
        else
        {
            Logger.LogError("why wrong value type: " + value);
        }
    }

    public override void removeAt(int index)
    {
        if (index == 0)
        {
            T value = this.v;
            this.removeValue(value);
        }
        else
        {
            Logger.LogError("index error: " + this);
        }
    }

    #endregion

    public override Type getType()
    {
        return WrapProperty.Type.Value;
    }

    #endregion

    #region processWrapProperty

    public override void copyWrapProperty(WrapProperty otherWrapProperty)
    {
        if (otherWrapProperty != null && otherWrapProperty is VD<T>)
        {
            VD<T> otherVP = (VD<T>)otherWrapProperty;
            // copy index
            {

            }
            // Data
            {
                // Get Data
                Data oldData = this.v != null ? (Data)(object)this.v : null;
                Data newData = otherVP.v != null ? (Data)(object)otherVP.v : null;
                // Check need new
                bool needNew = true;
                {
                    if (oldData != null && newData != null)
                    {
                        if (oldData.GetType() == newData.GetType())
                        {
                            if (oldData.uid == newData.uid)
                            {
                                needNew = false;
                            }
                        }
                    }
                }
                if (needNew)
                {
                    this.v = (T)(object)DataUtils.cloneData(newData);
                }
                else
                {
                    // update
                    DataUtils.copyData(oldData, newData);
                }
            }
        }
        else
        {
            Logger.LogError("why not the same type wrapProperty: " + this + "; " + otherWrapProperty);
        }
    }

    #endregion

    #region value

    [SerializeField]
    private T V;
    public T v
    {
        get
        {
            return V;
        }

        /** Khi set null: can phai check*/
        set
        {
            if (!object.Equals(V, value))
            {
                T oldValue = V;
                // Destroy old
                {
                    if (oldValue != null)
                    {
                        Data oldData = oldValue as Data;
                        // Remove connection to parent
                        {
                            oldData.p = null;
                        }
                    }
                }
                // Set new
                V = value;
                {
                    if (value != null)
                    {
                        Data newData = value as Data;
                        // Make connection to parent
                        {
                            newData.p = this;
                        }
                    }
                }
                // Broadcast event for parent
                if (p != null)
                {
                    foreach(ValueChangeCallBack callBack in p.callBacks.ToArray())
                    {
                        if (callBack == null || ReferenceEquals(callBack, null))
                        {
                            Logger.LogError("Why callback is null: " + this);
                        }
                        else
                        {
                            // callBack.onValueChange<T> (this, oldValue, value);
                            // SyncUpdate
                            {
                                List<Sync<T>> syncs = new List<Sync<T>>();
                                {
                                    SyncSet<T> syncSet = new SyncSet<T>();
                                    {
                                        syncSet.index = 0;
                                        syncSet.olds.Add(oldValue);
                                        syncSet.news.Add(value);
                                    }
                                    syncs.Add(syncSet);
                                }
                                callBack.onUpdateSync(this, syncs);
                            }
                        }
                    }
                }
                else
                {
                    Logger.LogError("Why valueProperty parent null: " + this);
                }
            }
            else
            {
                // Debug.LogError ("why equal: " + this);
            }
        }
    }

    #endregion

    public override string ToString()
    {
        return p + ": " + n + ", " + v;
    }

    #region Undo/Redo

    private void undo(List<Sync<T>> syncs)
    {
        for (int syncCount = syncs.Count - 1; syncCount >= 0; syncCount--)
        {
            Sync<T> sync = syncs[syncCount];
            switch (sync.getType())
            {
                case Sync<T>.Type.Set:
                    {
                        SyncSet<T> syncSet = (SyncSet<T>)sync;
                        if (syncSet.olds.Count == syncSet.news.Count && syncSet.olds.Count == 1)
                        {
                            this.v = syncSet.olds[0];
                        }
                        else
                        {
                            Logger.LogError("count error: " + syncSet.olds.Count + ", " + syncSet.news.Count);
                        }
                    }
                    break;
                case Sync<T>.Type.Add:
                    {

                    }
                    break;
                case Sync<T>.Type.Remove:
                    {

                    }
                    break;
                default:
                    Logger.LogError("unknown type: " + sync.getType() + "; " + this);
                    break;
            }
        }
    }

    private void redo(List<Sync<T>> syncs)
    {
        for (int syncCount = 0; syncCount < syncs.Count; syncCount++)
        {
            Sync<T> sync = syncs[syncCount];
            switch (sync.getType())
            {
                case Sync<T>.Type.Set:
                    {
                        SyncSet<T> syncSet = (SyncSet<T>)sync;
                        if (syncSet.olds.Count == syncSet.news.Count && syncSet.olds.Count == 1)
                        {
                            this.v = syncSet.news[0];
                        }
                        else
                        {
                            Logger.LogError("count error: " + this);
                        }
                    }
                    break;
                case Sync<T>.Type.Add:
                    {

                    }
                    break;
                case Sync<T>.Type.Remove:
                    {

                    }
                    break;
                default:
                    Logger.LogError("unknown type: " + sync.getType() + "; " + this);
                    break;
            }
        }
    }

    #endregion

    public override void allAddCallBack(ValueChangeCallBack callBack)
    {
        if (this.v != null)
        {
            ((AddCallBackInterface)this.v).addCallBack(callBack);
        }
        else
        {
            // Debug.LogError ("data null: " + this);
        }
    }

    public override void allRemoveCallBack(ValueChangeCallBack callBack)
    {
        if (this.v != null)
        {
            ((AddCallBackInterface)this.v).removeCallBack(callBack);
        }
        else
        {
            // Debug.LogError ("data null: " + this);
        }
    }

    #region make new or get old

    public K newOrOld<K>() where K : T
    {
        K ret = default(K);
        {
            // Find old
            if (this.v != null)
            {
                if (this.v is K)
                {
                    ret = (K)(object)this.v;
                }
            }
            // Make new
            if (ret == null)
            {
                // Debug.LogError ("need create new: " + this + "; " + typeof(K));
                ret = Activator.CreateInstance<K>(); //(K)Activator.CreateInstance(typeof(K));
                {
                    if (ret is Data)
                    {
                        ((Data)(object)ret).uid = this.makeId();
                    }
                    else
                    {
                        Logger.LogError("why not data: " + this);
                    }
                }
                // this.v = ret;
            }
        }
        return ret;
    }

    #endregion

    #region makeBinary

    public override void makeBinary(BinaryWriter writer)
    {
        writer.Write(this.i);
        DataUtils.writeBinary(writer, this.v);
    }

    public override void parse(BinaryReader reader)
    {
        this.i = reader.ReadUInt32();
        this.v = DataUtils.readBinary<T>(reader);
    }

    #endregion

    #region makeSqliteBinary

    public override void makeSqliteBinary(BinaryWriter writer)
    {
        writer.Write(this.i);
    }

    public override void parseSqlite(BinaryReader reader)
    {
        this.i = reader.ReadUInt32();
    }

    #endregion

}