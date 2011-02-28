using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StateMachine.Rules.Interpreter.SCXML
{
    public class SCXMLDataEvent : IEvent
    {
        private String _name;
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }

        private object _context;
        public object Context
        {
            get
            {
                return _context;
            }
            set
            {
                _context = value;
            }
        }

        private string _dataExpression;
        public String DataExpression
        {
            get
            {
                return _dataExpression;
            }
            set
            {
                _dataExpression = value;
            }
        }

        private bool _isTriggred = false;
        public bool IsTriggered
        {
            get
            {
                return _isTriggred;
            }
            set
            {
                _isTriggred = value;
            }
        }
    }
}
