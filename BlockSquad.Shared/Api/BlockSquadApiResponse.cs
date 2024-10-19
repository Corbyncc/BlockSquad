namespace BlockSquad.Shared.Api
{
    public class BlockSquadApiResponse<T>
    {
        public bool IsSuccess { get; set; }
        public T? Data { get; set; }
        public string? ErrorMessage { get; set; }
    }
}
