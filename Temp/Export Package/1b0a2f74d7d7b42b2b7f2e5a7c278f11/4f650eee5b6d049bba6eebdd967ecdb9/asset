using UnityEngine;
using System.Collections;

public abstract class UpdateBehavior<K> : GameBehavior<K>, DirtyInterface where K : Data
{

    private bool Dirty = true;

    protected bool dirty
    {
        get
        {
            return Dirty;
        }
        set
        {
            Dirty = value;
            if (this)
            {
                if (this.isShouldDisableUpdate())
                {
                    this.enabled = Dirty;
                }
            }
        }
    }

    public void makeDirty()
    {
        dirty = true;
    }

    public abstract bool isShouldDisableUpdate();

    void Awake()
    {
        dirty = true;
        // this.update();
    }

    public void OnEnable()
    {
        // base.OnEnable();
        dirty = true;
        // this.update();
    }

    #region Update

    void Start()
    {
        // this.update();
    }

    void FixedUpdate()
    {
        this.update();
    }

    public virtual void Update()
    {
        this.update();
    }

    public override void onAfterSetData()
    {
        base.onAfterSetData();
        this.update();
    }

    public abstract void update();

    #endregion

    /*#region implement callBacks

    public override void onAddCallBack<T>(T data)
    {
        Logger.LogError("Don't process: " + data + "; " + this);
    }

    public override void onRemoveCallBack<T>(T data, bool isHide)
    {
        Logger.LogError("Don't process: " + data + "; " + this);
    }

    public override void onUpdateSync<T>(WrapProperty wrapProperty, List<Sync<T>> syncs)
    {
        if (WrapProperty.checkError(wrapProperty))
        {
            return;
        }
        Logger.LogError("Don't process: " + data + "; " + syncs + "; " + this);
    }

    #endregion*/

}