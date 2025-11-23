namespace Frontend.Models
{
    public class YieldInput
    {
        public string Region { get; set; }
        public string Soil_Type { get; set; }
        public string Crop { get; set; }
        public int Rainfall_mm { get; set; }
        public int Temperature_Celsius { get; set; }
        public bool Fertilizer_Used { get; set; }
        public bool Irrigation_Used { get; set; }
        public string Weather_Condition { get; set; }
        public int Days_to_Harvest { get; set; }
    }

}
