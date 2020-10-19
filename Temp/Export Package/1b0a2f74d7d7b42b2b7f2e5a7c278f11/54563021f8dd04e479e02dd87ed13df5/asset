using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToastS
{
    public class Disappear : ToastMessageUI.UIData.State
    {

        public VO<ReferenceData<ToastMessage>> toastMessage;

        #region Constructor

        public enum Property
        {
            toastMessage
        }

        public Disappear() : base()
        {
            this.toastMessage = new VO<ReferenceData<ToastMessage>>(this, (byte)Property.toastMessage, new ReferenceData<ToastMessage>(null));
        }

        #endregion

        public override Type getType()
        {
            return Type.Disappear;
        }

    }
}