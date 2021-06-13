using Dna_Project.Core.Config;
using Dna_Project.Core.Enums;
using Dna_Project.Core.Interfaces.Strategies;

namespace Dna_Project.Core.Strategies.DnaDirections
{
    public class RDnaDirection : IDnaDirection
    {
        readonly DnaConfig _dnaConfig;

        public RDnaDirection(DnaConfig dnaConfig)
        {
            _dnaConfig = dnaConfig;
        }

        public DnaDirection Direction => DnaDirection.Right;

        public bool ValidateDirection(string[] dna, int position, char letter, int letterPosition)
        {
            int sequence = 1;

            for (int i = 1 + letterPosition; i < _dnaConfig.TotalToValidate + letterPosition; i++)
            {
                if (i < dna[position].ToCharArray().Length && letter == dna[position].ToCharArray()[i])
                    sequence++;
                else
                    break;
            }

            if (sequence == _dnaConfig.TotalToValidate) return true;

            return false;
        }
    }
}
