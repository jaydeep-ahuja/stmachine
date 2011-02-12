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

    internal class SCXMLTransitionEventComparer : ITransitionEventComparer<char>
    {
        public int Compare(List<char> x, List<char> y)
        {
            if (x.Count != y.Count) return -1;

            for (int i = 0; i < x.Count; i++)
                if (x[i] != y[i]) return -1;

            return 1;
        }
    }
}
