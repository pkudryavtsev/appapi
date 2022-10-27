using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Mvc;

namespace App.Api.Controllers
{
    [ApiController]
    [Route("/files")]
    public class FilesController : ControllerBase
    {
        private string _storageContainerName;
        private string _storageConnectionString;

        public FilesController(IConfiguration config)
        {
            _storageConnectionString = config.GetValue<string>("BlobConnectionString");
            _storageContainerName = config.GetValue<string>("BlobContainerName");
        }

        [HttpGet()]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public IActionResult Files()
        {
            List<string> result;
            var container = new BlobContainerClient(_storageConnectionString, _storageContainerName);
            try {
                result = container.GetBlobs().Select(b => b.Name).ToList();
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }

            return Ok(result);
        }
    }
}