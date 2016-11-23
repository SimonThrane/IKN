using System;
using System.IO;
using System.Text;
using Transportlaget;
using Library;

namespace Application
{
	class file_client
	{
		/// <summary>
		/// The BUFSIZE.
		/// </summary>
		const int BUFSIZE = 1000;
		private byte[] buffer;
		/// <summary>
		/// Initializes a new instance of the <see cref="file_client"/> class.
		/// 
		/// file_client metoden opretter en peer-to-peer forbindelse
		/// Sender en forspÃ¸rgsel for en bestemt fil om denne findes pÃ¥ serveren
		/// Modtager filen hvis denne findes eller en besked om at den ikke findes (jvf. protokol beskrivelse)
		/// Lukker alle streams og den modtagede fil
		/// Udskriver en fejl-meddelelse hvis ikke antal argumenter er rigtige
		/// </summary>
		/// <param name='args'>
		/// Filnavn med evtuelle sti.
		/// </param>
		private file_client (string[] args)
		{
			buffer = new byte[BUFSIZE];
			Console.WriteLine ("Client started");
			Transport clientSocket = new Transport (BUFSIZE);
			clientSocket.send (Encoding.ASCII.GetBytes (args [0]),Encoding.ASCII.GetBytes (args [0]).Length);
			clientSocket.receive (ref buffer);
			Console.WriteLine (Encoding.ASCII.GetString (buffer));
			/*receiveFile (args [0], clientSocket);*/
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
		private void receiveFile (string fileName, Transport transport)
		{
			int bytesRead = 0;
			long accumulatedBytes = 0;
			int size=transport.receive (ref buffer);
			long fileSize = BitConverter.ToInt64(buffer,0);
			if ( fileSize> 0) {
				var file = File.Create (fileName);

				while (accumulatedBytes < fileSize) {
					bytesRead = transport.receive(ref buffer);
					accumulatedBytes += bytesRead;

					file.Write (buffer, 0, bytesRead);
				}
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
			Console.WriteLine ("Client starts...");
			new file_client(args);
		}
	}
}