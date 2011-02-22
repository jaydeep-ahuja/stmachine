using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StateMachine.Rules.Interpreter.SCXML
{
    public class SCXMLTransition : ITransition
    {
        private String _targetRuleName;
        public string TargetRuleName
        {
            get { return _targetRuleName; }
            internal set { _targetRuleName = value; }
        }

        private ITransitionEvent _event;
        public ITransitionEvent Event
        {
            get { return _event; }
            internal set { _event = value; }
        }

        private List<IEvent> _dataEvents;
        public List<IEvent> DataEvents
        {
            get
            {
                if (_dataEvents == null) _dataEvents = new List<IEvent>();
                return _dataEvents;
            }
        }
    }
}