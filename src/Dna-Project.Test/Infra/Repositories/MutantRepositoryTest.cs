namespace Dna_Project.Test.Infra.Repositories
{
    using Dna_Project.Infra.Models;
    using Dna_Project.Infra.Repositories;
    using Microsoft.Azure.Cosmos;
    using Moq;
    using NUnit.Framework;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    [TestFixture]
    public class MutantRepositoryTest
    {
        MutantRepository _mutantRepository;

        [OneTimeSetUp]
        public void SetUp()
        {
            var container = new Mock<Container>();

            var countDnaModel = new List<CountDnaModel>()
            {
                new CountDnaModel()
            };

            var mockFeedResponse = new Mock<ItemResponse<DnaModel>>();
            container
            .Setup(_ => _.CreateItemAsync(
                It.IsAny<DnaModel>(),
                It.IsAny<PartitionKey>(),
                null,
                default)
            )
            .ReturnsAsync(mockFeedResponse.Object)
            .Verifiable();

            var feedResponseMock = new Mock<FeedResponse<CountDnaModel>>();
            feedResponseMock.Setup(x => x.GetEnumerator()).Returns(countDnaModel.GetEnumerator());

            var feedIteratorMock = new Mock<FeedIterator<CountDnaModel>>();
            feedIteratorMock.Setup(f => f.HasMoreResults).Returns(true);
            feedIteratorMock
                .Setup(f => f.ReadNextAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(feedResponseMock.Object)
                .Callback(() => feedIteratorMock
                    .Setup(f => f.HasMoreResults)
                    .Returns(false));

            container
                .Setup(c => c.GetItemQueryIterator<CountDnaModel>(
                    It.IsAny<QueryDefinition>(),
                    null,
                    null))
                .Returns(feedIteratorMock.Object);

            var client = new Mock<CosmosClient>();

            client.Setup(x => x.GetContainer(It.IsAny<string>(), It.IsAny<string>()))
                  .Returns(container.Object);

            _mutantRepository = new MutantRepository(client.Object, It.IsAny<string>(), It.IsAny<string>());
        }

        [Test]
        public void AddDnaAsyncTest()
        {
            DnaModel dnaModel = new()
            {
                Id = "123",
                Dna = "",
                IsMutant = false
            };

            Assert.DoesNotThrowAsync(async () => { await _mutantRepository.AddDnaAsync(dnaModel); });
        }

        [Test]
        public async Task GetCountDnaAsyncTest()
        {
            // Service
            var countDnas = await _mutantRepository.GetCountDnaAsync("SELECT SUM(c.isMutant ? 1 : 0) AS count_mutant_dna, SUM(c.isMutant = false ? 1 : 0) AS count_human_dna FROM c");

            Assert.IsNotNull(countDnas.FirstOrDefault());
        }
    }
}
