namespace scpoc.data.CustomApi
{
    public class ControlSettings
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Path { get; set; }
        public ControlDataSources[] DataSources { get; set; }
    }
}