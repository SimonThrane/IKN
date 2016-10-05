using System;
using System.IO;
using System.Net;
using System.Text;
using System.Net.Sockets;

namespace tcp
{
	class file_server
	{
		const int PORT = 9000;

		private file_server ()
		{
			Console.WriteLine(" >> Server Started");
			UdpClient udpClient = new UdpClient(PORT);
			while(true)
			{
				IPEndPoint RemoteIpEndPoint = new IPEndPoint(IPAddress.Any,PORT);
				Byte[] receiveBytes = udpClient.Receive(ref RemoteIpEndPoint);
				string recievedString = Encoding.ASCII.GetString(receiveBytes);

				switch (recievedString.ToLower()) {
				case "l":
					udpClient.Send(ReadFile("/proc/loadavg"), ReadFile("/proc/loadavg").Length);
					break;
				case "u":
					udpClient.Send(ReadFile("/proc/uptime"), ReadFile("/proc/uptime").Length);
					break;
				default:
					Byte[] sendBytes = Encoding.ASCII.GetBytes("File do not exist!");
					udpClient.Send(sendBytes, sendBytes.Length);
					break;
				}
		}
			udpClient.Close ();
		}
		private byte[] ReadFile(string path)
			{
				var fileStream = new FileStream(path,FileMode.Open,FileAccess.Read);
				byte[] buffer = new byte[fileStream.Length];
				fileStream.Read (buffer, 0, buffer.Length);
				return buffer;
			}

		/// <summary>
		/// The entry point of the program, where the program control starts and ends.
		/// </summary>
		/// <param name='args'>
		/// The command-line arguments.
		/// </param>
		public static void Main (string[] args)
		{
			Console.WriteLine ("Server starts...");
			new file_server();
		}
	}
}
