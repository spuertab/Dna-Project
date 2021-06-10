namespace Dna_Project.Core.Services
{
    using Interfaces;
    using System.Linq;

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
                int sequence = 0;
                int positionAux = 0;

                // Validar diagonal
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

                if (sequence == 3) equals++;

                letterPosition++;
            }

            if (equals > 1) return true;

            position += 1;

            if (dna.Length == position) return false;

            return IsMutantRecursive(dna, position, equals);
        }
    }
}
