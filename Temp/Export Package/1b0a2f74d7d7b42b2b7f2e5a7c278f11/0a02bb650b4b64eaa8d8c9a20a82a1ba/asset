using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using ToastS;

public class Toast : UIBehavior<Toast.UIData>
{

    #region UIData

    public class UIData : Data
    {

        #region singleton

        protected static readonly UIData instance;

        static UIData()
        {
            instance = new UIData();
        }

        public static UIData get()
        {
            return instance;
        }

        #endregion

        public VD<ToastData> toastData;

        public VD<ToastMessageUI.UIData> toastMessageUI;

        #region Constructor

        public enum Property
        {
            toastData,
            toastMessageUI
        }

        public UIData() : base()
        {
            this.toastData = new VD<ToastData>(this, (byte)Property.toastData, new ToastData());
            this.toastMessageUI = new VD<ToastMessageUI.UIData>(this, (byte)Property.toastMessageUI, new ToastMessageUI.UIData());
        }

        #endregion

    }

    #endregion

    #region Refresh

    public override void refresh()
    {
        if (dirty)
        {
            dirty = false;
            if (this.data != null)
            {
                // Find current show toastMessage
                ToastMessage currentMessage = null;
                {
                    if (this.data.toastData.v != null)
                    {
                        ToastData toastData = this.data.toastData.v;
                        if (toastData.state.v == ToastData.State.Normal)
                        {
                            if (toastData.messages.vs.Count > 0)
                            {
                                currentMessage = toastData.messages.vs[0];
                            }
                            else
                            {
                                Logger.Log("Don't have any message");
                            }
                        }
                    }
                    else
                    {
                        Logger.LogError("toastData null");
                    }
                }
                // Show
                {
                    if (this.data.toastMessageUI.v != null)
                    {
                        ToastMessageUI.UIData toastMessageUI = this.data.toastMessageUI.v;
                        toastMessageUI.toastMessage.v = new ReferenceData<ToastMessage>(currentMessage);
                    }
                }
            }
            else
            {
                Debug.LogError("data null");
            }
        }
    }

    public override bool isShouldDisableUpdate()
    {
        return true;
    }

    #endregion

    #region implement callBacks

    public ToastMessageUI toastMessageUI;

    public override void onAddCallBack<T>(T data)
    {
        if (data is UIData)
        {
            UIData uiData = data as UIData;
            {
                uiData.toastData.allAddCallBack(this);
                uiData.toastMessageUI.allAddCallBack(this);
            }
            dirty = true;
            return;
        }
        if (data is ToastData)
        {
            ToastData toastData = data as ToastData;
            {
                ToastDataUpdate toastDataUpdate = this.GetComponent<ToastDataUpdate>();
                if (toastDataUpdate != null)
                {
                    toastDataUpdate.setData(toastData);
                }
                else
                {
                    Debug.LogError("toastDataUpdate null");
                }
            }
            dirty = true;
            return;
        }
        if (data is ToastMessageUI.UIData)
        {
            ToastMessageUI.UIData subUIData = data as ToastMessageUI.UIData;
            {
                if (toastMessageUI != null)
                {
                    toastMessageUI.setData(subUIData);
                }
                else
                {
                    Debug.LogError("toastMessageUI null");
                }
            }
            dirty = true;
            return;
        }
        Debug.LogError("Don't process: " + data + "; " + this);
    }

    public override void onRemoveCallBack<T>(T data, bool isHide)
    {
        if (data is UIData)
        {
            UIData uiData = data as UIData;
            {
                uiData.toastData.allRemoveCallBack(this);
                uiData.toastMessageUI.allRemoveCallBack(this);
            }
            this.setDataNull(uiData);
            return;
        }
        if (data is ToastData)
        {
            ToastData toastData = data as ToastData;
            {
                ToastDataUpdate toastDataUpdate = this.GetComponent<ToastDataUpdate>();
                if (toastDataUpdate != null)
                {
                    if (toastDataUpdate.data == toastData)
                    {
                        toastDataUpdate.setData(null);
                    }
                    else
                    {
                        Debug.LogError("why different");
                    }
                }
                else
                {
                    Debug.LogError("toastDataUpdate null");
                }
            }
            return;
        }
        if (data is ToastMessageUI.UIData)
        {
            ToastMessageUI.UIData subUIData = data as ToastMessageUI.UIData;
            {
                if (toastMessageUI != null)
                {
                    if (toastMessageUI.data == subUIData)
                    {
                        toastMessageUI.setData(null);
                    }
                    else
                    {
                        Debug.LogError("why different");
                    }
                }
                else
                {
                    Debug.LogError("toastMessageUI null");
                }
            }
            // Remove child
            {
                if (subUIData.toastMessage.v.data != null)
                {
                    subUIData.toastMessage.v = new ReferenceData<ToastMessage>(null);
                }
            }
            return;
        }
        Debug.LogError("Don't process: " + data + "; " + this);
    }

    public override void onUpdateSync<T>(WrapProperty wrapProperty, List<Sync<T>> syncs)
    {
        if (WrapProperty.checkError(wrapProperty))
        {
            return;
        }
        if (wrapProperty.p is UIData)
        {
            switch ((UIData.Property)wrapProperty.n)
            {
                case UIData.Property.toastData:
                    {
                        ValueChangeUtils.replaceCallBack(this, syncs);
                        dirty = true;
                    }
                    break;
                case UIData.Property.toastMessageUI:
                    {
                        ValueChangeUtils.replaceCallBack(this, syncs);
                        dirty = true;
                    }
                    break;
                default:
                    Debug.LogError("unknown wrapProperty: " + wrapProperty + "; " + this);
                    break;
            }
            return;
        }
        if (wrapProperty.p is ToastData)
        {
            switch ((ToastData.Property)wrapProperty.n)
            {
                case ToastData.Property.messages:
                    dirty = true;
                    break;
                case ToastData.Property.maxIndex:
                    break;
                case ToastData.Property.state:
                    dirty = true;
                    break;
                default:
                    Debug.LogError("unknown wrapProperty: " + wrapProperty + "; " + this);
                    break;
            }
            return;
        }
        if (wrapProperty.p is ToastMessageUI.UIData)
        {
            return;
        }
        Debug.LogError("Don't process: " + wrapProperty + "; " + syncs + "; " + this);
    }

    #endregion

    private static bool alreadyCreate = false;

    public override void Awake()
    {
        // base.Awake ();
        // uiData
        {
            alreadyCreate = true;
            this.setData(UIData.get());
        }
    }

    void Destroy()
    {
        this.setDataNull(UIData.get());
    }

    public static void showMessage(string message)
    {
        //#if (UNITY_IOS || UNITY_ANDROID)
#if (UNITY_ANDROID && !UNITY_EDITOR)
        _ShowAndroidToastMessage(message);
#else
        // create prefab
        {
            Logger.Log("showMessage: " + message);
            if (!alreadyCreate)
            {
                Logger.Log("create toast game object");
                alreadyCreate = true;
                GameObject toastGameObject = Resources.Load("VTToast") as GameObject;
                if (toastGameObject != null)
                {
                    Instantiate(toastGameObject);
                }
                else
                {
                    Logger.LogError("toastGameObject error");
                }
            }
        }
        UIData.get().toastData.v.addMessage(message);
#endif
    }

    private static void _ShowAndroidToastMessage(string message)
    {
        AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        if (unityPlayer != null)
        {
            AndroidJavaObject unityActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
            if (unityActivity != null)
            {
                AndroidJavaClass toastClass = new AndroidJavaClass("android.widget.Toast");
                if (toastClass != null)
                {
                    unityActivity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
                    {
                        AndroidJavaObject toastObject = toastClass.CallStatic<AndroidJavaObject>("makeText", unityActivity, message, 0);
                        toastObject.Call("show");
                    }));
                }
                else
                {
                    Logger.LogError("toast null");
                }
            }
        }
        else
        {
            Logger.LogError("unityPlayer null");
        }
    }

}