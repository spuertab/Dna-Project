﻿namespace Dna_Project.Test.Core.Services
{
    using Dna_Project.Core.Services;
    using Dna_Project.Infra.Interface;
    using Dna_Project.Infra.Models;
    using Moq;
    using NUnit.Framework;
    using System.ComponentModel.DataAnnotations;
    using System.Threading.Tasks;

    [TestFixture]
    public class MutantServiceTest
    {
        MutantService mutantService;

        [OneTimeSetUp]
        public void SetUp()
        {
            Mock<IMutantRepository> mockMutantRepository = new();
            mockMutantRepository.Setup(st => st.AddItemAsync(It.IsAny<DnaModel>()));

            mutantService = new(mockMutantRepository.Object);
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
    }
}
