namespace Dna_Project.Core.Strategies
{
    using Dna_Project.Core.Enums;
    using Dna_Project.Core.Interfaces.Strategies;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class DnaStrategy : IDnaStrategy
    {
        private readonly IEnumerable<IDnaDirection> _dnaDirections;

        public DnaStrategy(IEnumerable<IDnaDirection> dnaDirections)
        {
            _dnaDirections = dnaDirections;
        }

        public bool ValidateDirection(string[] dna, int position, char letter, int letterPosition, DnaDirection dnaDirection)
        {
            return _dnaDirections
                .FirstOrDefault(x => x.Direction == dnaDirection)?.ValidateDirection(dna, position, letter, letterPosition) 
                ?? throw new ArgumentNullException(nameof(dnaDirection));
        }
    }
}
