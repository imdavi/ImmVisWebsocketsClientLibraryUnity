using System.Text;
using WebSocketSharp;

public class ClientUrlBuilder
{
    private const string DEFAULT_SERVER_ADDRESS = "localhost";
    private const int DEFAULT_SERVER_PORT = 8888;

    public static string BuildServerUrl(string address, int port = DEFAULT_SERVER_PORT, string path = "")
    {
        StringBuilder stringBuilder = new StringBuilder("ws://");

        stringBuilder.Append(address.IsNullOrEmpty() ? DEFAULT_SERVER_ADDRESS : address);

        stringBuilder.Append($":{port}");

        if (!path.IsNullOrEmpty())
        {
            stringBuilder.Append($"/{path}");
        }

        return stringBuilder.ToString();
    }
}