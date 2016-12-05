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
			int bytelength;
			byte[] buf = new byte[BUFSIZE];
			Transport = new Transport (BUFSIZE);

			while (true) {
				bytelength = Transport.receive (ref buf);
				Console.WriteLine (bytelength.ToString());
				string filename = System.Text.Encoding.Default.GetString (buf).Substring(0,bytelength);
				long size=(LIB.check_File_Exists(filename));
				Transport.send (Encoding.ASCII.GetBytes (filename), Encoding.ASCII.GetBytes (filename).Length);
				var sizeinbytes = Encoding.ASCII.GetBytes(size.ToString());
				var sizeinbyteslength = sizeinbytes.Length;
				Transport.send (sizeinbytes, sizeinbyteslength);
				if (size > 0)
					sendFile (filename, size, Transport);
				else {
					Console.WriteLine ("File does not exist");
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