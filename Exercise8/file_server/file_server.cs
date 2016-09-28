using System;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace tcp
{
	class file_server
	{
		const int FILESIZE = 1000;
		/// <summary>
		/// The PORT
		/// </summary>
		const int PORT = 9000;
		/// <summary>
		/// The BUFSIZE
		/// </summary>
		const int BUFSIZE = 1000;

		/// <summary>
		/// Initializes a new instance of the <see cref="file_server"/> class.
		/// Opretter en socket.
		/// Venter på en connect fra en klient.
		/// Modtager filnavn
		/// Finder filstørrelsen
		/// Kalder metoden sendFile
		/// Lukker socketen og programmet
 		/// </summary>
		private file_server ()
		{
			TcpListener serverSocket=new TcpListener(new IPEndPoint(IPAddress.Any,PORT));
			TcpClient clientSocket = default(TcpClient);
			serverSocket.Start ();
			Console.WriteLine(" >> Server Started");


			while (true) {
				try
				{
					clientSocket = serverSocket.AcceptTcpClient ();
					NetworkStream networkStream = clientSocket.GetStream();
					string dataFromClient = LIB.readTextTCP(networkStream);
					Console.WriteLine(" >> Data from client - " + dataFromClient);
					FileInfo fileInfo = new FileInfo(dataFromClient);
					long fileSize = fileInfo.Length;
					LIB.writeTextTCP(networkStream,fileSize.ToString());
					if(0!=LIB.check_File_Exists(dataFromClient))
					{
						sendFile(dataFromClient,fileSize,networkStream);
					}
					networkStream.Flush();
				}
				catch (Exception ex)
				{
					Console.WriteLine (ex.ToString());
				}

				} 
			clientSocket.Close ();
			serverSocket.Stop ();
		}

		/// <summary>
		/// Sends the file.
		/// </summary>
		/// <param name='fileName'>
		/// The filename.
		/// </param>
		/// <param name='fileSize'>
		/// The filesize.
		/// </param>
		/// <param name='io'>
		/// Network stream for writing to the client.
		/// </param>
		private void sendFile (String fileName, long fileSize, NetworkStream io)
		{
			// TO DO Your own code

			Byte[] buffer = new Byte[BUFSIZE];

			var fileStream = new FileStream (fileName,FileMode.Open,FileAccess.Read);


			for (int len = fileStream.Read(buffer,0,BUFSIZE); len != 0; len = fileStream.Read(buffer,0,BUFSIZE))
				{
				io.Write (buffer, 0, len);
				}
					
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
