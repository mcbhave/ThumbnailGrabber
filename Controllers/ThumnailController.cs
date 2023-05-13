using Microsoft.AspNetCore.Mvc;
using ThumbnailGrabber.Models;
using Microsoft.AspNetCore.Hosting;
using System.Diagnostics;
using ThumbnailGrabber.Services;
using ThumbnailGrabber.Validation;
using ThumbnailGrabber.Authorization;
using Microsoft.AspNetCore.Http.Extensions;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ThumbnailGrabber.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ThumnailController : ControllerBase
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IThumbNailService _thumbNailService;

        public ThumnailController(IWebHostEnvironment webHostEnvironment, IThumbNailService thumbNailService)
        {
            _webHostEnvironment = webHostEnvironment;
            _thumbNailService = thumbNailService;
        }

        

        
        // POST api/<ThumnailController>
        [HttpPost]
        public  IActionResult Post([FromBody] VideoUrlModel urlModel)
        {
            try
            {
                var pth = Request.PathBase;
                var host=Request.Host.Value;
                if (!urlModel.CaptureOnDuration.IsValidTimeFormat())
                {
                    return BadRequest(string.Format("{0} is not valid time", urlModel.CaptureOnDuration));
                }
                UrlValidation validation = new UrlValidation();
                if (validation.ValidateUrl(urlModel.VideoUrl))
                {
                    string fileName = _thumbNailService.CreateThumbnail(urlModel, out byte[] fileBytes);
                   
                    if (System.IO.File.Exists(fileName))
                    {
                        // File.Delete(fileName);
                        fileName = Path.GetFileName(fileName);
                        //return File(fileBytes, "image/jpeg");
                        return Ok(new { 
                            message= $"{Request.Scheme}://{Request.Host.Value}/thumbs/{fileName}",
                            file=Convert.ToBase64String(fileBytes)
                        });                      
                    }
                    else
                    {
                        fileName = "Time is greater than video length";
                        return Ok(fileName);
                    }
                }
                else
                {
                    return BadRequest("Please provide valid URL");
                }
            }
            catch (Exception ae)
            {

                return BadRequest(ae.Message);
            }
            

        }

       

    }
}
