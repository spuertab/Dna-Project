namespace Dna_Project.Core.Interfaces.Strategies
{
    using Dna_Project.Core.Enums;

    public interface IDnaStrategy
    {
        bool ValidateDirection(string[] dna, int position, char letter, int letterPosition, DnaDirection dnaDirection);
    }
}
