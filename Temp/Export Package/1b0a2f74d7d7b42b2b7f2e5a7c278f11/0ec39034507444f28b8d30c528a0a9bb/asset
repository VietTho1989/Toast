using UnityEngine;
using System.Collections;

namespace ToastS
{
    public class ToastMessage : Data
    {

        public VO<int> toastIndex;

        public VO<string> message;

        public VO<float> time;

        public const float DefaultDuration = 2.5f;
        public VO<float> duration;

        #region Constructor

        public enum Property
        {
            toastIndex,
            message,
            time,
            duration
        }

        public ToastMessage() : base()
        {
            this.toastIndex = new VO<int>(this, (byte)Property.toastIndex, 0);
            this.message = new VO<string>(this, (byte)Property.message, "");
            this.time = new VO<float>(this, (byte)Property.time, 0);
            this.duration = new VO<float>(this, (byte)Property.duration, DefaultDuration);
        }

        #endregion

    }
}