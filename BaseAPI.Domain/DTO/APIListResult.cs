namespace BaseAPI.Domain.DTO
{
    public class APIListResult
    {
        public List<Result> Results { get; set; }
    }

    public class Result
    {
        public string Name { get; set; }
        public string Url { get; set; }
    }
}
