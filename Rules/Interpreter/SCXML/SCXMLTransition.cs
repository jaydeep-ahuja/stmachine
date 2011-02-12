using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StateMachine.Rules.Interpreter.SCXML
{
    public class SCXMLTransition : ITransition
    {
        public string TargetRuleName
        {
            get;
            internal set { }
        }

        public ITransitionEvent Event
        {
            get;
            internal set { }
        }
    }
}
