using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StateMachine.Rules.Interpreter;

namespace StateMachine
{
    /// <summary>
    /// <![CDATA[State<T> entity class]]>
    /// State class is used by state machine to represent a rule in rule file and to buffer the input processed by this state.
    /// </summary>
    /// <typeparam name="T">T specifies the type of input state machine will process</typeparam>
    public class State<T>
    {
        private IRule rule;

        /// <summary>
        /// Private State constructor
        /// </summary>
        /// <param name="rule">Rule object from which to create the state.</param>
        private State(IRule rule)
        {
            inputBuffer = new List<T>();
            this.rule = rule;
        }

        /// <summary>
        /// Name of the state as read from the rule
        /// </summary>
        public String Name
        {
            get
            {
                return this.rule.Name;
            }
            set { }
        }

        /// <summary>
        /// Buffer of input current state has processed.
        /// Used by comparer to compare with events in rule file.
        /// </summary>
        internal List<T> inputBuffer;

        /// <summary>
        /// Method to create a state of a rule.
        /// </summary>
        /// <param name="rule">Rule object.</param>
        /// <returns>Created State object.</returns>
        internal static State<T> Create(IRule rule)
        {
            return new State<T>(rule);
        }

        /// <summary>
        /// Method to process the input.
        /// TODO: Currently State Machine on a single match
        /// Will need immediate change
        /// </summary>
        /// <param name="interpreter">Rule Interpreter as provided by dependency injection.</param>
        /// <param name="input">Input object to process</param>
        /// <returns>Returns the State loaded after processing the input over the rule file.</returns>
        internal State<T> Process(IRuleInterpreter interpreter, T input)
        {
            inputBuffer.Add(input);
            List<IRule> nextRules 
                = interpreter.LoadEvent<List<T>>(this.rule
                                                    , interpreter.CreateTransitionEvent<List<T>>(inputBuffer)
                                                    , Config<T>.GetInstance().stateMachineInputComparer);

            if (nextRules.Count == 0
                    || nextRules[0].Name == this.Name)
                return this;

            return State<T>.Create(nextRules[0]);
        }
    }
}