using UnityEngine;
using System.IO;
using System.Collections;
using System.Collections.Generic;

public abstract class WrapProperty
{

    [SerializeField]
    public Data p = null;
    [SerializeField]
    public byte n = 0;// "-1";

    public WrapProperty(Data parent, byte name)
    {
        this.p = parent;
        this.n = name;
        parent.pts.Add(this);
    }

    public enum Type
    {
        List,
        Value
    }
    public abstract Type getType();

    public abstract object getValue();

    public abstract object getValue(int index);

    public abstract int getValueCount();

    public abstract System.Type getValueType();

    #region process value

    public abstract void clear();

    public abstract void addValue(object value, bool needOrder = false);

    public abstract void insertValue(object value, int index);

    public abstract void removeValue(object value);

    public abstract void removeAt(int index);

    public abstract void copyWrapProperty(WrapProperty otherWrapProperty);

    #region index

    public abstract uint getIndex();

    public abstract void setIndex(uint i);

    #endregion

    #endregion

    public abstract void allAddCallBack(ValueChangeCallBack callBack);

    public abstract void allRemoveCallBack(ValueChangeCallBack callBack);

    #region Need create dataIdentity

    /** Need create identity or not*/
    public byte ni = 1;

    /** Need history*/
    public byte nh = 1;

    #endregion

    public static bool checkError(WrapProperty wrapProperty)
    {
        if (wrapProperty.p == null)
        {
            Logger.LogError("wrapProperty null");
            return true;
        }
        return false;
    }

    #region Compare

    public static bool isDifferent(WrapProperty wrapProperty1, WrapProperty wrapProperty2)
    {
        if (!object.Equals(wrapProperty1, wrapProperty2))
        {
            if (wrapProperty1.getType() == wrapProperty2.getType())
            {
                if (wrapProperty1.getValueCount() == wrapProperty2.getValueCount())
                {
                    bool ret = false;
                    for (int i = 0; i < wrapProperty1.getValueCount(); i++)
                    {
                        object value1 = wrapProperty1.getValue(i);
                        object value2 = wrapProperty2.getValue(i);
                        // equals
                        if (object.Equals(value1, value2))
                        {
                            // Debug.Log ("equal value, continue");
                        }
                        else
                        {
                            // if data?
                            if (value1 != null && value2 != null)
                            {
                                if (value1.GetType() != value2.GetType())
                                {
                                    ret = true;
                                    break;
                                }
                                else
                                {
                                    if (value1 is Data && value2 is Data)
                                    {
                                        Data data1 = value1 as Data;
                                        Data data2 = value2 as Data;
                                        if (DataUtils.IsDifferent(data1, data2))
                                        {
                                            ret = true;
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        ret = true;
                                        break;
                                    }
                                }
                            }
                            else
                            {
                                Logger.LogError("why value null when compare");
                            }
                        }
                    }
                    return ret;
                }
                else
                {
                    Logger.LogError("different value count: " + wrapProperty1 + "; " + wrapProperty2);
                    return true;
                }
            }
            else
            {
                Logger.LogError("not the same type: " + wrapProperty1 + "; " + wrapProperty2);
                return true;
            }
        }
        else
        {
            Logger.LogError("the same property: " + wrapProperty1 + "; " + wrapProperty2);
            return false;
        }
    }

    #endregion

    #region makeBinary

    public abstract void makeBinary(BinaryWriter writer);

    public abstract void parse(BinaryReader binaryReader);

    #endregion

    #region makeSqliteBinary

    public abstract void makeSqliteBinary(BinaryWriter writer);

    public abstract void parseSqlite(BinaryReader reader);

    #endregion

    #region Refresh

    public static void refresh<T>(LO<T> listProperty, IList<T> syncList)
    {
        if (listProperty == null || syncList == null)
        {
            Debug.LogError("IdentityUtils null error");
            return;
        }
        // find listChange
        List<Change<T>> changes = new List<Change<T>>();
        {
            // make the same count
            {
                // remove excess value
                if (listProperty.getValueCount() > syncList.Count)
                {
                    ChangeRemove<T> changeRemove = new ChangeRemove<T>();
                    {
                        changeRemove.index = syncList.Count;
                        changeRemove.number = listProperty.getValueCount() - syncList.Count;
                    }
                    changes.Add(changeRemove);
                }
                // need insert
                else if (listProperty.getValueCount() < syncList.Count)
                {
                    ChangeAdd<T> changeAdd = new ChangeAdd<T>();
                    {
                        changeAdd.index = listProperty.getValueCount();
                        for (int i = listProperty.getValueCount(); i < syncList.Count; i++)
                        {
                            changeAdd.values.Add(syncList[i]);
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
                int minCount = System.Math.Min(listProperty.getValueCount(), syncList.Count);
                // get changes
                for (int i = 0; i < minCount; i++)
                {
                    T oldValue = (T)listProperty.getValue(i);
                    T newValue = syncList[i];
                    if (!object.Equals(newValue, oldValue))
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
                        changeSet.values.Add(newValue);
                    }
                }
            }
        }
        // Change
        if (changes.Count > 0)
        {
            listProperty.processChange(changes);
        }
    }

    #endregion

}