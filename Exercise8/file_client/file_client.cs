using System;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace tcp
{
	class file_client
	{
		/// <summary>
		/// The PORT.
		/// </summary>
		const int PORT = 9000;
		/// <summary>
		/// The BUFSIZE.
		/// </summary>
		const int BUFSIZE = 1000;

		/// <summary>
		/// Initializes a new instance of the <see cref="file_client"/> class.
		/// </summary>
		/// <param name='args'>
		/// The command-line arguments. First ip-adress of the server. Second the filename
		/// </param>
		private file_client (string[] args)
		{
			long size;
			Console.WriteLine ("Client started");
			TcpClient clientSocket = new TcpClient ();
			clientSocket.Connect (args[0], PORT);
			NetworkStream serverStream = clientSocket.GetStream();
			LIB.writeTextTCP (serverStream, args[1]); //Filename skal gives med som argument.
			size = LIB.getFileSizeTCP(serverStream);
			if(size!= 0)
				receiveFile(args[1],serverStream);

			clientSocket.Close();

		}

		/// <summary>
		/// Receives the file.
		/// </summary>
		/// <param name='fileName'>
		/// File name.
		/// </param>
		/// <param name='io'>
		/// Network stream for reading from the server
		/// </param>
		private void receiveFile (String fileName, NetworkStream io)
		{
			
			string fileString = LIB.readTextTCP (io);

			var file = File.Create(fileName);

			byte[] bytesRead = System.Text.ASCIIEncoding.Default.GetBytes(fileString);

			file.Write (bytesRead, 0, 1000);

/*			var file = File.Create (fileName);
			int bytesRead;
			// read the file in chunks of 1000Bytes
			var buffer = new byte[1000];

			while ((bytesRead=io.Read (buffer, 0, buffer.Length)) > 0) 
				{
					//io.Read(buffer,0,length);
					file.Write (buffer, 0, bytesRead);

				}*/
			
		}

		/// <summary>
		/// The entry point of the program, where the program control starts and ends.
		/// </summary>
		/// <param name='args'>
		/// The command-line arguments.
		/// </param>
		public static void Main (string[] args)
		{
			Console.WriteLine ("Client starts...");
			new file_client(args);
		}
	}
}
