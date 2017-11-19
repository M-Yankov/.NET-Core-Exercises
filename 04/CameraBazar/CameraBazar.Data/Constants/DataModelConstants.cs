namespace CameraBazar.Data.Constants
{
    public class DataModelConstants
    {
        public const int MaxQuantity = 100;
        public const int MinShutterSpeerdLowerBoundary = 0;
        public const int MinShutterSpeerdUpperBoundary = 100;
        public const int MaxShutterSpeerdLowerBoundary = 2000;
        public const int MaxShutterSpeerdUpperBoundary = 8000;
        public const int DescriptionMaxLength = 6000;
        public const int ResolutionMaxLength = 15;
        public const int MinISOSpeerdLowerBoundary = 50;
        public const int MinISOSpeerdUpperBoundary = 100;
        public const int MaxISOSpeerdLowerBoundary = 200;
        public const int MaxISOSpeerdUpperBoundary = 409600;
        public const string ImageUrlRegExValue = "^https?://";
    }
}
