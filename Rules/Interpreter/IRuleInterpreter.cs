using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StateMachine.Rules.Interpreter
{
    public interface IRuleInterpreter
    {
        IRule InitialRule { get; }

        List<IRule> LoadRule(IRule rule);
        List<IRule> LoadEvent<T>(IRule rule, ITransitionEvent data);
        List<IRule> LoadEvent<T>(IRule rule, ITransitionEvent data, ITransitionEventComparer<T> comparer);
        void LoadRuleFile(string filePath);
    }
}