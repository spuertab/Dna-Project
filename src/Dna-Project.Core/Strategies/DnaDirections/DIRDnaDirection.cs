namespace Dna_Project.Core.Strategies.DnaDirections
{
    using Dna_Project.Core.Config;
    using Dna_Project.Core.Enums;
    using Dna_Project.Core.Interfaces.Strategies;

    public class DIRDnaDirection : IDnaDirection
    {
        readonly DnaConfig _dnaConfig;

        public DIRDnaDirection(DnaConfig dnaConfig)
        {
            _dnaConfig = dnaConfig;
        }

        public DnaDirection Direction => DnaDirection.DiagonalReverse;

        public bool ValidateDirection(string[] dna, int position, char letter, int letterPosition)
        {
            int sequence = 1;
            int positionAux = 0;
            int totalToValidateAux = _dnaConfig.TotalToValidate - 1;

            for (int i = letterPosition; i > (letterPosition - totalToValidateAux < 0 ? totalToValidateAux : letterPosition - totalToValidateAux); i--)
            {
                positionAux++;
                if (position + positionAux < dna.Length &&
                    i < dna[position + positionAux].ToCharArray().Length &&
                    letter == dna[position + positionAux].ToCharArray()[i - 1])
                    sequence++;
                else
                    break;
            }

            if (sequence == _dnaConfig.TotalToValidate) return true;

            return false;
        }
    }
}
