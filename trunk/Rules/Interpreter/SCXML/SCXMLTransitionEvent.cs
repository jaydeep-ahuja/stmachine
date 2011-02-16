using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StateMachine.Rules.Interpreter.SCXML
{
    internal class SCXMLTransitionEvent : ITransitionEvent
    {
        internal object eventData;

        public T GetEventData<T>()
        {
            return (T)eventData;
        }

        public override string ToString()
        {
            return eventData.ToString();
        }

        public static ITransitionEvent Create<T>(T data)
        {
            return new SCXMLTransitionEvent { eventData = data };
        }
    }

    internal class SCXMLTransitionEventComparer : ITransitionEventComparer
    {
        public int Compare(ITransitionEvent x, ITransitionEvent y)
        {
            return 1;
        }
    }
}
