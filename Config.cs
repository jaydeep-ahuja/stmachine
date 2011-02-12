using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StateMachine.Rules.Interpreter;

namespace StateMachine
{
    public class Config<T>
    {
        private static Config<T> configuration;
        private Config() { }

        public ITransitionEventComparer<T> stateMachineInputComparer;

        public static Config<T> GetInstance()
        {
            if (configuration == null)
                configuration = new Config<T>();

            return configuration;
        }
    }
}
