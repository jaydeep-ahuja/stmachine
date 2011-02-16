using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using StateMachine.Rules.Interpreter;
using StateMachine.Rules.Interpreter.SCXML;

namespace StateMachine
{
    /// <summary>
    /// StateIterator class
    /// Used by State Machine to process input and iterate to next states.
    /// </summary>
    /// <typeparam name="T">T specifies the type of input state machine will process</typeparam>
    internal class StateIterator<T> where T : class
    {
        private IRuleInterpreter rulesInterpreter;
        private StateMachine.StateMachine<T> stateMachine = null;
        private IEnumerator<State<T>> iterator;

        /// <summary>
        /// Initial State of state machine as read from rule file. <![CDATA[State<T> object.]]>
        /// </summary>
        internal State<T> InitialState
        {
            get {
                return State<T>.Create(rulesInterpreter.InitialRule);
            }
        }

        /// <summary>
        /// Currennt State of the state machine. <![CDATA[State<T> object.]]>
        /// </summary>
        internal State<T> CurrentState
        {
            get {

                if (iterator.Current == null)
                    return InitialState;
                else
                    return iterator.Current;
            }
        }

        /// <summary>
        /// StateIterator constructor
        /// </summary>
        /// <param name="stateMachine">Instance of state machine.</param>
        /// <param name="filePath">Full path of the rule file.</param>
        public StateIterator(StateMachine.StateMachine<T> stateMachine, string filePath) {

            // TODO: Use Factory Pattern to generate this interpreter instance
            rulesInterpreter = new SCXMLInterpreter();
            rulesInterpreter.LoadRuleFile(filePath);

            this.stateMachine = stateMachine;
            this.iterator = this.GetEnumerator();
        }

        /// <summary>
        /// Method to process the input.
        /// </summary>
        /// <returns>Returns true if input is successfully processed.</returns>
        public bool Process()
        {
            return iterator.MoveNext();
        }

        /// <summary>
        /// Method to get the Iterator object.
        /// </summary>
        /// <returns>Returns the state after processing the Current Input.</returns>
        public IEnumerator<State<T>> GetEnumerator()
        {
            State<T> state = null;
            while ((state = this.CurrentState.Process(this.rulesInterpreter, this.stateMachine.CurrentInput)) != null)
                yield return state;

            yield return null;
        }
    }
}