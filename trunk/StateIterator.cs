using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using StateMachine.Rules.Interpreter;
using StateMachine.Rules.Interpreter.SCXML;

namespace StateMachine
{
    internal class StateIterator<T> where T : class
    {
        private IRuleInterpreter rulesInterpreter;
        private StateMachine.StateMachine<T> stateMachine = null;

        internal State<T> InitialState
        {
            get {
                return State<T>.Create(rulesInterpreter.InitialRule);
            }
        }

        public StateIterator(StateMachine.StateMachine<T> stateMachine, string filePath) {

            // TODO: Use Factory Pattern to generate this interpreter instance
            rulesInterpreter = new SCXMLInterpreter();
            rulesInterpreter.LoadRuleFile(filePath);

            this.stateMachine = stateMachine;
            this.Process(null);
        }

        public IEnumerator<State<T>> Process(T input)
        {
            yield return this.stateMachine.CurrentState.Process(this.rulesInterpreter, input);
        }
    }
}