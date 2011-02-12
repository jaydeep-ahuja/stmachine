using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StateMachine.Rules.Interpreter
{
    public interface ITransition
    {
        String TargetRuleName { get; }
        ITransitionEvent Event { get; }
    }
}
