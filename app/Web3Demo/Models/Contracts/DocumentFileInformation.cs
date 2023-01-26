using Web3Demo.Models.Contracts.GLO;

namespace Web3Demo.Models.Contracts
{
    public class DocumentFileInformation
    {
        public DocumentFileInformation(Publication publication)
        {
            FileUrl = publication.TechDocument.Url;
        }
        public Uri FileUrl { get; set; }
    }
}
