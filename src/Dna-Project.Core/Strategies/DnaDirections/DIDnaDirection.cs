namespace Dna_Project.Core.Strategies.DnaDirections
{
    using Dna_Project.Core.Config;
    using Dna_Project.Core.Enums;
    using Dna_Project.Core.Interfaces.Strategies;

    public class DIDnaDirection : IDnaDirection
    {
        readonly DnaConfig _dnaConfig;

        public DIDnaDirection(DnaConfig dnaConfig)
        {
            _dnaConfig = dnaConfig;
        }

        public DnaDirection Direction => DnaDirection.Diagonal;

        public bool ValidateDirection(string[] dna, int position, char letter, int letterPosition)
        {
            int sequence = 1;
            int positionAux = 0;

            for (int i = 1 + letterPosition; i < _dnaConfig.TotalToValidate + letterPosition; i++)
            {
                positionAux++;
                if (position + positionAux < dna.Length &&
                    i < dna[position + positionAux].ToCharArray().Length &&
                    letter == dna[position + positionAux].ToCharArray()[i])
                    sequence++;
                else
                    break;
            }

            if (sequence == _dnaConfig.TotalToValidate) return true;

            return false;
        }
    }
}
