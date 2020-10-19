using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Text;

public class LD<T> : WrapProperty where T:Data
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

    public LD(Data parent, byte name) : base(parent, name)
    {
        //Debug.Log ("ListProperty Constructor: " + parent + ", " + properties);
    }

    public override string ToString()
    {
        // TODO can viet them list cac velue nua
        StringBuilder builder = new StringBuilder();
        {
            int count = this.vs.Count;
            for (int i = 0; i < count; i++)
            {
                builder.Append("" + this.vs[i]);
                if (i != count - 1)
                {
                    builder.Append(";");
                }
            }
        }
        return p + ": " + n + "; " + this.vs.Count + " {" + builder.ToString() + "}";
    }

    public override System.Type getValueType()
    {
        return typeof(T);
    }

    public override object getValue()
    {
        return this.getValue(0);
    }

    public override object getValue(int index)
    {
        if (index >= 0 && index < this.vs.Count)
        {
            return this.vs[index];
        }
        else
        {
            Logger.LogError("LP: index error: " + index);
        }
        return null;
    }

    public override int getValueCount()
    {
        return this.vs.Count;
    }

    #region Process Value

    public override void addValue(object value, bool needOrder = false)
    {
        if (value is T)
        {
            T t = (T)value;
            this.add(t, needOrder);
        }
        else
        {
            Logger.LogError("Why wrong value type: " + value);
        }
    }

    public override void removeValue(object value)
    {
        if (value is T)
        {
            T t = (T)value;
            this.remove(t);
        }
        else
        {
            Logger.LogError("why wrong value type: " + value);
        }
    }

    #region RemoveAt

    public override void removeAt(int index)
    {
        if (index >= 0 && index < this.vs.Count)
        {
            // Make change
            List<Change<T>> changes = new List<Change<T>>();
            {
                ChangeRemove<T> changeRemove = new ChangeRemove<T>();
                {
                    changeRemove.index = index;
                    changeRemove.number = 1;
                }
                changes.Add(changeRemove);
            }
            this.processChange(changes);
        }
        else
        {
            Logger.LogError("index error: " + index + "; " + this.vs.Count);
        }
    }

    public void remove(int index, int number)
    {
        if (index >= 0 && index < this.vs.Count)
        {
            if (index + number >= 0 && index + number <= this.vs.Count)
            {
                // Make change
                List<Change<T>> changes = new List<Change<T>>();
                {
                    ChangeRemove<T> changeRemove = new ChangeRemove<T>();
                    {
                        changeRemove.index = index;
                        changeRemove.number = number;
                    }
                    changes.Add(changeRemove);
                }
                this.processChange(changes);
            }
            else
            {
                Logger.LogError("index+number error: " + index + "; " + number + "; " + this.vs.Count);
            }
        }
        else
        {
            Logger.LogError("index error: " + index + "; " + this.vs.Count);
        }
    }

    public bool remove(T property)
    {
        if (property != null)
        {
            // Find index
            int index = -1;
            {
                T t = this.find(property.uid);
                if (t != null)
                {
                    index = this.vs.IndexOf(t);
                }
                else
                {
                    Logger.LogError("Cannot find data in list: " + property + "; " + this);
                }
            }
            if (index >= 0 && index < this.vs.Count)
            {
                // Make change
                List<Change<T>> changes = new List<Change<T>>();
                {
                    ChangeRemove<T> changeRemove = new ChangeRemove<T>();
                    {
                        changeRemove.index = index;
                        changeRemove.number = 1;
                    }
                    changes.Add(changeRemove);
                }
                this.processChange(changes);
                return true;
            }
            else
            {
                Logger.LogError("index error: " + index + "; " + this.vs.Count);
                return false;
            }
        }
        else
        {
            Logger.LogError("property null");
            return false;
        }
    }

    #endregion

    #endregion

    public override Type getType()
    {
        return WrapProperty.Type.List;
    }

    #endregion

    #region value

    // [SerializeField]
    private List<T> Vs = new List<T>();
    public List<T> vs
    {
        get
        {
            return Vs;
        }

        set
        {
            if (Vs != value)
            {
                // Remove old
                {
                    foreach (T property in Vs)
                    {
                        ((Data)(object)property).p = null;
                    }
                }
                // Set new
                {
                    Vs = value;
                    foreach (T property in Vs)
                    {
                        ((Data)(object)property).p = this;
                    }
                }
            }
        }
    }

    /**
     * search by id
     * */
    public T find(uint uniqueId)
    {
        foreach (T property in vs)
        {
            if (((Data)(object)property).uid.Equals(uniqueId))
            {
                return property;
            }
        }
        return null;
    }

    #endregion

    #region Operation

    public override void clear()
    {
        if (vs.Count > 0)
        {
            List<Change<T>> changes = new List<Change<T>>();
            {
                ChangeRemove<T> changeRemove = new ChangeRemove<T>();
                {
                    changeRemove.index = 0;
                    changeRemove.number = vs.Count;
                }
                changes.Add(changeRemove);
            }
            this.processChange(changes);
        }
        else
        {
            // Debug.LogError ("why don't have values: " + this);
        }
    }

    public void add(T property, bool needOrder = false)
    {
        List<Change<T>> changes = new List<Change<T>>();
        {
            ChangeAdd<T> changeAdd = new ChangeAdd<T>();
            {
                // index
                {
                    // find
                    int index = this.vs.Count;
                    {
                        if (needOrder)
                        {
                            if (this.vs.Count > 0)
                            {
                                Data newData = property as Data;
                                for (int i = this.vs.Count - 1; i >= 0; i--)
                                {
                                    Data check = this.vs[i] as Data;
                                    if (check != null)
                                    {
                                        if (newData.uid > check.uid)
                                        {
                                            index = i + 1;
                                            break;
                                        }
                                        else
                                        {
                                            index = i;
                                        }
                                    }
                                    else
                                    {
                                        Logger.LogError("check null: " + this);
                                    }
                                }
                            }
                        }
                    }
                    // set
                    changeAdd.index = index;
                }
                changeAdd.values.Add(property);
            }
            changes.Add(changeAdd);
        }
        this.processChange(changes);
    }

    public void add(List<T> properties)
    {
        List<Change<T>> changes = new List<Change<T>>();
        {
            ChangeAdd<T> changeAdd = new ChangeAdd<T>();
            {
                changeAdd.index = this.vs.Count;
                changeAdd.values.AddRange(properties);
            }
            changes.Add(changeAdd);
        }
        this.processChange(changes);
    }

    public override void insertValue(object value, int index)
    {
        if (value is T)
        {
            T t = (T)value;
            this.insert(index, t);
        }
        else
        {
            Logger.LogError("Why wrong value type: " + value);
        }
    }

    public void insert(int index, T property)
    {
        List<Change<T>> changes = new List<Change<T>>();
        {
            ChangeAdd<T> changeAdd = new ChangeAdd<T>();
            {
                changeAdd.index = index;
                changeAdd.values.Add(property);
            }
            changes.Add(changeAdd);
        }
        this.processChange(changes);
    }

    public void set(int index, T property)
    {
        List<Change<T>> changes = new List<Change<T>>();
        {
            ChangeSet<T> changeSet = new ChangeSet<T>();
            {
                changeSet.index = index;
                changeSet.values.Add(property);
            }
            changes.Add(changeSet);
        }
        this.processChange(changes);
    }

    public void copyList(List<T> list)
    {
        // find listChange
        List<Change<T>> changes = new List<Change<T>>();
        {
            // make the same count
            {
                // remove excess value
                if (this.getValueCount() > list.Count)
                {
                    ChangeRemove<T> changeRemove = new ChangeRemove<T>();
                    {
                        changeRemove.index = list.Count;
                        changeRemove.number = this.getValueCount() - list.Count;
                    }
                    changes.Add(changeRemove);
                }
                // need insert
                else if (this.getValueCount() < list.Count)
                {
                    ChangeAdd<T> changeAdd = new ChangeAdd<T>();
                    {
                        changeAdd.index = this.getValueCount();
                        for (int i = this.getValueCount(); i < list.Count; i++)
                        {
                            // add
                            Data oldData = (Data)(object)list[i];
                            changeAdd.values.Add((T)(object)DataUtils.cloneData(oldData));
                        }
                    }
                    changes.Add(changeAdd);
                }
            }
            // Copy each value
            {
                // oldChangeSet
                ChangeSet<T> oldChangeSet = null;
                // minCount
                int minCount = Math.Min(this.getValueCount(), list.Count);
                for (int i = 0; i < minCount; i++)
                {
                    T oldValue = this.vs[i];
                    T newValue = list[i];
                    // get new add data
                    T needAddData = default(T);
                    bool needAdd = false;
                    {
                        // Get Data
                        Data oldData = oldValue != null ? (Data)(object)oldValue : null;
                        Data newData = newValue != null ? (Data)(object)newValue : null;
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
                            needAdd = true;
                            needAddData = (T)(object)DataUtils.cloneData(newData);
                        }
                        else
                        {
                            // update
                            DataUtils.copyData(oldData, newData);
                        }
                    }
                    // Make changeSet
                    if (needAdd)
                    {
                        // get changeSet
                        ChangeSet<T> changeSet = null;
                        {
                            // setIdex: set position in list
                            int setIndex = i;
                            // check old
                            if (oldChangeSet != null)
                            {
                                if (oldChangeSet.index + oldChangeSet.values.Count == setIndex)
                                {
                                    changeSet = oldChangeSet;
                                }
                            }
                            // make new
                            if (changeSet == null)
                            {
                                changeSet = new ChangeSet<T>();
                                {
                                    changeSet.index = setIndex;
                                }
                                changes.Add(changeSet);
                                // set new old
                                oldChangeSet = changeSet;
                            }
                        }
                        // add value
                        changeSet.values.Add(needAddData);
                    }
                }
            }
        }
        // Change
        if (changes.Count > 0)
        {
            this.processChange(changes);
        }
    }

    #endregion

    #region Process Change

    public override void copyWrapProperty(WrapProperty otherWrapProperty)
    {
        if (otherWrapProperty is LD<T>)
        {
            LD<T> otherLD = (LD<T>)otherWrapProperty;
            // find listChange
            List<Change<T>> changes = new List<Change<T>>();
            {
                // make the same count
                {
                    // remove excess value
                    if (this.getValueCount() > otherLD.getValueCount())
                    {
                        ChangeRemove<T> changeRemove = new ChangeRemove<T>();
                        {
                            changeRemove.index = otherLD.getValueCount();
                            changeRemove.number = this.getValueCount() - otherLD.getValueCount();
                        }
                        changes.Add(changeRemove);
                    }
                    // need insert
                    else if (this.getValueCount() < otherLD.getValueCount())
                    {
                        ChangeAdd<T> changeAdd = new ChangeAdd<T>();
                        {
                            changeAdd.index = this.getValueCount();
                            for (int i = this.getValueCount(); i < otherLD.getValueCount(); i++)
                            {
                                Data oldData = (Data)(object)otherLD.vs[i];
                                changeAdd.values.Add((T)(object)DataUtils.cloneData(oldData));
                            }
                        }
                        changes.Add(changeAdd);
                    }
                }
                // Copy each value
                {
                    // oldChangeSet
                    ChangeSet<T> oldChangeSet = null;
                    // minCount
                    int minCount = Math.Min(this.getValueCount(), otherLD.getValueCount());
                    for (int i = 0; i < minCount; i++)
                    {
                        T oldValue = this.vs[i];
                        T newValue = otherLD.vs[i];
                        // get new add data
                        T needAddData = default(T);
                        bool needAdd = false;
                        {
                            // Get Data
                            Data oldData = oldValue != null ? (Data)(object)oldValue : null;
                            Data newData = newValue != null ? (Data)(object)newValue : null;
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
                                needAdd = true;
                                needAddData = (T)(object)DataUtils.cloneData(newData);
                            }
                            else
                            {
                                // update
                                DataUtils.copyData(oldData, newData);
                            }
                        }
                        // Make changeSet
                        if (needAdd)
                        {
                            // get changeSet
                            ChangeSet<T> changeSet = null;
                            {
                                // setIdex: set position in list
                                int setIndex = i;
                                // check old
                                if (oldChangeSet != null)
                                {
                                    if (oldChangeSet.index + oldChangeSet.values.Count == setIndex)
                                    {
                                        changeSet = oldChangeSet;
                                    }
                                }
                                // make new
                                if (changeSet == null)
                                {
                                    changeSet = new ChangeSet<T>();
                                    {
                                        changeSet.index = setIndex;
                                    }
                                    changes.Add(changeSet);
                                    // set new old
                                    oldChangeSet = changeSet;
                                }
                            }
                            // add value
                            changeSet.values.Add(needAddData);
                        }
                    }
                }
            }
            // Change
            if (changes.Count > 0)
            {
                this.processChange(changes);
            }
        }
        else
        {
            Logger.LogError("why not the same type wrapProperty: " + this + "; " + otherWrapProperty);
        }
    }

    public void processChange(List<Change<T>> changes)
    {
        if (changes.Count > 0)
        {
            // Make list change
            List<Sync<T>> syncs = new List<Sync<T>>();
            {
                for (int syncCount = 0; syncCount < changes.Count; syncCount++)
                {
                    Change<T> change = changes[syncCount];
                    switch (change.getType())
                    {
                        case Change<T>.Type.Set:
                            {
                                ChangeSet<T> changeSet = (ChangeSet<T>)change;
                                // keep old SyncSet
                                SyncSet<T> oldSyncSet = null;
                                if (changeSet.index >= 0 && changeSet.index + changeSet.values.Count <= this.vs.Count)
                                {
                                    for (int i = 0; i < changeSet.values.Count; i++)
                                    {
                                        int setIndex = changeSet.index + i;
                                        T oldValue = this.vs[setIndex];
                                        // check is different
                                        bool isDifferent = true;
                                        {
                                            if (object.Equals(oldValue, changeSet.values[i]))
                                            {
                                                isDifferent = false;
                                            }
                                            else
                                            {
                                                if (((Data)(object)changeSet.values[i]).uid == ((Data)(object)oldValue).uid)
                                                {
                                                    isDifferent = false;
                                                }
                                            }
                                        }
                                        // Make change
                                        if (isDifferent)
                                        {
                                            // Change
                                            {
                                                // Set parent
                                                {
                                                    ((Data)(object)oldValue).p = null;
                                                    ((Data)(object)changeSet.values[i]).p = this;
                                                }
                                                // add
                                                this.vs[setIndex] = changeSet.values[i];
                                            }
                                            // Make Sync
                                            {
                                                // get changeSet
                                                SyncSet<T> syncSet = null;
                                                {
                                                    // check old
                                                    if (oldSyncSet != null)
                                                    {
                                                        if (oldSyncSet.index + oldSyncSet.olds.Count == setIndex)
                                                        {
                                                            syncSet = oldSyncSet;
                                                        }
                                                    }
                                                    // make new
                                                    if (syncSet == null)
                                                    {
                                                        syncSet = new SyncSet<T>();
                                                        {
                                                            syncSet.index = setIndex;
                                                        }
                                                        syncs.Add(syncSet);
                                                        // set new old
                                                        oldSyncSet = syncSet;
                                                    }
                                                }
                                                // add value
                                                {
                                                    syncSet.olds.Add(oldValue);
                                                    syncSet.news.Add(changeSet.values[i]);
                                                }
                                            }
                                        }
                                        else
                                        {
                                            // Debug.LogError("why the same: " + oldValue + "; " + changeSet.values[i]);
                                        }
                                    }
                                }
                                else
                                {
                                    Logger.LogError("index error: " + changeSet.index + "; " + this.vs.Count + "; " + this);
                                }
                            }
                            break;
                        case Change<T>.Type.Add:
                            {
                                ChangeAdd<T> changeAdd = (ChangeAdd<T>)change;
                                // Add
                                if (changeAdd.index >= 0 && changeAdd.index <= this.vs.Count)
                                {
                                    // Change
                                    {
                                        // set parent
                                        {
                                            foreach (T value in changeAdd.values)
                                            {
                                                if (value != null)
                                                {
                                                    ((Data)(object)value).p = this;
                                                }
                                                else
                                                {
                                                    Logger.LogError("why value null: " + this);
                                                }
                                            }
                                        }
                                        this.vs.InsertRange(changeAdd.index, changeAdd.values);
                                    }
                                    // Make Sync
                                    {
                                        SyncAdd<T> syncAdd = new SyncAdd<T>();
                                        {
                                            syncAdd.index = changeAdd.index;
                                            syncAdd.values.AddRange(changeAdd.values);
                                        }
                                        syncs.Add(syncAdd);
                                    }
                                }
                                else
                                {
                                    Logger.LogError("index error: " + changeAdd.index + "; " + this.vs.Count + "; " + this);
                                }
                            }
                            break;
                        case Change<T>.Type.Remove:
                            {
                                ChangeRemove<T> changeRemove = (ChangeRemove<T>)change;
                                // Check index
                                if (changeRemove.number > 0 && changeRemove.index >= 0 && changeRemove.index + changeRemove.number <= this.vs.Count)
                                {
                                    // Make sync: phai make sync truoc moi lay duoc oldValues
                                    SyncRemove<T> syncRemove = new SyncRemove<T>();
                                    {
                                        syncRemove.index = changeRemove.index;
                                        for (int i = 0; i < changeRemove.number; i++)
                                        {
                                            syncRemove.values.Add(this.vs[changeRemove.index + i]);
                                        }
                                    }
                                    syncs.Add(syncRemove);
                                    // Change
                                    {
                                        // set parent
                                        {
                                            foreach (T value in syncRemove.values)
                                            {
                                                if (value != null)
                                                {
                                                    ((Data)(object)value).p = null;
                                                }
                                                else
                                                {
                                                    Logger.LogError("why value null: " + this);
                                                }
                                            }
                                        }
                                        // Remove
                                        this.vs.RemoveRange(changeRemove.index, changeRemove.number);
                                    }
                                }
                                else
                                {
                                    Logger.LogError("index error: " + this);
                                }
                            }
                            break;
                        default:
                            Logger.LogError("unknown change type: " + change.getType() + "; " + this);
                            break;
                    }
                }
            }
            // CallBack
            if (syncs.Count > 0)
            {
                foreach (ValueChangeCallBack callBack in p.callBacks.ToArray())
                {
                    callBack.onUpdateSync(this, syncs);
                }
            }
            else
            {
                // Debug.LogError("why don't have syncCount: " + this);
            }
        }
        else
        {
            Logger.LogError("why don't have changes: " + this);
        }
    }

    #endregion

    private void undo(List<Sync<T>> syncs)
    {
        List<Change<T>> changes = new List<Change<T>>();
        {
            for (int syncCount = syncs.Count - 1; syncCount >= 0; syncCount--)
            {
                Sync<T> sync = syncs[syncCount];
                switch (sync.getType())
                {
                    case Sync<T>.Type.Set:
                        {
                            SyncSet<T> syncSet = (SyncSet<T>)sync;
                            // Make change
                            ChangeSet<T> changeSet = new ChangeSet<T>();
                            {
                                changeSet.index = syncSet.index;
                                changeSet.values.AddRange(syncSet.olds);
                            }
                            changes.Add(changeSet);
                        }
                        break;
                    case Sync<T>.Type.Add:
                        {
                            SyncAdd<T> syncAdd = (SyncAdd<T>)sync;
                            // Make change
                            ChangeRemove<T> changeRemove = new ChangeRemove<T>();
                            {
                                changeRemove.index = syncAdd.index;
                                changeRemove.number = syncAdd.values.Count;
                            }
                            changes.Add(changeRemove);
                        }
                        break;
                    case Sync<T>.Type.Remove:
                        {
                            SyncRemove<T> syncRemove = (SyncRemove<T>)sync;
                            // Make change
                            ChangeAdd<T> changeAdd = new ChangeAdd<T>();
                            {
                                changeAdd.index = syncRemove.index;
                                changeAdd.values.AddRange(syncRemove.values);
                            }
                            changes.Add(changeAdd);
                        }
                        break;
                    default:
                        Logger.LogError("unknown type: " + sync.getType() + "; " + this);
                        break;
                }
            }
        }
        if (changes.Count > 0)
        {
            this.processChange(changes);
        }
    }

    private void redo(List<Sync<T>> syncs)
    {
        List<Change<T>> changes = new List<Change<T>>();
        {
            for (int syncCount = 0; syncCount < syncs.Count; syncCount++)
            {
                Sync<T> sync = syncs[syncCount];
                switch (sync.getType())
                {
                    case Sync<T>.Type.Set:
                        {
                            SyncSet<T> syncSet = (SyncSet<T>)sync;
                            // Make change
                            ChangeSet<T> changeSet = new ChangeSet<T>();
                            {
                                changeSet.index = syncSet.index;
                                changeSet.values.AddRange(syncSet.news);
                            }
                            changes.Add(changeSet);
                        }
                        break;
                    case Sync<T>.Type.Add:
                        {
                            SyncAdd<T> syncAdd = (SyncAdd<T>)sync;
                            // Make change
                            ChangeAdd<T> changeAdd = new ChangeAdd<T>();
                            {
                                changeAdd.index = syncAdd.index;
                                changeAdd.values.AddRange(syncAdd.values);
                            }
                            changes.Add(changeAdd);
                        }
                        break;
                    case Sync<T>.Type.Remove:
                        {
                            SyncRemove<T> syncRemove = (SyncRemove<T>)sync;
                            // Make change
                            ChangeRemove<T> changeRemove = new ChangeRemove<T>();
                            {
                                changeRemove.index = syncRemove.index;
                                changeRemove.number = syncRemove.values.Count;
                            }
                            changes.Add(changeRemove);
                        }
                        break;
                    default:
                        Logger.LogError("unknown type: " + sync.getType() + "; " + this);
                        break;
                }
            }
        }
        if (changes.Count > 0)
        {
            this.processChange(changes);
        }
    }

    public override void allAddCallBack(ValueChangeCallBack callBack)
    {
        foreach (T t in this.vs)
        {
            if (t != null)
            {
                t.addCallBack(callBack);
            }
            else
            {
                Logger.LogError("data null: " + callBack + "; " + this + "; " + typeof(T));
            }
        }
    }

    public override void allRemoveCallBack(ValueChangeCallBack callBack)
    {
        foreach (T t in this.vs)
        {
            if (t != null)
            {
                t.removeCallBack(callBack);
            }
            else
            {
                Logger.LogError("data null: " + callBack + "; " + this + "; " + typeof(T));
            }
        }
    }

    #region makeBinary

    public override void makeBinary(BinaryWriter writer)
    {
        writer.Write(this.i);
        // vs
        {
            writer.Write(this.vs.Count);
            foreach (T value in this.vs)
            {
                DataUtils.writeBinary(writer, value);
            }
        }
    }

    public override void parse(BinaryReader reader)
    {
        this.i = reader.ReadUInt32();
        // vs
        {
            int count = reader.ReadInt32();
            for (int i = 0; i < count; i++)
            {
                this.add(DataUtils.readBinary<T>(reader));
            }
        }
    }

    #endregion

    #region makeSqliteBinary

    public override void makeSqliteBinary(BinaryWriter writer)
    {
        writer.Write(this.i);
        // vs
        {
            writer.Write(this.vs.Count);
        }
    }

    public override void parseSqlite(BinaryReader reader)
    {
        this.i = reader.ReadUInt32();
        // vs
        {
            int count = reader.ReadInt32();
        }
    }

    #endregion

}