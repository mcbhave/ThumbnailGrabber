using Microsoft.AspNetCore.Hosting;
using ThumbnailGrabber.Models;

namespace ThumbnailGrabber.Services
{
    public interface IThumbNailService
    {
        string CreateThumbnail(VideoUrlModel urlModel, out byte[] fileBytes);
    }
}
