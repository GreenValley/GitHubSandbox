namespace Slade.Applications.DuplexCommunication.Networking
{
    /// <summary>
    /// Represents the different type of application connection.
    /// </summary>
    public enum ApplicationType
    {
        /// <summary>
        /// Refers to an application that hosts the service.
        /// </summary>
        Server = 0,

        /// <summary>
        /// Refers to an application that acts as a client and connects to a hosted service.
        /// </summary>
        Client = 1
    }
}