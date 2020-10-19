using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToastS
{
    public class Hide : ToastMessageUI.UIData.State
    {

        public VO<ReferenceData<ToastMessage>> toastMessage;

        #region Constructor

        public enum Property
        {
            toastMessage
        }

        public Hide() : base()
        {
            this.toastMessage = new VO<ReferenceData<ToastMessage>>(this, (byte)Property.toastMessage, new ReferenceData<ToastMessage>(null));
        }

        #endregion

        public override Type getType()
        {
            return Type.Hide;
        }

    }
}