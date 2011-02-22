/*
 * SCXML Interpreter
 * 
 * TODO: Put the SCXML Interpreter in seperate assembly
 * As, internal attributes could only be accessed by SCXML classes
 * 
 * TODO: Look at the way for the Rule Interpreter to work for extremely large files
 * 
 * TODO: cannot create the SM for primitive data types
 **/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace StateMachine.Rules.Interpreter.SCXML
{
    public class SCXMLInterpreter : IRuleInterpreter
    {
        private XmlDocument rulesDoc;

        // TODO: Can pick these values from a config file
        private String ruleMatch = "//state[@id='{0}']";
        private String allTransitionsInARuleMatch = "transition";
        private String specificTransitionInARuleBasedOnEventMatch = "transition[@event={0}]";
        private String dataEventMatch = "transition[@event={0}]";
        private String allDataEventsInATagMatch = "send";

        private ITransitionEventComparer scxmlTransitionEventComparer;
        private IOnEvent callback;

        public SCXMLInterpreter()
        {
            scxmlTransitionEventComparer = new SCXMLTransitionEventComparer();
            rulesDoc = new XmlDocument();
        }

        public IRule InitialRule
        {
            get
            {
                string initialRuleName = rulesDoc.FirstChild.Attributes["initialstate"].Value;
                return PrepareRule(initialRuleName);
            }
        }

        private SCXMLRule PrepareRule(String ruleName)
        {

            XmlNodeList rules = rulesDoc.SelectNodes(String.Format(ruleMatch, ruleName));
            return PrepareRule(rules[0]);
        }

        private SCXMLRule PrepareRule(XmlNode node)
        {

            SCXMLRule rule = new SCXMLRule { Name = node.Attributes["id"].Value };

            foreach (XmlNode transition in node.SelectNodes(allTransitionsInARuleMatch))
            {
                SCXMLTransition scxmlTransition = new SCXMLTransition
                {
                    TargetRuleName = transition.Attributes["target"].Value,
                    Event = new SCXMLTransitionEvent { eventData = transition.Attributes["event"] == null ? null : transition.Attributes["event"].Value }
                                                    };

                foreach (XmlNode dataEvent in transition.SelectNodes(allDataEventsInATagMatch))
                {
                    scxmlTransition.DataEvents.Add(new SCXMLDataEvent
                    {
                        Name = dataEvent.Attributes["event"].Value,
                        Context = dataEvent.Attributes["context"] == null ? null : dataEvent.Attributes["context"].Value
                    });
                }

                rule.Transitions.Add(scxmlTransition);
            }

            foreach (XmlNode dataEvent in node.SelectNodes(allDataEventsInATagMatch))
            {
                rule.DataEvents.Add(new SCXMLDataEvent
                {
                    Name = dataEvent.Attributes["event"].Value,
                    Context = dataEvent.Attributes["context"] == null ? null : dataEvent.Attributes["context"].Value
                });
            }

            return rule;
        }

        /// <summary>
        /// Load a specific rule
        /// TODO: Definition is not very clear of this function, will change a lot
        /// Currently, it just traverse a rule's transitions and see if it has to go some other rule or not
        /// </summary>
        /// <param name="rule"></param>
        /// <returns></returns>
        public List<IRule> LoadRule(IRule rule)
        {
            return LoadRule(rule, new SCXMLTransitionEvent { eventData = "" });
        }

        private List<IRule> LoadRule(IRule rule, ITransitionEvent data)
        {
            return LoadRule(rule, data, scxmlTransitionEventComparer);
        }

        private List<IRule> LoadRule(IRule rule, ITransitionEvent data, ITransitionEventComparer comparer)
        {
            List<IRule> nextRules = new List<IRule>();
            Stack<IRule> ruleStack = new Stack<IRule>();

            ruleStack.Push(rule);
            int count = 0;
            while (ruleStack.Count > 0)
            {
                rule = ruleStack.Pop();
                foreach (SCXMLTransition transition in rule.Transitions)
                {
                    if ((count == 0  && comparer.Compare(transition.Event, data) > -1)
                         || transition.Event == null)
                    {
                        SCXMLRule nextRule = PrepareRule(transition.TargetRuleName);
                        nextRules.Add(nextRule);
                        ruleStack.Push(nextRule);

                        if (callback != null)
                        {
                            foreach (SCXMLDataEvent dataEvent in transition.DataEvents)
                            {
                                if (!dataEvent.IsTriggered)
                                {
                                    callback.OnEvent(dataEvent);
                                    dataEvent.IsTriggered = true;
                                }
                            }
                        }

                        // TODO: Uncomment this to suppport NFA
                        break;
                    }
                }

                if (callback != null)
                {
                    foreach (SCXMLDataEvent dataEvent in rule.DataEvents)
                    {
                        if (!dataEvent.IsTriggered)
                        {
                            callback.OnEvent(dataEvent);
                            dataEvent.IsTriggered = true;
                    }
                }
            }

                count++;
            }

            return nextRules;
        }

        private List<IRule> LoadRule(String ruleName)
        {
            return LoadRule(PrepareRule(ruleName));
        }

        public List<IRule> LoadEvent<String>(IRule rule, ITransitionEvent data)
        {
            return LoadRule(rule, data);
        }

        public List<IRule> LoadEvent<String>(IRule rule, ITransitionEvent data, ITransitionEventComparer comparer)
        {
            return LoadRule(rule, data, comparer);
        }

        public void LoadRuleFile(string filePath)
        {
            rulesDoc.Load(filePath);
        }

        public void OnEvent(IOnEvent callback)
        {
            this.callback = callback;
        }

        public ITransitionEvent CreateTransitionEvent<T>(T eventData)
        {
            return new SCXMLTransitionEvent { eventData = eventData };
        }
    }
}