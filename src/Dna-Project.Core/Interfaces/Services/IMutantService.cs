namespace Dna_Project.Core.Interfaces.Services
{
    using Dna_Project.Core.Entities;
    using System.Threading.Tasks;

    public interface IMutantService
    {
        Task<bool> IsMutantAsync(string[] dna);

        Task<CountDnaEntity> GetCountDnaAsync();
    }
}
