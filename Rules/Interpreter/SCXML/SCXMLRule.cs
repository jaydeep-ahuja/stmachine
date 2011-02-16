using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StateMachine.Rules.Interpreter.SCXML
{
    public class SCXMLRule : IRule
    {
        private string _name;
        public string Name
        {
            get { return _name; }
            internal set { _name = value; }
        }

        private List<ITransition> _transitions;
        public List<ITransition> Transitions
        {
            get
            {
                if (_transitions == null) _transitions = new List<ITransition>();
                return _transitions;
            }
        }
    }
}