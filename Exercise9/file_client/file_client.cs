using System;
using System.IO;
using System.Net;
using System.Text;
using System.Net.Sockets;

namespace UDP
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
			UdpClient udpClient = new UdpClient(PORT);
			
			 try{
         udpClient.Connect(args[0], PORT);

         // Sends a message to the host to which you have connected.
         Byte[] sendBytes = Encoding.ASCII.GetBytes(args[1]);

         udpClient.Send(sendBytes, sendBytes.Length);

        
         //IPEndPoint object will allow us to read datagrams sent from any source.
         IPEndPoint RemoteIpEndPoint = new IPEndPoint(IPAddress.Any, PORT);

         // Blocks until a message returns on this socket from a remote host.
         Byte[] receiveBytes = udpClient.Receive(ref RemoteIpEndPoint); 
         string returnData = Encoding.ASCII.GetString(receiveBytes);

         // Uses the IPEndPoint object to determine which of these two hosts responded.
         Console.WriteLine("This is the message you received: \n" +
    	                              returnData.ToString());
         

          udpClient.Close();
        

          }  
       catch (Exception e ) {
                  Console.WriteLine(e.ToString());
        }

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
