using System.ComponentModel.DataAnnotations;
using ThumbnailGrabber.Validation;

namespace ThumbnailGrabber.Models
{
    public class VideoUrlModel
    {
        [Required(ErrorMessage = "Please provide video url")]
        public string VideoUrl { get; set; }
        [DisplayFormat(DataFormatString = "00:00:00")]
        public string CaptureOnDuration { get; set; }

        [Range(minimum:200,maximum: 650, ErrorMessage = "{0} should be between 200 and 650")]
        public int Width { get; set; }
        [Range(minimum: 200, maximum: 650, ErrorMessage = "{0} should be between 200 and 650")]
        public int Height { get; set; }
        public string Time
        {
            get
            {
                if (CaptureOnDuration.IsValidTimeFormat())
                    return TimeSpan.Parse(CaptureOnDuration).ToString("hh\\:mm\\:ss");
                else
                    return "00:00:01";
            }
        }
        public string Dimentions { 
            get 
            {
                string toReturn = string.Empty;
                if(Width<= 0)
                {
                    Width = 650;
                }
                if (Height<= 0)
                {
                    Height = 550;
                }
                return $"{Width}x{Height}";
            } 
        }

    }
}
