using System;
using Xunit;
using ZeitPlan.Core;

namespace ZeitPlanTest
{
    public class UnitTest1
    {
        [Fact]
        public void EmptyString()
        {
            var t = new Tokenizer("");
            Assert.Equal(TokenType.EndToken,t.Pop().Type);

            t = new Tokenizer("    ");
            Assert.Equal(TokenType.EndToken,t.Pop().Type);
        }
        
        [Fact]
        public void MixedToken()
        {
            var t = new Tokenizer("* */2 *");
            
            Assert.True(t.Pop().IsSymbol("*"));
            Assert.True(t.Pop().IsWhiteSpace());            
            Assert.True(t.Pop().IsSymbol("*"));
            Assert.True(t.Pop().IsSymbol("/"));
            Assert.True(t.Pop().IsInteger());
            Assert.True(t.Pop().IsWhiteSpace());
            Assert.True(t.Pop().IsSymbol("*"));
            Assert.True(t.Pop().IsEnd());
           
        }
        
        [Fact]
        public void PeekTest()
        {
            var t = new Tokenizer(" \t*/ 10 - 20, 23");
            
            Assert.True(t.Peek().IsSymbol("*")); t.Pop(true);
            Assert.True(t.Peek().IsSymbol("/")); t.Pop(true);
            Assert.True(t.Peek().IsInteger()); t.Pop(true);
            Assert.True(t.Peek().IsSymbol("-")); t.Pop(true);
            Assert.True(t.Peek().IsInteger()); t.Pop(true);
            Assert.True(t.Peek().IsSymbol(",")); t.Pop(true);
            Assert.True(t.Peek().IsInteger()); t.Pop(true);


        }
    }
}