namespace Dna_Project.Core.Interfaces
{
    using Dna_Project.Core.Dto;
    using System.Threading.Tasks;

    public interface IMutantService
    {
        Task<bool> IsMutantAsync(string[] dna);
        Task<CountDnaDto> GetCountDnaAsync();
    }
}
