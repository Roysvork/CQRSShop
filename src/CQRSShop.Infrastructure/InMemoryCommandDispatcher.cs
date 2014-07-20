namespace CQRSShop.Infrastructure
{
    using System;
    using System.Collections.Generic;

    public class InMemoryCommandDispatcher : ICommandDispatcher
    {
        private readonly Dictionary<Type, List<Action<object>>> handlerActions;

        public InMemoryCommandDispatcher()
        {
            this.handlerActions = new Dictionary<Type, List<Action<object>>>();
        }

        public void Dispatch(object command)
        {
            var type = command.GetType();
            if (!this.handlerActions.ContainsKey(type))
            {
                throw new InvalidOperationException(string.Format("No handlers were found for command {0}", type));
            }

            foreach (var handler in this.handlerActions[type])
            {
                handler(command);
            }
        }

        public void RegisterHandler<T>(Action<T> handlerFunc)
        {
            var type = typeof(T);
            if (!this.handlerActions.ContainsKey(type))
            {
                this.handlerActions.Add(type, new List<Action<object>>());
            }

            var actionList = this.handlerActions[type];
            actionList.Add(o => handlerFunc((T)o));
        }
    }
}
