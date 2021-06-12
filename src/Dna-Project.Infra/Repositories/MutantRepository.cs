namespace Dna_Project.Infra.Repositories
{
    using Dna_Project.Infra.Interface;
    using Dna_Project.Infra.Models;
    using Microsoft.Azure.Cosmos;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class MutantRepository : IMutantRepository
    {
        private Container _container;

        public MutantRepository(
            CosmosClient dbClient,
            string databaseName,
            string containerName)
        {
            _container = dbClient.GetContainer(databaseName, containerName);
        }

        public async Task AddItemAsync(DnaModel item)
        {
            await _container.CreateItemAsync(item, new PartitionKey(item.Id));
        }

        public async Task<IEnumerable<DnaModel>> GetItemsAsync(string queryString)
        {
            var query = _container.GetItemQueryIterator<DnaModel>(new QueryDefinition(queryString));

            List<DnaModel> results = new List<DnaModel>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();

                results.AddRange(response.ToList());
            }

            return results;
        }
    }
}
