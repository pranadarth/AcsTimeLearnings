using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace PracticeApp
{
   
        public class Breafast
        {
            [JsonProperty("Main")]
            public string Main { get; set; }

            [JsonProperty("MainMaxSelection")]
            public int MainMaxSelection { get; set; }
        }

        public class Lunch
        {
            [JsonProperty("Main")]
            public string Main { get; set; }

            [JsonProperty("MainMaxSelection")]
            public int MainMaxSelection { get; set; }

            [JsonProperty("Side")]
            public string Side { get; set; }

            [JsonProperty("SideMaxSelection")]
            public int SideMaxSelection { get; set; }

            [JsonProperty("Sweet")]
            public string Sweet { get; set; }

            [JsonProperty("SweetMaxSelection")]
            public int SweetMaxSelection { get; set; }
        }

        public class MealOptions
        {
            [JsonProperty("Main")]
            public List<int> Main { get; set; }

            [JsonProperty("Side")]
            public List<string> Side { get; set; }

            [JsonProperty("Sweet ")]
            public List<string> Sweet { get; set; }
        }

        public class Tea
        {
            [JsonProperty("Main")]
            public string Main { get; set; }

            [JsonProperty("MainMaxSelection")]
            public int MainMaxSelection { get; set; }

            [JsonProperty("Side")]
            public string Side { get; set; }

            [JsonProperty("SideMaxSelection")]
            public int SideMaxSelection { get; set; }

            [JsonProperty("Sweet")]
            public string Sweet { get; set; }

            [JsonProperty("SweetMaxSelection")]
            public int SweetMaxSelection { get; set; }
        }

        public class MealSelection
        {
            [JsonProperty("Breafast")]
            public Breafast Breafast { get; set; }

            [JsonProperty("Lunch")]
            public Lunch Lunch { get; set; }

            [JsonProperty("Tea")]
            public Tea Tea { get; set; }
        }

        public class Prison
        {
            [JsonProperty("Name")]
            public string Name { get; set; }

            [JsonProperty("WeekCycle")]
            public int WeekCycle { get; set; }

            [JsonProperty("WeekStartDay")]
            public string WeekStartDay { get; set; }

            [JsonProperty("InputDirectory")]
            public string InputDirectory { get; set; }
        }

        public class EditingRules
        {
            [JsonProperty("Prison")]
            public Prison Prison { get; set; }

            [JsonProperty("MealSelection")]
            public MealSelection MealSelection { get; set; }

            [JsonProperty("MealOptions")]
            public MealOptions MealOptions { get; set; }
        }

       

    
}
