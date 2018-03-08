using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace TCPTeste
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Int32 porta = 1200;
                TcpListener listen = new TcpListener(IPAddress.Any, porta);

                Console.WriteLine("Escutando!");

                listen.Start();

                TcpClient client = listen.AcceptTcpClient();
                Console.WriteLine("[Cliente Conectou!]");

                NetworkStream stream = client.GetStream();

                using (StreamReader streamReader = new StreamReader(stream))
                {
                    List<byte> msg = new List<byte>();
                    //Loop para receber todos dados enviados pelo cliente.
                    string dados;
                    while ((dados = streamReader.ReadLine()) != "")
                    {
                        Console.WriteLine(String.Format("Received: {0}", dados));

                        //Processa dados enviados pelo cliente.
                        dados = dados.ToUpper();

                        msg.AddRange(System.Text.Encoding.ASCII.GetBytes(dados));
                    }

                    using (StreamWriter sw = new StreamWriter(stream))
                    {
                        sw.Write("<html><body>Hello There!</body></html>");
                    }
                    //ss
                    stream.Write(msg.ToArray(), 0, msg.Count);
                    Console.WriteLine("Sending messagem...");
                }
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
                Console.Write("ok");
            }
            //byte[] buffer = new byte[client.ReceiveBufferSize];

            //int data = stream.Read(buffer, 0, client.ReceiveBufferSize);

            //string ch = Encoding.Unicode.GetString(buffer, 0, data);

            //Console.WriteLine("Message Received:" + ch);

            //client.Close();
            //Console.ReadKey;

        }
    }
}
