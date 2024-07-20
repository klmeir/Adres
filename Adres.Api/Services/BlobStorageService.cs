using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.Extensions.Configuration;

namespace Adres.Api.Services
{
    public class BlobStorageService : IFileStorageService
    {
        //private readonly IConfiguration _configuration;
        //private readonly ILogger<BlobStorageService> _logger;
        //private string blobStorageconnection = string.Empty;
        //private string blobContainerName = string.Empty;// = "rijsatdemo";
        //public BlobStorageService(IConfiguration configuration, ILogger<BlobStorageService> logger)
        //{
        //    _configuration = configuration;
        //    _logger = logger;
        //    blobStorageconnection = _configuration["BlobStorageSettings:AzureStorageAccount"]!;
        //    blobContainerName = _configuration["BlobStorageSettings:ContainerName"]!;
        //}

        //public async Task<string> UploadAsync(Stream fileStream, string fileName, string contentType)
        //{
        //    try
        //    {
        //        var container = new BlobContainerClient(blobStorageconnection, blobContainerName);
        //        var createResponse = await container.CreateIfNotExistsAsync();
        //        if (createResponse != null && createResponse.GetRawResponse().Status == 201)
        //            await container.SetAccessPolicyAsync(PublicAccessType.Blob);
        //        var blob = container.GetBlobClient(fileName);
        //        await blob.DeleteIfExistsAsync(DeleteSnapshotsOption.IncludeSnapshots);
        //        await blob.UploadAsync(fileStream, new BlobHttpHeaders { ContentType = contentType });
        //        var urlString = blob.Uri.ToString();
        //        return urlString;
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger?.LogError(ex.ToString());
        //        throw;
        //    }
        //}

        private readonly BlobServiceClient _blobServiceClient;
        private readonly IConfiguration _configuration;

        public BlobStorageService(BlobServiceClient blobServiceClient, IConfiguration configuration)
        {
            _blobServiceClient = blobServiceClient ?? throw new ArgumentNullException(nameof(blobServiceClient));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public async Task<string> UploadAsync(Stream fileStream, string fileName, string contentType)
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient(_configuration["BlobStorageSettings:DefaultContainer"]);

            var createResponse = await containerClient.CreateIfNotExistsAsync();
            if (createResponse != null && createResponse.GetRawResponse().Status == 201)
                await containerClient.SetAccessPolicyAsync(PublicAccessType.Blob);

            // Generar un nombre único para el archivo en Blob Storage
            string blobName = $"{Guid.NewGuid()}{Path.GetExtension(fileName)}";

            var blobClient = containerClient.GetBlobClient(blobName);

            await blobClient.UploadAsync(fileStream, new BlobHttpHeaders { ContentType = contentType }).ConfigureAwait(false);

            return blobClient.Uri.ToString();
        }


        //private readonly IConfiguration _configuration;
        //private readonly BlobServiceClient _blobServiceClient;
        //private readonly string _containerName;

        //public BlobStorageService(IConfiguration configuration, BlobServiceClient blobServiceClient)
        //{
        //    _blobServiceClient = blobServiceClient ?? throw new ArgumentNullException(nameof(blobServiceClient));
        //    _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));             
        //    _containerName = _configuration["BlobStorageSettings:ContainerName"]!;
        //}

        //public async Task<string> UploadAsync(Stream fileStream, string fileName, string contentType)
        //{
        //    // Obtener referencia al contenedor de blobs
        //    var containerClient = _blobServiceClient.GetBlobContainerClient(_containerName);
        //    await containerClient.CreateIfNotExistsAsync();

        //    // Generar un nombre único para el archivo en Blob Storage
        //    string blobName = $"{Guid.NewGuid()}{Path.GetExtension(fileName)}";

        //    // Subir el archivo al contenedor de blobs
        //    var blobClient = containerClient.GetBlobClient(blobName);
        //    //using (var stream = file.OpenReadStream())
        //    //{
        //        await blobClient.UploadAsync(fileStream, true);
        //    //}

        //    // Devolver la URL del archivo subido
        //    return blobClient.Uri.ToString();
        //}
    }
}
