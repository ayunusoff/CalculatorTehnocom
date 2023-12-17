using CalculatorTehnocom.Tokenizers.TokenReaders;
using System.IO;
using System.Reflection;

namespace CalculatorTehnocom.Tokenizers
{
    public class Tokenizer : ITokenizer
    {
        private readonly IReadOnlyCollection<ITokenReader> _tokenReaders;

        public Tokenizer(IEnumerable<ITokenReader> tokenReaders)
        {
            _tokenReaders = tokenReaders.ToList();
        }

        public IEnumerable<Token> Tokenize(string expression)
        {
            if (string.IsNullOrEmpty(expression))
                yield break;

            var reader = new StringReader(expression);

            while (reader.Peek() != -1)
            {
                var sym = (char)reader.Peek();

                if (char.IsWhiteSpace(sym))
                {
                    reader.Read();
                    continue;
                }

                var token = _tokenReaders.Single(x => x.CanRead(sym))
                                         .Read(reader);

                yield return token;

            }
        }
    }
}
