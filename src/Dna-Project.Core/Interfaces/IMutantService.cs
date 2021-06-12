namespace Dna_Project.Core.Interfaces
{
    using System.Threading.Tasks;

    public interface IMutantService
    {
        Task<bool> IsMutantAsync(string[] dna);
    }
}
