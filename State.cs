using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StateMachine.Rules.Interpreter;

namespace StateMachine
{
    public class State<T>
    {
        private IRule rule;

        private State(IRule rule)
        {
            this.rule = rule;
        }

        public String Name
        {
            get
            {
                return this.rule.Name;
            }
            set;
        }
        private List<T> inputBuffer;

        internal static State<T> Create(IRule rule)
        {
            return new State<T>(rule);
        }

        // TODO: Currently State Machine on a single match
        // Will need immediate change
        internal State<T> Process(IRuleInterpreter interpreter, T input)
        {
            inputBuffer.Add(input);
            List<IRule> nextRules 
                = interpreter.LoadEvent<List<T>>(this.rule
                                                    , ITransitionEvent.CreateTransitionEvent<List<T>>(inputBuffer)
                                                    , Config<T>.GetInstance().stateMachineInputComparer);

            if (nextRules == null)
                return null;

            if (nextRules[0].Name == this.Name)
                return this;

            return State<T>.Create(nextRules[0]);
        }
    }
}