using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StateMachine.Rules.Interpreter;

namespace StateMachine
{
    /// <summary>
    /// <![CDATA[Config<T> class.]]> [Singleton class]
    /// Used to list objects which would be accessed by other classes while performing the operation.
    /// </summary>
    /// <typeparam name="T">T specifies the type of input state machine will process</typeparam>
    public class Config<T>
    {
        private static Config<T> configuration;
        private Config() { }

        /// <summary>
        /// Custom comparer specified while creating the instance of State Machine
        /// </summary>
        public ITransitionEventComparer stateMachineInputComparer;

        /// <summary>
        /// Method to get the instance of the Config class.
        /// </summary>
        /// <returns><![CDATA[Config<T> object.]]></returns>
        public static Config<T> GetInstance()
        {
            if (configuration == null)
                configuration = new Config<T>();

            return configuration;
        }
    }
}
