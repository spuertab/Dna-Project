namespace Dna_Project.Core.Services
{
    using Interfaces;

    public class MutantService : IMutantService
    {
        public bool IsMutant(string[] dna)
        {
            return IsMutantRecursive(dna);
        }

        private bool IsMutantRecursive(string[] dna, int position = 0, int equals = 0)
        {
            int letterPosition = 0;

            foreach (char letter in dna[position].ToCharArray())
            {
                // Validar diagonal
                if (ValidateDiagonal(dna, position, letter, letterPosition)) equals++;
                // Validar derecha
                if (ValidateRight(dna, position, letter, letterPosition)) equals++;

                letterPosition++;
            }

            if (equals > 1) return true;

            position += 1;

            if (dna.Length == position) return false;

            return IsMutantRecursive(dna, position, equals);
        }

        // Validar diagonalmente
        private bool ValidateDiagonal(string[] dna, int position, char letter, int letterPosition)
        {
            int sequence = 0;
            int positionAux = 0;

            for (int i = 1 + letterPosition; i < 4 + letterPosition; i++)
            {
                positionAux++;
                if (position + positionAux >= dna.Length || i >= dna[position + positionAux].ToCharArray().Length)
                    break;
                if (letter == dna[position + positionAux].ToCharArray()[i])
                    sequence++;
                else
                    break;
            }

            if (sequence == 3) return true;

            return false;
        }

        // Validar a la derecha
        private bool ValidateRight(string[] dna, int position, char letter, int letterPosition)
        {
            int sequence = 0;

            for (int i = 1 + letterPosition; i < 4 + letterPosition; i++)
            {
                if (i >= dna[position].ToCharArray().Length)
                    break;
                if (letter == dna[position].ToCharArray()[i])
                    sequence++;
                else
                    break;
            }

            if (sequence == 3) return true;

            return false;
        }
    }
}
