using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TmpConsole.Services.TelnetClient
{
    public class TelnetClient
    {
        private TcpClient? _client;
        private NetworkStream? _stream;

        public async Task ConnectAsync(string hostname, int port)
        {
            _client = new TcpClient();
            await _client.ConnectAsync(hostname, port);
            _stream = _client.GetStream();
            Console.WriteLine("Подключение серверу по протоколу Telnet....");
        }

        public async Task SendAsync(string message)
        {
            byte[] buffer = Encoding.ASCII.GetBytes(message);
            if (_stream!=null) 
                await _stream.WriteAsync(buffer, 0, buffer.Length);
            Console.WriteLine("Отправлено:" + message);
        }

        public async Task<string> ReceivedAsync()
        {
            byte[] buffer = new byte[1024];
            int bytesRead = 0;
            if (_stream!= null) bytesRead= await _stream.ReadAsync(buffer, 0, buffer.Length);
            string response = Encoding.ASCII.GetString(buffer, 0, bytesRead);
            Console.WriteLine("Получено:" + response);
            return response;
        }

        public void Disconnect()
        {
            _stream?.Close();
            _client?.Close();
            Console.WriteLine("Отключение от сервера.....");
        }
    }
}
