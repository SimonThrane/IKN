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
			// TO DO Your own code
			TcpListener serverSocket=new TcpListener(PORT);
			TcpClient clientSocket = default(TcpClient);
			serverSocket.Start ();
			Console.WriteLine(" >> Server Started");
			clientsocket = serverSocket.AcceptTcpClient ();

			while (true) {
				try
				{
					NetworkStream networkStream = new NetworkStream(clientSocket);
					string dataFromClient = LIB.readTextTCP(networkStream);
					Console.WriteLine(" >> Data from client - " + dataFromClient);
					string fileFromClient = LIB.extractFileName(dataFromClient);
					if(LIB.check_File_Exists(fileFromClient))
					{
						FileInfo fileInfo = new FileInfo(fileFromClient);
						long fileSize = fileInfo.Length();
						sendFile(fileFromClient,fileSize,networkStream);
						networkStream.Flush();
					}
				}
				catch (Exception ex)
				{
					Console.WriteLine (ex.ToString);
				}
				clientSocket.Close ();
				serverSocket.Stop ();
				} 

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
			int index = 0;
			char[] buffer = new char[FILESIZE];
			StreamReader streamReader = new StreamReader();
			while (FILESIZE-1 < streamReader.ReadBlock (buffer, index, FILESIZE))
			{
				index += FILESIZE;
				LIB.writeTextTCP (io, buffer);
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
