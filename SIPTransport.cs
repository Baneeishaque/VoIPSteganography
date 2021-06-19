using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net.Sockets;
using System.Net;
using System.Windows.Forms;


namespace VoIPSteganography
{
    class SIPTransport
    {
        /// <summary>
        /// Socket Port For Sending Commands.
        /// </summary>
        const Int16 COMMAND_PORT = 5060;

        /// <summary>
        /// Buffer to store the data received.
        /// </summary>
        private byte[] bufferReceive;

        /// <summary>
        /// Socket For Commands.
        /// </summary>
        private Socket clientSocket;

        /// <summary>
        /// Boolean for wether user is in call or not.
        /// </summary>
        private Boolean onCall = false;

        /// <summary>
        /// Action (Callback) When user receives an invite.
        /// </summary>
        private Action<Data> gotACall;

        /// <summary>
        /// Action (Callback) When user is Busy.
        /// </summary>
        private Action<Data> userBusy;

        /// <summary>
        /// Action (Callback) To Start a Call.
        /// </summary>
        private Action startCall;

        /// <summary>
        /// Action (Callback) for Stopping a Call.
        /// </summary>
        private Action<Data> stopCall;

        private Action<Data> endCall;

        /// <summary>
        /// Thread for socket receiver.
        /// </summary>
        private Thread receiverThread;

        public SIPTransport(Action<Data> gotACall, Action startCall, Action<Data> userBusy, Action<Data> endCall)
        {
            // Initialize Member Variables.
            this.gotACall = gotACall;
            this.startCall = startCall;
            this.userBusy = userBusy;

            // Create the receive buffer of size 1024 Bytes.
            bufferReceive = new byte[1024];

            // Create an UDP Socket at Command Port.
            clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            clientSocket.Bind(new IPEndPoint(IPAddress.Any, COMMAND_PORT)); // Accept any host.

            // Initially there is no call.
            onCall = false;

            // Create the Receiver thread.
            receiverThread = new Thread(new ThreadStart(CommandsReceived));
            receiverThread.Start(); // Start the thread.
        }

        /// <summary>
        /// Get The IP Address of the local system.
        /// </summary>
        /// <returns>String IP Address.</returns>
        public static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("No network adapters with an IPv4 address in the system!");
        }

        /// <summary>
        /// Sets the Call Status.
        /// </summary>
        /// <param name="status">Call Status.</param>
        public void setOnCall(bool status)
        {
            onCall = status;
        }

        /// <summary>
        /// Receiver Thread Function.
        /// </summary>
        private void CommandsReceived()
        {
            while (true)
            {
                int len = clientSocket.Receive(bufferReceive, 0, bufferReceive.Length, SocketFlags.None);
                Data msg = Data.FromBytes(bufferReceive);

                switch (msg.cmdCommand)
                {
                    case Command.Invite:
                        /// Show someone is calling
                        if (!onCall)
                        {
                            gotACall(msg);
                        }
                        break;
                    case Command.Bye:
                        /// Disable Call
                        break;
                    case Command.Busy:
                        /// User Busy
                        userBusy?.Invoke(msg);
                        onCall = false;
                        break;
                    case Command.OK:
                        /// Start call
                        // Get the IP
                        startCall();
                        onCall = true;
                        break;
                }
            }
        }

        public void sendCommand(Command cmd, Vocoder vocoder, string name, IPAddress iPAddress)
        {
            Data data = new Data();
            data.vocoder = vocoder;
            data.strName = name;
            data.cmdCommand = cmd;

            // For Invite Command send the Sender IP (Local Ip).
            if (cmd == Command.Invite)
            {
                data.myip = GetLocalIPAddress();
            }

            // Convert Data to Bytes.
            byte[] message = data.ToByte();

            //Send the message asynchronously.
            clientSocket.BeginSendTo(
                message,
                0, message.Length,
                SocketFlags.None,
                new IPEndPoint(iPAddress, COMMAND_PORT),
                new AsyncCallback(CommandSent),
                null
               );

        }
        /// <summary>
        ///  Command Sent Callback.
        /// </summary>
        /// <param name="ar"></param>
        private void CommandSent(IAsyncResult ar)
        {
            try
            {
                clientSocket.EndSendTo(ar);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "SIP Command Sent Failure", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        /// <summary>
        /// Close The Transport Channel. Close Socket and Thread.
        /// </summary>
        public void Close()
        {
            receiverThread.Abort();
            receiverThread = null;

            clientSocket.Close();
            clientSocket = null;

            bufferReceive = null;
        }
    }
}
