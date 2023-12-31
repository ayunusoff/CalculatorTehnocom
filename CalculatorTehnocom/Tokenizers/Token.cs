﻿namespace CalculatorTehnocom.Tokenizers
{
    public class Token : IEquatable<Token>
    {
        public Token(TokenType type, string value)
        {
            Type = type;
            Value = value;
        }

        public Token(TokenType type, string value, ElementType elementType) : this(type, value)
        {
            ElementType = elementType;
        }

        public TokenType Type { get; }

        public string Value { get; }

        public ElementType ElementType { get; }

        public bool Equals(Token other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Type == other.Type && Value == other.Value && ElementType == other.ElementType;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((Token)obj);
        }

        public override string ToString()
        {
            return Value;
        }
    }
}
