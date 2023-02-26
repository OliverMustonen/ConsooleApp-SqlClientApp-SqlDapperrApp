

namespace ConsoleApp
{
    // Access Modifiers - private, public, protected, internal
    internal class User
    {
        private string FirstName { get; set; }

        public string DisplayName => $"{FirstName}";
    }


    public static class StaticUser
    {
        private static string FirstName { get; set; }

        public static string DisplayName => $"{FirstName}";
    }
}
