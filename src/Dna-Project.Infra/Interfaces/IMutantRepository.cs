namespace Dna_Project.Infra.Interface
{
    using Dna_Project.Infra.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IMutantRepository
    {
        Task AddItemAsync(DnaModel item);
        Task<IEnumerable<DnaModel>> GetItemsAsync(string query);
    }
}
