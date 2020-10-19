using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToastS
{
    public class AppearUpdate : UpdateBehavior<Appear>
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
                            // change to show
                            {
                                Show show = new Show();
                                {
                                    show.uid = toastMessageUIData.state.makeId();
                                    show.toastMessage.v = new ReferenceData<ToastMessage>(this.data.toastMessage.v.data);
                                }
                                toastMessageUIData.state.v = show;
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

        public override void onAddCallBack<T>(T data)
        {
            if(data is Appear)
            {
                dirty = true;
                return;
            }
            Logger.LogError("Don't process: " + data + "; " + this);
        }

        public override void onRemoveCallBack<T>(T data, bool isHide)
        {
            if(data is Appear)
            {
                Appear appear = data as Appear;
                this.setDataNull(appear);
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
            if(wrapProperty.p is Appear)
            {
                switch ((Appear.Property)wrapProperty.n)
                {
                    case Appear.Property.toastMessage:
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