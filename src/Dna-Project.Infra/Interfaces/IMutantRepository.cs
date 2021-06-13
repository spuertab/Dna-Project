namespace Dna_Project.Infra.Interface
{
    using Dna_Project.Infra.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IMutantRepository
    {
        Task AddDnaAsync(DnaModel item);
        Task<IEnumerable<CountDnaModel>> GetCountDnaAsync(string query);
    }
}
