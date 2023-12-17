namespace CalculatorTehnocom.Tokenizers.TokenReaders
{
    public interface ITokenReader
    {
        bool CanRead(char sym);
        Token Read(StringReader stringReader);
    }
}