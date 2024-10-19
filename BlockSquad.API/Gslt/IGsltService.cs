namespace BlockSquad.Api.Gslt
{
    public interface IGsltService
    {
        Task<string?> GetGsltAsync(string memo);
    }
}
