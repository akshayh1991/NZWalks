using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageRepository _imageRepository;

        public ImagesController(IImageRepository imageRepository)
        {
            _imageRepository = imageRepository;
        }
        [HttpPost]
        public async Task<IActionResult> Upload([FromForm] ImageUploadDto imageUploadDto)
        {
            ValidateImage(imageUploadDto);
            if (ModelState.IsValid)
            {
                //Convert Dto to Domain model
                var image = new Image
                {
                    File = imageUploadDto.File,
                    FileExtension = Path.GetExtension(imageUploadDto.File.FileName),
                    FileName=imageUploadDto.FileName,
                    FileSizeInBytes=imageUploadDto.File.Length,
                    FileDescription=imageUploadDto.FileDescription
                };
                await _imageRepository.UploadImage(image);
                return Ok(image);
            }
            return BadRequest(ModelState);
        }

        private void ValidateImage(ImageUploadDto imageUploadDto)
        {
            var allowedExtension = new string[] { ".jpg", ".jpeg", ".png" };
            if (!allowedExtension.Contains(Path.GetExtension(imageUploadDto.File.FileName)))
            {
                ModelState.AddModelError("file", "Unsupported file extension");
            }
            if (imageUploadDto.File.Length>10485760)
            {
                ModelState.AddModelError("file", "File size is more than 10MB");
            }
        }
    }
}
