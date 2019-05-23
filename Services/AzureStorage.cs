using System;
using System.IO;
using Tambaqui.Models;
using Tambaqui.Interfaces;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.WindowsAzure.Storage;
using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage.Blob;

namespace INATS.Services
{
    public class AzureStorage : IStorage
    {           
        public string connectionString { get; private set; }            
        
        public AzureStorage([FromServices] IConfiguration configuration)
        {
            connectionString = configuration["AzureStorageConnectionString"];            
        }

        public CloudBlobClient IniciarBlobClient()
        {
            var conta = CloudStorageAccount.Parse(connectionString);
            var blobClient = conta.CreateCloudBlobClient();
            return blobClient;
        }

        public async Task Upload(string localizador, IFormFile arquivo)
        {
            //Esse client é quem conecta com o azure
            var blobClient = IniciarBlobClient(); 

            //Esse container é a referência da 'pasta' lá no azure. É legal colocar todo arquivo numa pasta diferente pra não ter conflito de nome. O valor salvor no 
            //identificador do arquivo é sempre o caminho pra encontrar o arquivo. 
            var container = blobClient.GetContainerReference(localizador); 

            await container.CreateIfNotExistsAsync();

            //Esse blockblob é a referencia do arquivo
            var blockBlob = container.GetBlockBlobReference(arquivo.FileName);

            blockBlob.Properties.ContentType = arquivo.ContentType;
            
            //Aqui eu envio o arquivo pro azure
            using (MemoryStream stream = new MemoryStream())
            {                
                await arquivo.CopyToAsync(stream);
                stream.Seek(0, SeekOrigin.Begin);
                await blockBlob.UploadFromStreamAsync(stream);
            }                        
        }      

        public async Task<byte[]> Download(string localizador)
        {
            var blobClient = IniciarBlobClient();

            var container = blobClient.GetContainerReference(localizador);

            var blockBlob = await container.GetBlobReferenceFromServerAsync("nome do arquivo");

            var bytes = new byte[blockBlob.Properties.Length];

            await blockBlob.DownloadToByteArrayAsync(bytes, 0);

            return bytes;            
        }

        public async Task Excluir(string localizador)
        {
            var blobClient = IniciarBlobClient();
            var container = blobClient.GetContainerReference(localizador);
            await container.DeleteIfExistsAsync();
        }

        public Task SubstituirAnexo(Anexo anexo, IFormFile arquivoNovo)
        {
            throw new NotImplementedException();
        }
    }
}
