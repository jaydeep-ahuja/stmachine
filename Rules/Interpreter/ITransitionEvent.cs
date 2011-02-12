using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StateMachine.Rules.Interpreter
{
    public interface ITransitionEvent
    {
        T GetEventData<T>();
        static ITransitionEvent CreateTransitionEvent<T>(T data);
    }
}
