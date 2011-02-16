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
        private IEnumerator<State<T>> iterator;

        internal State<T> InitialState
        {
            get {
                return State<T>.Create(rulesInterpreter.InitialRule);
            }
        }

        internal State<T> CurrentState
        {
            get {

                if (iterator.Current == null)
                    return InitialState;
                else
                    return iterator.Current;
            }
        }

        public StateIterator(StateMachine.StateMachine<T> stateMachine, string filePath) {

            // TODO: Use Factory Pattern to generate this interpreter instance
            rulesInterpreter = new SCXMLInterpreter();
            rulesInterpreter.LoadRuleFile(filePath);

            this.stateMachine = stateMachine;
            this.iterator = this.GetEnumerator();
        }

        public bool Process()
        {
            return iterator.MoveNext();
        }

        public IEnumerator<State<T>> GetEnumerator()
        {
            State<T> state = null;
            while ((state = this.CurrentState.Process(this.rulesInterpreter, this.stateMachine.CurrentInput)) != null)
                yield return state;

            yield return null;
        }
    }
}