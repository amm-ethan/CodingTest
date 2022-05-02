namespace PWA.Utility
{
    public interface ApiRepository
    {
        Task<HttpResponseMessage> ImportData(MultipartFormDataContent dataContent);
    }
}
