namespace Dna_Project.Core.Dto
{
    using Newtonsoft.Json;
    using System;

    public class CountDnaDto
    {
        [JsonProperty("count_mutant_dna")]
        public int CountMutantDna { get; set; } = 0;

        [JsonProperty("count_human_dna")]
        public int CountHumanDna { get; set; } = 0;

        [JsonProperty("ratio")]
        public double Ratio { get; set; }

        public void CalculateRatio()
        {
            if (CountMutantDna == 0 && CountHumanDna == 0)
            {
                Ratio = 0.0;
            }
            else
            {
                if (CountHumanDna == 0)
                {
                    Ratio = 1;
                }
                else
                {
                    Ratio = Math.Round((double)CountMutantDna / CountHumanDna * 10) / 10.0;
                }
            }
        }
    }
}
