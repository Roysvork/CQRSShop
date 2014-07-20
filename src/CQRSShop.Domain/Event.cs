namespace CQRSShop.Domain
{
    using System;
    using System.Runtime.Remoting.Messaging;

    public static class Event
    {
        private const string RaiseFuncKey = "RaiseFunc";

        public static Action<object> RaiseAction
        {
            get
            {
                var raiseFunc = CallContext.LogicalGetData(RaiseFuncKey);
                if (raiseFunc == null)
                {
                    throw new Exception("Event dispatcher has not been initialised");
                }

                return (Action<object>)raiseFunc;
            }

            set
            {
                CallContext.LogicalSetData(RaiseFuncKey, value);
            }
        }

        public static void Raise(object domainEvent)
        {
            RaiseAction(domainEvent);
        }
    }
}
