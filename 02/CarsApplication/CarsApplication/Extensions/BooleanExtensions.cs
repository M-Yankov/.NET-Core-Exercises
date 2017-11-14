namespace CarsApplication.Extensions
{
    public static class BooleanExtensions
    {
        public static string YesNo(this bool value) => value ? "Yes" : "No";
    }
}
