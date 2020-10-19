using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToastS
{
    public class ShowUpdate : UpdateBehavior<Show>
    {

        #region Update

        public override void Update()
        {
            base.Update();
            if (dirty)
            {
                dirty = false;
                if (this.data != null)
                {
                    ToastMessageUI.UIData toastMessageUIData = this.data.findDataInParent<ToastMessageUI.UIData>();
                    if (toastMessageUIData != null)
                    {
                        ToastMessageUI toastMessageUI = toastMessageUIData.findCallBack<ToastMessageUI>();
                        if (toastMessageUI != null)
                        {
                            // UI
                            {
                                ToastMessageUI.ShowRect.set((RectTransform)toastMessageUI.transform);
                                // tvMessage
                                {
                                    if (toastMessageUI.tvMessage != null)
                                    {
                                        ToastMessage toastMessage = this.data.toastMessage.v.data;
                                        if (toastMessage != null)
                                        {
                                            toastMessageUI.tvMessage.text = toastMessage.message.v;
                                        }
                                        else
                                        {
                                            Logger.LogError("toastMessage null");
                                        }
                                    }
                                    else
                                    {
                                        Logger.LogError("tvMessage null");
                                    }
                                }
                            }
                            // change to disappear
                            {
                                if (this.data.toastMessage.v.data != toastMessageUIData.toastMessage.v.data)
                                {
                                    Disappear disappear = new Disappear();
                                    {
                                        disappear.uid = toastMessageUIData.state.makeId();
                                        disappear.toastMessage.v = new ReferenceData<ToastMessage>(this.data.toastMessage.v.data);
                                    }
                                    toastMessageUIData.state.v = disappear;
                                }
                                else
                                {
                                    Logger.Log("not different, don't need to change");
                                }
                            }
                        }
                        else
                        {
                            Logger.LogError("toastMessageUI null");
                        }
                    }
                    else
                    {
                        Logger.LogError("toastMessageUIData null");
                    }
                }
                else
                {
                    Logger.LogError("data null");
                }
            }
        }

        public override void update()
        {

        }

        public override bool isShouldDisableUpdate()
        {
            return true;
        }

        #endregion

        #region implement callBacks

        private ToastMessageUI.UIData toastMessageUIData = null;

        public override void onAddCallBack<T>(T data)
        {
            if(data is Show)
            {
                Show show = data as Show;
                // Parent
                {
                    DataUtils.addParentCallBack(show, this, ref this.toastMessageUIData);
                }
                dirty = true;
                return;
            }
            // Parent
            if(data is ToastMessageUI.UIData)
            {
                dirty = true;
                return;
            }
            Logger.LogError("Don't process: " + data + "; " + this);
        }

        public override void onRemoveCallBack<T>(T data, bool isHide)
        {
            if (data is Show)
            {
                Show show = data as Show;
                // Parent
                {
                    DataUtils.removeParentCallBack(show, this, ref this.toastMessageUIData);
                }
                this.setDataNull(show);
                return;
            }
            // Parent
            if (data is ToastMessageUI.UIData)
            {
                return;
            }
            Logger.LogError("Don't process: " + data + "; " + this);
        }

        public override void onUpdateSync<T>(WrapProperty wrapProperty, List<Sync<T>> syncs)
        {
            if (WrapProperty.checkError(wrapProperty))
            {
                return;
            }
            if (wrapProperty.p is Show)
            {
                switch ((Show.Property)wrapProperty.n)
                {
                    case Show.Property.toastMessage:
                        dirty = true;
                        break;
                    default:
                        Logger.LogError("Don't process: " + wrapProperty + "; " + this);
                        break;
                }
                return;
            }
            // Parent
            if (wrapProperty.p is ToastMessageUI.UIData)
            {
                switch ((ToastMessageUI.UIData.Property)wrapProperty.n)
                {
                    case ToastMessageUI.UIData.Property.toastMessage:
                        dirty = true;
                        break;
                    case ToastMessageUI.UIData.Property.state:
                        break;
                    default:
                        Logger.LogError("Don't process: " + wrapProperty + "; " + this);
                        break;
                }
                return;
            }
            Logger.LogError("Don't process: " + data + "; " + syncs + "; " + this);
        }

        #endregion

    }
}