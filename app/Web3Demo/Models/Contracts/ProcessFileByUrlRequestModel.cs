namespace Web3Demo.Models.UIContracts
{
    public class ProcessFileByUrlRequestModel
    {
        public string PostUrl { get; set; }
        public string Token { get; set; }
    }

    public class ProcessDocumentResponseModel
    {
        public Uri FileUrl { get; set; }
        public string RawNFT { get; set; }
    }
}
