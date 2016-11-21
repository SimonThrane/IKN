using System;
using System.IO.Ports;

/// <summary>
/// Link.
/// </summary>
namespace Linklaget
{
	/// <summary>
	/// Link.
	/// </summary>
	public class Link
	{
		/// <summary>
		/// The DELIMITE for slip protocol.
		/// </summary>
		const byte DELIMITER = (byte)'A';
		/// <summary>
		/// The buffer for link.
		/// </summary>
		private byte[] buffer;
		/// <summary>
		/// The serial port.
		/// </summary>
		SerialPort serialPort;

		/// <summary>
		/// Initializes a new instance of the <see cref="link"/> class.
		/// </summary>
		public Link (int BUFSIZE)
		{
			// Create a new SerialPort object with default settings.
			serialPort = new SerialPort("/dev/ttyS1",115200,Parity.None,8,StopBits.One);

			if(!serialPort.IsOpen)
				serialPort.Open();

			buffer = new byte[(BUFSIZE*2)];

			//serialPort.ReadTimeout = 200;
			serialPort.DiscardInBuffer ();
			serialPort.DiscardOutBuffer ();
		}

		/// <summary>
		/// Send the specified buf and size.
		/// </summary>
		/// <param name='buf'>
		/// Buffer.
		/// </param>
		/// <param name='size'>
		/// Size.
		/// </param>s
		public void send (byte[] buf, int size)
		{
			int j=1;
			for (int i = 0; i < size; i++) {
				if(buf[i] == (byte)('A'))
					{
					buffer[j++]=Convert.ToByte('B');
					buffer[j++]=Convert.ToByte('C');
						
					}
				else if(buf[i] == (byte)('B'))
						{
					buffer[j++]=Convert.ToByte('B');
					buffer[j++]=Convert.ToByte('D');
							
						}
					else{
					buffer [j++] = buf [i];
				}
			}

			buffer[0]=Convert.ToByte('A');
			buffer[j++]=Convert.ToByte('A');
			serialPort.Write (buffer, 0, j);

		}

		/// <summary>
		/// Receive the specified buf and size.
		/// </summary>
		/// <param name='buf'>
		/// Buffer.
		/// </param>
		/// <param name='size'>
		/// Size.
		/// </param>
		public int receive (ref byte[] buf)
		{
			while ((char) serialPort.ReadByte () != 'A') {}
			int i = 0;
			while ((buffer[i++]= (byte) serialPort.ReadByte () )!= (byte)'A') {}

			i--;
			int f = 0;

			for(int j =0; j<i; j++)
			{
				if(buffer[j].Equals('B'))
					{
						j++;
						if (buffer [j].Equals ('C'))
							buf [f++] = Convert.ToByte ('A');
						if(buffer[j].Equals('D'))
							buf [f++] = Convert.ToByte ('B');
					}
					else
					{
						buf [f++] = buffer [j];
					}
			}

			return buf.Length;
						
		}
	}
}
