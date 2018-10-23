using System;
using System.IO;
using Tambaqui.Models;
using Tambaqui.Interfaces;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.Extensions.Configuration;

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

        public async Task Upload(IAnexo anexo, IFormFile arquivo)
        {
            //Esse client é quem conecta com o azure
            var blobClient = IniciarBlobClient(); 

            //Esse container é a referência da 'pasta' lá no azure. É legal colocar todo arquivo numa pasta diferente pra não ter conflito de nome. O valor salvor no 
            //identificador do arquivo é sempre o caminho pra encontrar o arquivo. 
            var container = blobClient.GetContainerReference(anexo.Localizacao); 

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

        public async Task<byte[]> Download(IAnexo anexo)
        {
            var blobClient = IniciarBlobClient();

            var container = blobClient.GetContainerReference(anexo.Localizacao);

            var blockBlob = await container.GetBlobReferenceFromServerAsync(anexo.Nome);

            var bytes = new byte[blockBlob.Properties.Length];

            await blockBlob.DownloadToByteArrayAsync(bytes, 0);

            return bytes;            
        }

        public async Task Excluir(IAnexo anexo)
        {
            var blobClient = IniciarBlobClient();
            var container = blobClient.GetContainerReference(anexo.Localizacao);
            await container.DeleteIfExistsAsync();
        }              
    }
}
