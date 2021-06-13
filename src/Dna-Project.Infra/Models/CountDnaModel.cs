namespace Dna_Project.Infra.Models
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;

    public class CountDnaModel
    {
        [JsonProperty("count_mutant_dna")]
        public int CountMutantDna { get; set; }

        [JsonProperty("count_human_dna")]
        public int CountHumanDna { get; set; }
    }
}
