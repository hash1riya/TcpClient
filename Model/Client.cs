using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace TcpClient.Model;
public class Client
{
    IPEndPoint endp;
    System.Net.Sockets.TcpClient tcpClient;
    NetworkStream? stream;
    public byte[] Buffer = new byte[1024];

    public Client(string ip, int port)
    {
        this.tcpClient = new();
        this.endp = new IPEndPoint(IPAddress.Parse(ip), port);
    }

    public bool IsConnected() => this.tcpClient.Client.Connected;
    public void Connect()
    {
        try
        {
            tcpClient.Connect(endp);
        }
        catch (SocketException ex)
        {
            Debug.WriteLine(ex.Message);
        }
    }

    public void Disconnect()
    {
        try
        {
            this.tcpClient.Client.Close();
        }
        catch (SocketException ex)
        {
            Debug.WriteLine(ex.Message);
        }
    }
    public async Task<int> Receive()
    {
        try
        {
            this.stream = this.tcpClient.GetStream();
            return await this.stream.ReadAsync(this.Buffer);
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return 0;
        }
    }
    public async void Send(string message) => await stream.WriteAsync(Encoding.UTF8.GetBytes(message));
}