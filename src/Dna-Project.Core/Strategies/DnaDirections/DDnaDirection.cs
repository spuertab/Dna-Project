namespace Dna_Project.Core.Strategies.DnaDirections
{
    using Dna_Project.Core.Config;
    using Dna_Project.Core.Enums;
    using Dna_Project.Core.Interfaces.Strategies;

    public class DDnaDirection : IDnaDirection
    {
        readonly DnaConfig _dnaConfig;

        public DDnaDirection(DnaConfig dnaConfig)
        {
            _dnaConfig = dnaConfig;
        }

        public DnaDirection Direction => DnaDirection.Down;

        public bool ValidateDirection(string[] dna, int position, char letter, int letterPosition)
        {
            int sequence = 1;

            for (int i = 1 + position; i < _dnaConfig.TotalToValidate + position; i++)
            {
                if (i < dna.Length && letter == dna[i].ToCharArray()[letterPosition])
                    sequence++;
                else
                    break;
            }

            if (sequence == _dnaConfig.TotalToValidate) return true;

            return false;
        }
    }
}
