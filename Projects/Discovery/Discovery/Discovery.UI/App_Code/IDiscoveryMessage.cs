using System.Web;

namespace Discovery.UI.Web
{
    public enum DiscoveryMessageType
    {
        Success,
        Information,
        Warning,
        Error
    }

    public interface IDiscoveryMessage
    {
        // Display a message using the information type
        void DisplayMessage(string message);

        // Display a messsage using the specified message type
        void DisplayMessage(string message, DiscoveryMessageType type);

        // Clear the messages
        void ClearMessages();
    }
}