/*
 * State Machine
 * 
 * TODO: Delegate to trigger state change
 * 
 * Two Missing Links
 * 1) T -> Comparer as String
 *      State Machine instance is created with template T, which is the mode of input State Machine would receive
 *      But, RuleInterpreter compares on an object
 * 2) StateIterator and Iterartor, why both are required
 **/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StateMachine.Rules.Interpreter;

namespace StateMachine
{
    /// <summary>
    /// StateMachine class
    /// </summary>
    /// <typeparam name="T">T specifies the type of input state machine will process</typeparam>
    public class StateMachine<T> where T : class
    {
        private StateIterator<T> stateIterator;

        /// <summary>
        /// <![CDATA[StateMachine<T> constructor]]>
        /// </summary>
        /// <param name="stateMachineInputComparer"><![CDATA[Custom IComparer<ITransitionEvent> object.
        /// User can use this object to define his own custom comparer to match the input with state transition events.]]></param>
        public StateMachine(ITransitionEventComparer stateMachineInputComparer)
        {
            Config<T>.GetInstance().stateMachineInputComparer = stateMachineInputComparer;
        }

        /// <summary>
        /// <![CDATA[Returns the current state. State<T> object.]]>
        /// </summary>
        public State<T> CurrentState
        {
            get
            {
                return stateIterator.CurrentState;
            }
        }

        /// <summary>
        /// <![CDATA[Returns the current input. T object]]>
        /// </summary>
        public T CurrentInput
        {
            get;
            set;
        }

        /// <summary>
        /// Method to process the input.
        /// </summary>
        /// <param name="input">Input object</param>
        public void Process(T input)
        {
            CurrentInput = input;
            stateIterator.Process();
        }

        /// <summary>
        /// Method to start the state machine.
        /// </summary>
        /// <param name="ruleFilePath">Full path of the rule file to load.</param>
        public void Enter(String ruleFilePath)
        {
            stateIterator = new StateIterator<T>(this, ruleFilePath);
            this.Process(null);
        }

        /// <summary>
        /// Method to exit the state machine.
        /// </summary>
        public void Exit()
        {

        }

        /// <summary>
        /// Method to again reset the state machine to Initial State.
        /// Initial state is read from the rule file.
        /// </summary>
        public void Reset()
        {

        }
    }
}
