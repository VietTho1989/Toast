using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

namespace ToastS
{
    public class ToastMessageUI : UIBehavior<ToastMessageUI.UIData>
    {

        #region position

        public static readonly UIRectTransform HideRect;// = new UIRectTransform();
        public static readonly UIRectTransform ShowRect;// = new UIRectTransform();

        static ToastMessageUI()
        {
            HideRect = CreateRect(-5000);
            ShowRect = CreateRect(25);
        }

        private static UIRectTransform CreateRect(float bottomY)
        {
            // anchoredPosition: (0.0, 25.0); anchorMin: (0.5, 0.0); anchorMax: (0.5, 0.0); pivot: (0.5, 0.0); offsetMin: (-300.0, 25.0); offsetMax: (300.0, 78.0);
            // sizeDelta: (600.0, 53.0); localRotation: (0.0, 0.0, 0.0, 1.0); localScale: (1.0, 1.0, 1.0);
            UIRectTransform rect = new UIRectTransform();
            {
                rect.anchoredPosition = new Vector3(0.0f, bottomY, 0.0f);
                rect.anchorMin = new Vector2(0.5f, 0.0f);
                rect.anchorMax = new Vector2(0.5f, 0.0f);
                rect.pivot = new Vector2(0.5f, 0.0f);
                rect.offsetMin = new Vector2(-275.0f, bottomY);
                rect.offsetMax = new Vector2(275.0f, bottomY + 53.0f);
                rect.sizeDelta = new Vector2(550.0f, 53.0f);
            }
            return rect;
        }

        #endregion

        #region UIData

        public class UIData : Data
        {

            public VO<ReferenceData<ToastMessage>> toastMessage;

            #region state

            public abstract class State : Data
            {

                public enum Type
                {
                    Hide,
                    Appear,
                    Show,
                    Disappear
                }

                public abstract Type getType();

            }

            public VD<State> state;

            #endregion

            #region Constructor

            public enum Property
            {
                toastMessage,
                state
            }

            public UIData() : base()
            {
                this.toastMessage = new VO<ReferenceData<ToastMessage>>(this, (byte)Property.toastMessage, new ReferenceData<ToastMessage>(null));
                this.state = new VD<State>(this, (byte)Property.state, new ToastS.Hide());
            }

            #endregion

        }

        #endregion

        #region Refresh

        public HorizontalLayoutGroup toastMessageContainer;
        public Text tvMessage;

        public override void refresh()
        {
            if (dirty)
            {
                dirty = false;
                if (this.data != null)
                {

                }
                else
                {
                    // Debug.LogError ("data null: " + this);
                }
            }
        }

        public override bool isShouldDisableUpdate()
        {
            return true;
        }

        #endregion

        #region implement callBacks

        public override void onAddCallBack<T>(T data)
        {
            if (data is UIData)
            {
                Debug.Log("Toast position: " + this.transform.localPosition);
                UIData uiData = data as UIData;
                // Child
                {
                    uiData.state.allAddCallBack(this);
                }
                dirty = true;
                return;
            }
            // Child
            if (data is UIData.State)
            {
                UIData.State state = data as UIData.State;
                // Update
                {
                    switch (state.getType())
                    {
                        case UIData.State.Type.Hide:
                            {
                                Hide hide = state as Hide;
                                UpdateUtils.makeUpdate<HideUpdate, Hide>(hide, this.transform);
                            }
                            break;
                        case UIData.State.Type.Appear:
                            {
                                Appear appear = state as Appear;
                                UpdateUtils.makeUpdate<AppearUpdate, Appear>(appear, this.transform);
                            }
                            break;
                        case UIData.State.Type.Show:
                            {
                                Show show = state as Show;
                                UpdateUtils.makeUpdate<ShowUpdate, Show>(show, this.transform);
                            }
                            break;
                        case UIData.State.Type.Disappear:
                            {
                                Disappear disappear = state as Disappear;
                                UpdateUtils.makeUpdate<DisappearUpdate, Disappear>(disappear, this.transform);
                            }
                            break;
                        default:
                            Logger.LogError("unknown type: " + state.getType());
                            break;
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
                // Child
                {
                    uiData.state.allRemoveCallBack(this);
                }
                this.setDataNull(uiData);
                return;
            }
            // Child
            if (data is UIData.State)
            {
                UIData.State state = data as UIData.State;
                // Update
                {
                    switch (state.getType())
                    {
                        case UIData.State.Type.Hide:
                            {
                                Hide hide = state as Hide;
                                hide.removeCallBackAndDestroy(typeof(HideUpdate));
                            }
                            break;
                        case UIData.State.Type.Appear:
                            {
                                Appear appear = state as Appear;
                                appear.removeCallBackAndDestroy(typeof(AppearUpdate));
                            }
                            break;
                        case UIData.State.Type.Show:
                            {
                                Show show = state as Show;
                                show.removeCallBackAndDestroy(typeof(ShowUpdate));
                            }
                            break;
                        case UIData.State.Type.Disappear:
                            {
                                Disappear disappear = state as Disappear;
                                disappear.removeCallBackAndDestroy(typeof(DisappearUpdate));
                            }
                            break;
                        default:
                            Logger.LogError("unknown type: " + state.getType());
                            break;
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
                    case UIData.Property.toastMessage:
                        break;
                    case UIData.Property.state:
                        {
                            ValueChangeUtils.replaceCallBack(this, syncs);
                            dirty = true;
                        }
                        break;
                    default:
                        Debug.LogError("Don't process: " + wrapProperty + "; " + this);
                        break;
                }
                return;
            }
            // Child
            if (wrapProperty.p is UIData.State)
            {
                return;
            }
            Debug.LogError("Don't process: " + wrapProperty + "; " + syncs + "; " + this);
        }

        #endregion

    }
}