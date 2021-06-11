namespace Dna_Project.Test.Core.Services
{
    using Dna_Project.Core.Services;
    using NUnit.Framework;
    using System.ComponentModel.DataAnnotations;

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
            new object[] 
            { 
                new string[] 
                {
                    "ATGCGA",
                    "CAGTGC",
                    "TTATGT",
                    "AGAAGG",
                    "ACCCTA",
                    "TCACTG"
                } 
            },
        };

        [Test]
        [TestCaseSource(nameof(Dnas))]
        public void IsNotMutant(string[] dna)
        {
            // Service
            Assert.IsFalse(mutantService.IsMutant(dna));
        }

        private static readonly object[] Dnas2 =
        {
            new object[] 
            { 
                new string[] {
                    "ATGGGG",
                    "CAGTGC",
                    "ATAGGT",
                    "ATAAGG",
                    "CCTCTA",
                    "TCATTG"
                },
                new string[] {
                    "ATGGGG",
                    "AAGTGC",
                    "ATAGGT",
                    "ATAATG",
                    "CCTTTA",
                    "TCTTTG"
                }
            },
        };

        [Test]
        [TestCaseSource(nameof(Dnas2))]
        public void IsMutant(string[] dna)
        {
            // Service
            Assert.IsTrue(mutantService.IsMutant(dna));
        }

        private static readonly object[] Dnas4 =
        {
            new object[]
            {
                new string[] {
                    "ORLGGG",
                    "AAGTGC",
                    "ATAGKT",
                    "ATAATG",
                    "CCTTTA",
                    "TCTTTG"
                },
                System.Array.Empty<string>()
            },
        };

        [Test]
        [TestCaseSource(nameof(Dnas4))]
        public void InvalidDna(string[] dna)
        {
            // Service
            Assert.Throws<ValidationException>(() => mutantService.IsMutant(dna));
        }
    }
}
