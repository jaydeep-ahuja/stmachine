using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StateMachine
{
    public interface IOnStateChanged
    {
        void OnStateChanged<T>(State<T> previousState, State<T> nextState);
    }
}
