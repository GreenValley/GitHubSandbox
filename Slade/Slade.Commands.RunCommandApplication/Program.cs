namespace Slade.Commands.RunCommandApplication
{
    /// <summary>
    /// Contains the entry point for the console application.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// Main entry point for the application process execution.
        /// </summary>
        /// <param name="arguments">A collection of argument provided by a command-line interface.</param>
        public static void Main(string[] arguments)
        {
            var context = new RunCommandApplicationContext();
            var application = new RunCommandConsoleApplication(context, arguments);

            application.Run();
        }
    }
}