using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TmpConsole.Services.Telnet
{
    internal class TelnetServer
    {
        private TcpListener _listener;
        private bool _isRunning;

        // Запуск сервера
        public async Task StartAsync(string ipAddress, int port)
        {
            _listener = new TcpListener(IPAddress.Parse(ipAddress), port);
            _listener.Start();
            _isRunning= true;
            Console.WriteLine($"Telnet сервер запущен {ipAddress}:{port}");
            while (_isRunning == true)
            {
                TcpClient client = await _listener.AcceptTcpClientAsync();
                Console.WriteLine("Client connected");
                // _ = HandleClientAsync(client);
                _ =HandleClientAsync(client);
            }
        }

        // Остановка сервера
        public void Stop()
        {
            _isRunning = false;
            _listener.Stop();
            Console.WriteLine("Telnet сервер остановлен");
        }

        private async Task HandleClientAsync(TcpClient client)
        {
            byte[] buffer = new byte[1024];
            StringBuilder inputBuffer=new StringBuilder();

            using (NetworkStream stream = client.GetStream())
            {
                while (client.Connected)
                {
                    try
                    {
                        int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
                        if (bytesRead == 0) break;
                        string receivedData = Encoding.ASCII.GetString(buffer, 0, bytesRead);
                        inputBuffer.Append(receivedData);
                        if (receivedData.Contains("\r\n") ||  receivedData.Contains("\n"))
                        {
                            string command = inputBuffer.ToString().Trim();
                            Console.WriteLine("Received:" + command);

                            string response = ProcessCommand(command);
                            

                            byte[] responseBytes = Encoding.ASCII.GetBytes(response + "\r\n");
                            await stream.WriteAsync(responseBytes, 0, responseBytes.Length);

                            inputBuffer.Clear();

                            if (command.Equals("exit"))
                            {
                                _isRunning = false;
                                Console.WriteLine("Server stoped....");
                                //stream.Close();
                                break;
                                //client.Close();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error:" + ex.Message);
                        break;
                    }

                }
                _isRunning = false;
                client.Close();
            }
            _isRunning = false;
        }

        private string ProcessCommand(string command)
        {
            switch(command.ToLower())
            {
                case "hello":
                    return "Hello Client\r\n";

                case "time":
                    return "Current time:" + DateTime.Now.ToString("HH:mm:ss");

                case "exit":
                    _isRunning = false;
                    return "Goodbye!\r\n";

                case "close":
                    _isRunning = false;
                    return "Close\r\n";

                case "help":
                    return "******** Telnet Server ******\r\n" +
                           "type\r\n"+
                           "hello - get Hello from server\r\n"+
                           "time - get current time\r\n"+
                           "get all clients - get all client from Database \r\n"+
                           "exit - stop connection to server \r\n";

                default:
                    return "Unknown command\r\n";
            }
        }
    }
}
