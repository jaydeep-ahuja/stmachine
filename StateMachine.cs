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
    public class StateMachine<T> where T : class
    {
        private StateIterator<T> stateIterator;
        private IEnumerator<State<T>> iterator;

        public StateMachine(ITransitionEventComparer<T> stateMachineInputComparer)
        {
            Config<T>.GetInstance().stateMachineInputComparer = stateMachineInputComparer;
        }

        public State<T> CurrentState
        {
            get {

                if (iterator.Current == null)
                    return stateIterator.InitialState;
                return iterator.Current;
            }
        }

        public void Process(T input)
        {
            if (iterator == null)
                iterator = stateIterator.Process(input);
            else
                stateIterator.Process(input);
        }

        public void Enter(String ruleFilePath)
        {
            if (iterator == null) {

                stateIterator = new StateIterator<T>(this, ruleFilePath);
                this.Process(null);
            }
        }

        public void Exit()
        {
            
        }

        public void Reset()
        {

        }
    }
}
