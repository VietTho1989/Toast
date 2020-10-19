using UnityEngine;
using System.Collections;

namespace ToastS
{
    public class ToastData : Data
    {

        public LD<ToastMessage> messages;

        public VO<int> maxIndex;

        #region State

        public enum State
        {
            Normal,
            WaitNext
        }

        public VO<State> state;

        #endregion

        #region Constructor

        public enum Property
        {
            messages,
            maxIndex,
            state
        }

        public ToastData() : base()
        {
            this.messages = new LD<ToastMessage>(this, (byte)Property.messages);
            this.maxIndex = new VO<int>(this, (byte)Property.maxIndex, 0);
            this.state = new VO<State>(this, (byte)Property.state, State.Normal);
        }

        #endregion

        public void addMessage(string message)
        {
            ToastMessage toastMessage = new ToastMessage();
            {
                toastMessage.uid = this.messages.makeId();
                // index
                {
                    toastMessage.toastIndex.v = this.maxIndex.v;
                    this.maxIndex.v = this.maxIndex.v + 1;
                }
                // message
                toastMessage.message.v = message;
                // time
                toastMessage.time.v = 0;
                // duration
                toastMessage.duration.v = ToastMessage.DefaultDuration;
            }
            this.messages.add(toastMessage);
        }
    }
}