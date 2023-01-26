using Web3Demo.Models.Contracts.NFT;

namespace Web3Demo.Models.Services
{
    public class NFTService
    {
        private readonly HttpClient _httpClient;

        public NFTService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<NFTGenerationResponse> GenerateNFT(NFTGenerationRequest request)
        {
            //todo:call NFT service
            return await Task.FromResult(new NFTGenerationResponse {
                //Id = Guid.NewGuid().ToString(),
                //BlockChainId = Guid.NewGuid().ToString(),
                //Address = Guid.NewGuid().ToString(),
                RawNFT = Guid.NewGuid().ToString(), Metadata = request.Metadata }
            );
        }
    }
}
