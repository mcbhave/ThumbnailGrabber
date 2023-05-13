namespace ThumbnailGrabber.Validation
{
    public class UrlValidation
    {
        /// <summary>
        /// Validates a URL.
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public bool ValidateUrl(string url)
        {
            Uri? validatedUri;

            if (Uri.TryCreate(url, UriKind.Absolute, out validatedUri)) //.NET URI validation.
            {
                //If true: validatedUri contains a valid Uri. Check for the scheme in addition.
                return (validatedUri.Scheme == Uri.UriSchemeHttp || validatedUri.Scheme == Uri.UriSchemeHttps);
            }
            return false;
        }
    }
}
