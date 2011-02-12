using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StateMachine.Rules.Interpreter.SCXML
{
    public class SCXMLRule : IRule
    {
        public string Name
        {
            get;
            internal set { }
        }

        public List<ITransition> Transitions
        {
            get;
        }
    }
}
