namespace Weather.Api.Models
{
    public class Coordinates
    {
        public string Lat { get; set; }
        public string Lon { get; set; }

        public override string ToString()
        {
            return $"{Lat},{Lon}";
        }
    }
}