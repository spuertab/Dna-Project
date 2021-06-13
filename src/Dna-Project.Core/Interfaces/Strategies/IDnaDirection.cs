namespace Dna_Project.Core.Interfaces.Strategies
{
    using Dna_Project.Core.Enums;

    public interface IDnaDirection
    {
        DnaDirection Direction { get; }

        bool ValidateDirection(string[] dna, int position, char letter, int letterPosition);
    }
}
