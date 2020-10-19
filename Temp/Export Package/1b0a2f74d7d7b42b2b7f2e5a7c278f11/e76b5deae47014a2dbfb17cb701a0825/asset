using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

/**
 * Wrap value of object
 * */
public class VO<T> : WrapProperty
{

    #region isData

    private static readonly bool IsAddCallBackInterface = false;

    static VO()
    {
        IsAddCallBackInterface = Generic.IsAddCallBackInterface<T>();
    }

    public override void setIndex(uint i)
    {
        // Logger.LogError("why set index with VO");
    }

    public override uint getIndex()
    {
        // Logger.LogError("why get index with VO");
        return 0;
    }

    #endregion

    #region Constructor

    public VO(Data parent, byte name, T defaultValue) : base(parent, name)
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
        if (otherWrapProperty != null && otherWrapProperty is VO<T>)
        {
            VO<T> otherVP = (VO<T>)otherWrapProperty;
            this.v = otherVP.v;
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
                this.V = value;
                // Broadcast event for parent
                if (p != null)
                {
                    // List<ValueChangeCallBack> test = new List<ValueChangeCallBack>(p.callBacks);
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
        if (IsAddCallBackInterface)
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
        else
        {
            Logger.LogError("why not data: " + callBack + ";\n " + this + "; " + typeof(T));
        }
    }

    public override void allRemoveCallBack(ValueChangeCallBack callBack)
    {
        if (IsAddCallBackInterface)
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
        else
        {
            Logger.LogError("why not data: " + callBack + ";\n " + this + "; " + typeof(T));
        }
    }

    #region makeBinary

    public override void makeBinary(BinaryWriter writer)
    {
        // writer.Write(this.i);
        DataUtils.writeBinary(writer, this.v);
    }

    public override void parse(BinaryReader reader)
    {
        // this.i = reader.ReadUInt32();
        this.v = DataUtils.readBinary<T>(reader);
    }

    #endregion

    #region makeSqliteBinary

    public override void makeSqliteBinary(BinaryWriter writer)
    {
        // writer.Write(this.i);
        DataUtils.writeBinary(writer, this.v);
    }

    public override void parseSqlite(BinaryReader reader)
    {
        // this.i = reader.ReadUInt32();
        this.v = DataUtils.readBinary<T>(reader);
    }

    #endregion

}