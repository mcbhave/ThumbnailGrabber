namespace ThumbnailGrabber.Validation
{
    public static class DateTimeExtensions
    {
        public static bool IsValidTimeFormat(this string input)
        {
            bool isTime= TimeSpan.TryParse(input, out var dummyOutput);
            TimeSpan t=dummyOutput;
            return isTime;
        }
    }
}
