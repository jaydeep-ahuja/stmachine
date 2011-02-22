using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StateMachine.Rules.Interpreter
{
    public interface IEvent
    {
        String Name { get; set; }
        Object Context { get; set; }
        Boolean IsTriggered { get; set; }
    }
}
