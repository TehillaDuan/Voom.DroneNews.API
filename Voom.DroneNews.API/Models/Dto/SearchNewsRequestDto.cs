namespace Voom.DroneNews.API.Models.Dto
{
    public class SearchNewsRequestDto
    {
        public string? Query { get; set; }
        public int Skip { get; set; } = 0;
        public int Take { get; set; } = 20;
    }
}
