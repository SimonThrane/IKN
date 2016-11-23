using System;
using System.IO;
using System.Text;
using Transportlaget;
using Library;

namespace Application
{
	class file_server
	{
		/// <summary>
		/// The BUFSIZE
		/// </summary>
		private const int BUFSIZE = 1000;

		private Transport Transport;

		/// <summary>
		/// Initializes a new instance of the <see cref="file_server"/> class.
		/// </summary>
		private file_server ()
		{
			byte[] buf = new byte[BUFSIZE];
			Transport = new Transport (BUFSIZE);
			byte[] noFile = Encoding.ASCII.GetBytes ("File does not exist");

			while (true) {
				Transport.receive (ref buf);
				string filename = LIB.extractFileName (System.Text.Encoding.Default.GetString (buf));
				long size=(LIB.check_File_Exists(filename));
				if (size > 0)
					sendFile (filename, size, Transport);
				else {
					Console.WriteLine ("File does not exist");
					sendFile (filename, size, Transport);
				}
			}
		
		}

		/// <summary>
		/// Sends the file.
		/// </summary>
		/// <param name='fileName'>
		/// File name.
		/// </param>
		/// <param name='fileSize'>
		/// File size.
		/// </param>
		/// <param name='tl'>
		/// Tl.
		/// </param>
		private void sendFile(String fileName, long fileSize, Transport transport)
		{
			Byte[] buffer = new Byte[BUFSIZE];

			transport.send (BitConverter.GetBytes (fileSize), BitConverter.GetBytes (fileSize).Length);

			var fileStream = new FileStream (fileName,FileMode.Open,FileAccess.Read);


			for (int len = fileStream.Read(buffer,0,BUFSIZE); len != 0; len = fileStream.Read(buffer,0,BUFSIZE))
			{
				transport.send(buffer, len);
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