using iText.Kernel.Pdf;
using iText.StyledXmlParser.Jsoup.Nodes;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Newtonsoft.Json;
using Org.BouncyCastle.Asn1.Cms;
using Org.BouncyCastle.Asn1.Ocsp;
using System.Globalization;
using System.Net.Http;
using System.Text;
using System.Web;
using Web3Demo.Models.Contracts;
using Web3Demo.Models.Contracts.GLO;
using Web3Demo.Models.Contracts.NFT;
using Web3Demo.Models.UIContracts;

namespace Web3Demo.Models.Services
{
    public class DocumentsService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly NFTService _nftService;

        public DocumentsService(IHttpClientFactory httpClientFactory, NFTService nftService)
        {
            _httpClientFactory = httpClientFactory;
            _nftService = nftService;
        }

        public async Task<ProcessDocumentResponseModel> ProcessFileByUrl(string authToken, ProcessFileByUrlRequestModel request)
        {
            Publication publication = await GetPublication(authToken, request);
            var fileData = await DownloadFile(authToken, publication);

            var nftGenerationRequest = new NFTGenerationRequest
            {
                Data = fileData,
                Metadata = new Dictionary<string, string>
                {
                    { "post_url",  request.PostUrl }
                }
            };

            var nftResponse = await _nftService.GenerateNFT(nftGenerationRequest);
            if (nftResponse.Metadata == null)
            {
                nftResponse.Metadata = new Dictionary<string, string>();
            }

            nftResponse.Metadata["NFT"] = nftResponse.RawNFT;

            var updatedFile = AppendNFT(fileData, nftResponse.Metadata);
            var uploadFileInfo = await UploadFile(authToken,  updatedFile);

            publication.TechDocument.Url = uploadFileInfo.AuthenticatedUrl;

            var updatedPublication = new UpdatedPublication(publication, uploadFileInfo);

            var newPublication = await UpdatePublication(authToken, request, updatedPublication);

            return new ProcessDocumentResponseModel
            {
                RawNFT = nftResponse.RawNFT,
                FileUrl = newPublication.TechDocument.Url,
            };
        }

        private byte[] AppendNFT(byte[] fileData, Dictionary<string, string> metadata)
        {
            using var source = new MemoryStream(fileData);
            using var destination = new MemoryStream();

            PdfReader reader = new PdfReader(source);
            PdfWriter writer = new PdfWriter(destination);
            PdfDocument pdfDoc = new PdfDocument(reader, writer);
            PdfDictionary catalog = pdfDoc.GetTrailer();
            PdfDictionary map = catalog.GetAsDictionary(PdfName.Info);

            foreach (var item in metadata)
            {
                map.Put(new PdfName(item.Key), new PdfString(item.Value));
            }

            map.Put(PdfName.Keywords, new PdfString("NFT"));
            pdfDoc.Close();

            var updatedFileData = destination.ToArray();
            return updatedFileData;
        }

        private async Task<Publication> GetPublication(string authToken, ProcessFileByUrlRequestModel request)
        {
            using var client = _httpClientFactory.CreateClient("GLO");
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", authToken);

            string requstUrl = GetRestDocumentModelUrl(request);

            var response = await client.GetAsync(requstUrl);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Publication>(content);
        }

        private async Task<Publication> UpdatePublication(string authToken, ProcessFileByUrlRequestModel request, UpdatedPublication publication)
        {
            using var client = _httpClientFactory.CreateClient("GLO");
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", authToken);

            string requstUrl = GetRestDocumentModelUrl(request);

            var response = await client.PutAsync(requstUrl, new StringContent(JsonConvert.SerializeObject(publication), Encoding.UTF8,  "application/json"));
            response.EnsureSuccessStatusCode();//title, description, types, document, practices, authors, locations, languages, creation_date

            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Publication>(content);
        }

        private static string GetRestDocumentModelUrl(ProcessFileByUrlRequestModel request)
        {
            var url = new Uri(request.PostUrl);
            var id = url.Segments.Last();
            var requstUrl = $"/v1/tech-documents/{id}";
            return requstUrl;
        }

        private async Task<byte[]> DownloadFile(string authToken, Publication publication)
        {
            var urlEncodedUrl = HttpUtility.UrlEncode(publication.TechDocument.Url.ToString());
            var requestUrl = $"v1/cloudstorage/signedUrl?url={urlEncodedUrl}";

            using var client = _httpClientFactory.CreateClient("glo-storage");
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", authToken);

            var response = await client.GetAsync(requestUrl);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var signedUrlInfo = JsonConvert.DeserializeObject<SignedUrlInfo>(content);

            var storageClient = _httpClientFactory.CreateClient();
            var storageResponse = await storageClient.GetAsync(signedUrlInfo.SignedUrl);
            storageResponse.EnsureSuccessStatusCode();
            return await storageResponse.Content.ReadAsByteArrayAsync();
        }

        private async Task<UploadedFileInfo> UploadFile(string authToken, byte[] fileData)
        {
            using var client = _httpClientFactory.CreateClient("glo-storage");
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", authToken);

            var formContent = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("file_key", "nugget-techDocument-doc")
            });

            var stringContent = new StringContent("nugget-techDocument-doc");

            using var content = new MultipartFormDataContent("Upload----" + DateTime.Now.ToString(CultureInfo.InvariantCulture));

            var streamContent = new ByteArrayContent(fileData);
            streamContent.Headers.Add("Content-Disposition", "form-data; name=\"file\"; filename=\"" + "file.pdf" + "\"");
            // content.Add(formContent);
            content.Add(streamContent);
            content.Add(stringContent, "file_key");

            var response = await client.PostAsync("v1/cloudstorage/upload", content);
            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<UploadedFileInfo>(responseContent);
        }
    }
}
