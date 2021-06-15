namespace Dna_Project.Core.Services
{
    using Dna_Project.Core.Config;
    using Dna_Project.Core.Entities;
    using Dna_Project.Core.Enums;
    using Dna_Project.Core.Interfaces.Strategies;
    using Dna_Project.Infra.Interface;
    using Dna_Project.Infra.Models;
    using Interfaces.Services;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Threading.Tasks;

    public class MutantService : IMutantService
    {
        readonly IMutantRepository _mutantRepository;
        readonly IDnaStrategy _dnaStrategy;
        readonly DnaConfig _dnaConfig;

        public MutantService(IMutantRepository mutantRepository, IDnaStrategy dnaStrategy, DnaConfig dnaConfig)
        {
            _mutantRepository = mutantRepository;
            _dnaConfig = dnaConfig;
            _dnaStrategy = dnaStrategy;
        }

        public async Task<bool> IsMutantAsync(string[] dna)
        {
            if (dna == null || dna.Length < 1) throw new ValidationException("Wrong DNA");

            bool isMutant = IsMutantRecursive(dna);

            DnaModel dnaModel = new DnaModel()
            {
                Dna = string.Join("; ", dna),
                IsMutant = isMutant
            };

            await _mutantRepository.AddDnaAsync(dnaModel);

            return isMutant;
        }

        private bool IsMutantRecursive(string[] dna, int position = 0, int equals = 0)
        {
            foreach (var letter in dna[position].ToCharArray().Select((r, i) => new { Row = r, Index = i }))
            {
                if (!_dnaConfig.Letters.Contains(letter.Row)) throw new ValidationException("Wrong DNA");

                // Validar diagonal
                if (_dnaStrategy.ValidateDirection(dna, position, letter.Row, letter.Index, DnaDirection.Diagonal)) 
                    equals++;
                // Validar diagonalmente reversa
                if (_dnaStrategy.ValidateDirection(dna, position, letter.Row, letter.Index, DnaDirection.DiagonalReverse)) 
                    equals++;
                // Validar derecha
                if (_dnaStrategy.ValidateDirection(dna, position, letter.Row, letter.Index, DnaDirection.Right)) 
                    equals++;
                // Validar abajo
                if (_dnaStrategy.ValidateDirection(dna, position, letter.Row, letter.Index, DnaDirection.Down)) 
                    equals++;
            }

            if (equals >= _dnaConfig.MinEquals) return true;
            position += 1;
            if (dna.Length == position) return false;

            return IsMutantRecursive(dna, position, equals);
        }

        public async Task<CountDnaEntity> GetCountDnaAsync()
        {
            string query = "SELECT SUM(c.isMutant ? 1 : 0) AS count_mutant_dna, SUM(c.isMutant = false ? 1 : 0) AS count_human_dna FROM c";

            CountDnaModel countDnaModel = (await _mutantRepository.GetCountDnaAsync(query)).FirstOrDefault();

            CountDnaEntity countDnaDto = new CountDnaEntity
            {
                CountMutantDna = countDnaModel.CountMutantDna,
                CountHumanDna = countDnaModel.CountHumanDna
            };

            countDnaDto.CalculateRatio();

            return countDnaDto;
        }
    }
}
