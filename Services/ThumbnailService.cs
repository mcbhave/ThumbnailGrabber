using Microsoft.AspNetCore.Hosting;
using System.Diagnostics;
using ThumbnailGrabber.Models;

namespace ThumbnailGrabber.Services
{
    public class ThumbnailService : IThumbNailService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ThumbnailService(IWebHostEnvironment webHostEnvironment) { 
        _webHostEnvironment= webHostEnvironment;
        }
        public string CreateThumbnail(VideoUrlModel urlModel,out byte[] fileBytes)
        {

            string path = _webHostEnvironment.ContentRootPath;
            string ffmpegFolderPath = Path.Combine(path, "ffmpeg");
            string ffmpegpath = Path.Combine(ffmpegFolderPath, "ffmpeg.exe");          
            string thumbName = Path.Combine(path, "thumbs", Guid.NewGuid().ToString() + ".JPEG");
         
            DeleteAllFiles(Path.Combine(path, "thumbs"));

             ProcessStartInfo startInfo = new ProcessStartInfo()
            {
                FileName = ffmpegpath,
                //Arguments = $"-i {vidPath} -vf -q:v 2 -s {urlModel.Width}x{urlModel.Height}:-1 -an {thumbName} -ss 00:00:15 -vframes 1 -vcodec mjpeg",
                Arguments = $"-i {urlModel.VideoUrl} -s {urlModel.Dimentions} -ss {urlModel.Time} -vframes 1 -f image2 -vcodec mjpeg {thumbName}",
                CreateNoWindow = true,
                UseShellExecute = false,
                WorkingDirectory = ffmpegFolderPath
            };

            using (Process process = Process.Start(startInfo))
            {
                process.Start();
                process.WaitForExit();
                //process.Close();
            }
            if (File.Exists(thumbName))
            {
                fileBytes = System.IO.File.ReadAllBytes(thumbName);
            }
            else
            {
                throw new Exception("File does not exists");
            }
            
            return thumbName;
        }

        private void DeleteAllFiles(string path)
        {
            if (Directory.Exists(path))
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(path);
                foreach (FileInfo fi in directoryInfo.GetFiles())
                {
                    fi.Delete();
                }
            }
            else
            {
                Directory.CreateDirectory(path);
            }
        }
    }
}
