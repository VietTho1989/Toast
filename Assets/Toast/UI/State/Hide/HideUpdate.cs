using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToastS
{
    public class HideUpdate : UpdateBehavior<Hide>
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
                            // parent position to hide
                            {
                                // toastMessageUI.transform.localPosition = ToastMessageUI.HidePos;
                                ToastMessageUI.HideRect.set((RectTransform)toastMessageUI.transform);
                            }
                            // check current toast message
                            {
                                if (this.data.toastMessage.v.data != null)
                                {
                                    // update UI
                                    {
                                        if (toastMessageUI.tvMessage != null)
                                        {
                                            toastMessageUI.tvMessage.text = this.data.toastMessage.v.data.message.v;
                                        }
                                        else
                                        {
                                            Logger.LogError("toastMessageUI null");
                                        }
                                    }
                                    // change to appear
                                    {
                                        Appear appear = new Appear();
                                        {
                                            appear.uid = toastMessageUIData.state.makeId();
                                            appear.toastMessage.v = new ReferenceData<ToastMessage>(this.data.toastMessage.v.data);
                                        }
                                        toastMessageUIData.state.v = appear;
                                    }
                                }
                                else
                                {
                                    if (toastMessageUIData.toastMessage.v.data != null)
                                    {
                                        // update UI
                                        {
                                            if (toastMessageUI.tvMessage != null)
                                            {
                                                toastMessageUI.tvMessage.text = toastMessageUIData.toastMessage.v.data.message.v;
                                            }
                                            else
                                            {
                                                Logger.LogError("toastMessageUI null");
                                            }
                                        }
                                        this.data.toastMessage.v = new ReferenceData<ToastMessage>(toastMessageUIData.toastMessage.v.data);
                                    }
                                }
                            }
                            // wrap content
                            {
                                if (toastMessageUI.toastMessageContainer != null)
                                {
                                    toastMessageUI.toastMessageContainer.enabled = false;
                                    toastMessageUI.toastMessageContainer.enabled = true;
                                }
                                else
                                {
                                    Logger.LogError("toastMessageContainer null");
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
            if(data is Hide)
            {
                Hide hide = data as Hide;
                // Parent
                {
                    DataUtils.addParentCallBack(hide, this, ref this.toastMessageUIData);
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
            if (data is Hide)
            {
                Hide hide = data as Hide;
                // Parent
                {
                    DataUtils.removeParentCallBack(hide, this, ref this.toastMessageUIData);
                }
                this.setDataNull(hide);
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
            if (wrapProperty.p is Hide)
            {
                switch ((Hide.Property)wrapProperty.n)
                {
                    case Hide.Property.toastMessage:
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