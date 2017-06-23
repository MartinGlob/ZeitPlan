using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Win32.SafeHandles;

namespace ZeitPlan.Core
{
    public enum TokenType
    {
        EndToken,
        Integer,
        String,
        Name,
        Symbol,
        Unknown,
        WhiteSpace,
    }


    public class Token
    {
        public TokenType Type { get; set; }
        public string Value { get; set; }

        public override string ToString()
        {
            return $"{Type} {Value}";
        }

        public bool IsSymbol(string value)
        {
            return Type == TokenType.Symbol && Value == value;
        }

        public bool IsInteger()
        {
            return Type == TokenType.Integer;
        }

        public bool IsWhiteSpace()
        {
            return Type == TokenType.WhiteSpace;
        }

        public bool IsEnd()
        {
            return Type == TokenType.EndToken;
        }

        public int IntValue()
        {
            return int.Parse(Value);
        }
    }

    public class Tokenizer
    {
        private List<Token> _tokens;
        private int _popIndex;

        public Tokenizer()
        {
            _tokens = new List<Token>();
            _popIndex = 0;
        }

        public Tokenizer(string s)
        {
            _tokens = new List<Token>();
            _popIndex = 0;
            Parse(s);
        }

        public Token Pop(bool skipWhiteSpace = true)
        {
            while (_popIndex < _tokens.Count && _tokens[_popIndex].IsWhiteSpace())
            {
                _popIndex++;
            }
            return _popIndex < _tokens.Count ? _tokens[_popIndex++] : new Token() {Type = TokenType.EndToken};
        }

        public Token Peek()
        {
            var i = _popIndex;

            while (i < _tokens.Count && _tokens[i].IsWhiteSpace())
            {
                i++;
            }
            return i < _tokens.Count ? _tokens[i] : new Token() {Type = TokenType.EndToken};            
        }

        public void Parse(string s)
        {
            _tokens = new List<Token>();
            
            var i = 0;

            while (i < s.Length)
            {
                var token = new Token();

                if (NextIsWhiteSpace())
                {
                    token.Type = TokenType.WhiteSpace;
                    while (NextIsWhiteSpace())
                    {
                        Next();
                    }
                    
                    // don't start with whitespace
                    if (_tokens.Any())
                    {
                        _tokens.Add(token);
                    }
                    continue;
                }

                if (NextIsDigit())
                {
                    token.Type = TokenType.Integer;
                    while (NextIsDigit())
                    {
                        token.Value += Next();
                    }
                    _tokens.Add(token);
                    continue;
                }

                if (NextIsLetter())
                {
                    token.Type = TokenType.Name;
                    while (NextIsLetterOrDigit())
                    {
                        token.Value += Next();
                    }
                    _tokens.Add(token);
                    continue;
                }

                token.Type = TokenType.Symbol;
                token.Value += Next();
                _tokens.Add(token);
            }

            char Next()
            {
                return s[i++];
            }

            bool More()
            {
                return i + 1 < s.Length;
            }

            bool NextIsWhiteSpace()
            {
                return i < s.Length && char.IsWhiteSpace(s[i]);
            }

            bool NextIsDigit()
            {
                return i < s.Length && char.IsDigit(s[i]);
            }

            bool NextIsLetter()
            {
                return i < s.Length && char.IsLetter(s[i]);
            }

            bool NextIsLetterOrDigit()
            {
                return i < s.Length && char.IsLetterOrDigit(s[i]);
            }
        }
    }
}