namespace Dna_Project.Test.Api.Controllers
{
    using Dna_Project.Api.Controllers;
    using Dna_Project.Core.Entities;
    using Dna_Project.Core.Interfaces.Services;
    using Microsoft.AspNetCore.Mvc;
    using Moq;
    using NUnit.Framework;

    public class MutantControllerTest
    {
        [Test]
        public void IsMutantAsyncTest()
        {
            // Service
            Mock<IMutantService> mockMutantService = new();
            mockMutantService
                .Setup(st => st.IsMutantAsync(It.IsAny<string[]>()))
                .ReturnsAsync(true);

            MutantController mutantController = new(mockMutantService.Object);

            var actionResult = mutantController.MutantAsync(new DnaEntity() { Dna = null });
            var result = actionResult.Result as OkObjectResult;

            //Assert
            Assert.AreEqual(200, result.StatusCode);
        }

        [Test]
        public void IsNotMutantAsyncTest()
        {
            // Service
            Mock<IMutantService> mockMutantService = new();
            mockMutantService
                .Setup(st => st.IsMutantAsync(It.IsAny<string[]>()))
                .ReturnsAsync(false);

            MutantController mutantController = new(mockMutantService.Object);

            var actionResult = mutantController.MutantAsync(new DnaEntity() { Dna = null });
            var result = actionResult.Result as StatusCodeResult;

            //Assert
            Assert.AreEqual(403, result.StatusCode);
        }

        [Test]
        public void GetStatsTestAsync()
        {
            // Service
            Mock<IMutantService> mockMutantService = new();
            mockMutantService
                .Setup(st => st.GetCountDnaAsync())
                .ReturnsAsync(new CountDnaEntity() { CountHumanDna = 40, CountMutantDna = 100, Ratio = 0.4 });

            MutantController mutantController = new(mockMutantService.Object);

            var actionResult = mutantController.GetStats();
            var result = actionResult.Result as OkObjectResult;

            //Assert
            Assert.IsInstanceOf<OkObjectResult>(actionResult.Result);
            Assert.NotNull(result.Value);
        }
    }
}
