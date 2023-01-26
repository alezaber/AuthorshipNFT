namespace Web3Demo.Models.Contracts.NFT
{
    public class NFTGenerationRequest
    {
        public Dictionary<string, string> Metadata { get; set; }
        public byte[] Data { get; set; }
    }


}
