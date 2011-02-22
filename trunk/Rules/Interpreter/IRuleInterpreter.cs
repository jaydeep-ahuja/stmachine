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
        List<IRule> LoadEvent<T>(IRule rule, ITransitionEvent data, ITransitionEventComparer comparer);
        void LoadRuleFile(string filePath);

        void OnEvent(IOnEvent callback);

        ITransitionEvent CreateTransitionEvent<T>(T eventData);
    }
}