﻿namespace Dna_Project.Core.Services
{
    using Dna_Project.Infra.Interface;
    using Dna_Project.Infra.Models;
    using Interfaces;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    public class MutantService : IMutantService
    {
        readonly int minEquals = 2;
        readonly int totalToValidate = 4;
        readonly char[] letters = new char[] { 'A', 'G', 'T', 'C' };
        readonly IMutantRepository _mutantService;

        public MutantService(IMutantRepository mutantService)
        {
            _mutantService = mutantService;
        }

        public bool IsMutant(string[] dna)
        {
            if (dna == null || dna.Length < 1) throw new ValidationException("Wrong DNA");

            bool isMutant = IsMutantRecursive(dna);

            DnaModel dnaModel = new DnaModel()
            {
                Dna = string.Join("; ", dna),
                IsMutant = isMutant
            };

            _mutantService.SaveDan(dnaModel);

            return isMutant;
        }

        private bool IsMutantRecursive(string[] dna, int position = 0, int equals = 0)
        {
            int letterPosition = 0;

            foreach (char letter in dna[position].ToCharArray())
            {
                if (!letters.Contains(letter)) throw new ValidationException("Wrong DNA");

                // Validar diagonal
                if (ValidateDiagonal(dna, position, letter, letterPosition)) equals++;
                // Validar diagonalmente reversa
                if (ValidateReverseDiagonal(dna, position, letter, letterPosition)) equals++;
                // Validar derecha
                if (ValidateRight(dna, position, letter, letterPosition)) equals++;
                // Validar abajo
                if (ValidateDown(dna, position, letter, letterPosition)) equals++;

                letterPosition++;
            }

            if (equals >= minEquals) return true;
            position += 1;
            if (dna.Length == position) return false;

            return IsMutantRecursive(dna, position, equals);
        }

        // Validar diagonalmente
        private bool ValidateDiagonal(string[] dna, int position, char letter, int letterPosition)
        {
            int sequence = 1;
            int positionAux = 0;

            for (int i = 1 + letterPosition; i < totalToValidate + letterPosition; i++)
            {
                positionAux++;
                if (position + positionAux < dna.Length && 
                    i < dna[position + positionAux].ToCharArray().Length && 
                    letter == dna[position + positionAux].ToCharArray()[i])
                    sequence++;
                else
                    break;
            }

            if (sequence == totalToValidate) return true;

            return false;
        }

        // Validar diagonalmente reversa
        private bool ValidateReverseDiagonal(string[] dna, int position, char letter, int letterPosition)
        {
            int sequence = 1;
            int positionAux = 0;

            for (int i = letterPosition; i > (letterPosition - totalToValidate < 0 ? totalToValidate : letterPosition - totalToValidate); i--)
            {
                positionAux++;
                if (position + positionAux < dna.Length &&
                    i < dna[position + positionAux].ToCharArray().Length &&
                    letter == dna[position + positionAux].ToCharArray()[i])
                    sequence++;
                else
                    break;
            }

            if (sequence == totalToValidate) return true;

            return false;
        }

        // Validar a la derecha
        private bool ValidateRight(string[] dna, int position, char letter, int letterPosition)
        {
            int sequence = 1;

            for (int i = 1 + letterPosition; i < totalToValidate + letterPosition; i++)
            {
                if (i < dna[position].ToCharArray().Length && letter == dna[position].ToCharArray()[i])
                    sequence++;
                else
                    break;
            }

            if (sequence == totalToValidate) return true;

            return false;
        }

        // Validar a la derecha
        private bool ValidateDown(string[] dna, int position, char letter, int letterPosition)
        {
            int sequence = 1;

            for (int i = 1 + position; i < totalToValidate + position; i++)
            {
                if (i < dna.Length && letter == dna[i].ToCharArray()[letterPosition])
                    sequence++;
                else
                    break;
            }

            if (sequence == totalToValidate) return true;

            return false;
        }
    }
}
