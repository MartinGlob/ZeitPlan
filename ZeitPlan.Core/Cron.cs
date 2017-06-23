using System;
using System.Collections.Generic;
using System.Linq;

namespace ZeitPlan.Core
{
    public class X
    {
        public int Start { get; set; }
        public int End { get; set; }
        public int Repeat { get; set; }
    }
    
    public class CronSpec
    {
        public string Specification { get; internal set; }

        bool[] _minutes = new bool[60];
        bool[] _hours = new bool[24];
        bool[] _months = new bool[12];
        bool[] _date = new bool[31];
        bool[] _day  = new bool[7];
        
        public void Parse(string specification)
        {
            var tokens = new Tokenizer();
            tokens.Parse(specification);
            
            while (true)
            {
                var t = tokens.Pop();

                var start = 0;
                var stop = 59;
                var step = 1;
                
                if (t.Value == "*")
                {
                } 
                else if (t.Type == TokenType.Integer)
                {
                    start = int.Parse(t.Value);
//                    if (tokens.Peek().Value == "-")
//                    {
//                        
//                    }
                }
            }
        }

        private void SkipWhiteSpace(Stack<Token> tokens)
        {
            while (tokens.Peek().Type == TokenType.WhiteSpace)
            {
                tokens.Pop();
            }
        }
        
        private void ParseSection(Stack<Token> tokens, int min, int max, bool[] values)
        {
            
            // *|n|n-m[/n],[repeat prev]
            while (true)
            {
                var x = new X();

                SkipWhiteSpace(tokens);
                
                var t = tokens.Pop();

                if (t.IsSymbol("*"))
                {
                    
                } 
                else if (t.IsInteger())
                {
                    x.Start = x.End = t.IntValue();
                    
                }
                else
                {
                    throw  new Exception("* or integer expected");
                }

            }
        }
    }
}

// day-specification time-specification

// *    *|n|n-m [/n]