namespace Dna_Project.Test.Core.Services
{
    using Dna_Project.Core.Config;
    using Dna_Project.Core.Entities;
    using Dna_Project.Core.Interfaces.Strategies;
    using Dna_Project.Core.Services;
    using Dna_Project.Core.Strategies;
    using Dna_Project.Core.Strategies.DnaDirections;
    using Dna_Project.Infra.Interface;
    using Dna_Project.Infra.Models;
    using Moq;
    using NUnit.Framework;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Threading.Tasks;

    [TestFixture]
    public class MutantServiceTest
    {
        MutantService mutantService;

        [OneTimeSetUp]
        public void SetUp()
        {
            DnaConfig dnaConfig = new()
            {
                MinEquals = 2,
                TotalToValidate = 4,
                Letters = new char[] { 'A', 'G', 'T', 'C' }
            };

            // Repository
            Mock<IMutantRepository> mockMutantRepository = new();
            mockMutantRepository
                .Setup(st => st.AddDnaAsync(It.IsAny<DnaModel>()));
            mockMutantRepository
                .Setup(st => st.GetCountDnaAsync(It.IsAny<string>()))
                .ReturnsAsync(new List<CountDnaModel>() {
                    new CountDnaModel
                    {
                        CountMutantDna = 40,
                        CountHumanDna = 100
                    }
                });

            // Strategy pettern
            List<IDnaDirection> dnaDirections = new()
            {
                new DDnaDirection(dnaConfig),
                new DIDnaDirection(dnaConfig),
                new DIRDnaDirection(dnaConfig),
                new RDnaDirection(dnaConfig)
            };

            DnaStrategy dnaStrategy = new(dnaDirections);

            mutantService = new(mockMutantRepository.Object, dnaStrategy, dnaConfig);
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
                    "AGAAAG",
                    "ACCCTA",
                    "TCACTG"
                } 
            },
        };

        [Test]
        [TestCaseSource(nameof(Dnas))]
        public async Task IsNotMutantAsync(string[] dna)
        {
            // Service
            Assert.IsFalse(await mutantService.IsMutantAsync(dna));
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
                }
            },
            new object[]
            {
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
        public async Task IsMutantAsync(string[] dna)
        {
            // Service
            Assert.IsTrue(await mutantService.IsMutantAsync(dna));
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
                }
            },
            new object[]
            {
                System.Array.Empty<string>()
            },
        };

        [Test]
        [TestCaseSource(nameof(Dnas4))]
        public void InvalidDna(string[] dna)
        {
            // Service
            Assert.ThrowsAsync<ValidationException>(async () => await mutantService.IsMutantAsync(dna));
        }

        [Test]
        public async Task GetStatsAsync()
        {
            // Service
            CountDnaEntity countDnaEntity = await mutantService.GetCountDnaAsync();

            Assert.AreEqual(0.4, countDnaEntity.Ratio);
        }
    }
}
