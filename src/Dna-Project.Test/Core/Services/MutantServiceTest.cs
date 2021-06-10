namespace Dna_Project.Test.Core.Services
{
    using Dna_Project.Core.Services;
    using NUnit.Framework;

    [TestFixture]
    public class MutantServiceTest
    {
        readonly MutantService mutantService;

        public MutantServiceTest()
        {
            mutantService = new();
        }

        private static readonly object[] Dnas =
        {
            new object[] { new string[] {"ATGCGA","CAGTGC","TTATGT","AGAAGG","CCCCTA","TCACTG"} },
        };

        [Test]
        [TestCaseSource(nameof(Dnas))]
        public void IsNotMutant(string[] dna)
        {
            // Service
            Assert.IsFalse(mutantService.IsMutant(dna));
        }
    }
}
