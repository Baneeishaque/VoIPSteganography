using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net.Sockets;
using System.Net;
using System.Windows.Forms;
using NAudio.Wave;
using System.IO;


namespace VoIPSteganography
{
    public class RTPTransport
    {
        const Int16 CALL_PORT = 16386;
        const int SAMPLE_RATE = 22050;
        const int CHANNELS = 1;

        private WaveInEvent waveIn;
        private UdpClient udpClient;
        private string hostname;
        private IWavePlayer waveOut;
        private BufferedWaveProvider waveProvider;
        private Thread receiverThread;
        private Vocoder vocoder;
        private Steganograph steganograph;
        private Steganograph recSteganography;
        private Encryption encryption;
        private Action<string> onTextDecoded;

        public RTPTransport(string host, Vocoder test,string secretMessage,Action<string> onTextDecoded)
        {
            this.vocoder = test;
            this.onTextDecoded = onTextDecoded;

            encryption = new Encryption();
            recSteganography = new Steganograph(messageReceived);

            if (secretMessage.Length > 0)
            {
                steganograph = new Steganograph(encryption.Encrypt(secretMessage));
            }

            waveOut = new WaveOut();
            waveProvider = new BufferedWaveProvider(new WaveFormat(SAMPLE_RATE, CHANNELS));
            waveProvider.BufferLength = SAMPLE_RATE * 1000;
            waveOut.Init(waveProvider);
            waveOut.Play();

            hostname = host;

            udpClient = new UdpClient(CALL_PORT);

            receiverThread = new Thread(new ThreadStart(ReceiveVoicePacket));
            receiverThread.Start();
        }

        private void messageReceived(byte[] message)
        {
            onTextDecoded?.Invoke(encryption.Decrypt(message));
        }

        private void SendVoicePacket(object sender, WaveInEventArgs e)
        {
            byte[] toSend = e.Buffer;
            if (vocoder != Vocoder.None)
            {
                if (vocoder == Vocoder.ALaw)
                {
                    toSend = ALawEncoder.ALawEncode(e.Buffer);
                }
                else toSend = MuLawEncoder.MuLawEncode(toSend);
            }

            if (steganograph != null)
            {
                steganograph.Hide(ref toSend);
            }

            udpClient.Send(toSend, toSend.Length, hostname, CALL_PORT);
        }

        private void ReceiveVoicePacket()
        {
            IPEndPoint remoteEP = new IPEndPoint(IPAddress.Any, 0);
            
            while (true)
            {
                byte[] data = udpClient.Receive(ref remoteEP);
                recSteganography.Show(data);
                if (vocoder != Vocoder.None)
                {
                    if (vocoder == Vocoder.ALaw)
                    {
                        data = ALawDecoder.ALawDecode(data);
                    }
                    else data = MuLawDecoder.MuLawDecode(data);
                }
                waveProvider.AddSamples(data, 0, data.Length);
            }
        }

        public void Start()
        {
            waveIn = new WaveInEvent();
            waveIn.BufferMilliseconds = 50;
            waveIn.DeviceNumber = 0;
            var tmp = new WaveFormat(SAMPLE_RATE, CHANNELS);
            waveIn.WaveFormat = tmp;
            waveIn.DataAvailable += new EventHandler<WaveInEventArgs>(this.SendVoicePacket);
            waveIn.StartRecording();
        }

        public void Close()
        {
            receiverThread.Abort();
            this.Stop();
            udpClient.Close();
        }

        public void Stop()
        {
            if (waveOut != null)
            {
                waveOut.Stop();
                waveOut = null;
            }

            if (waveIn != null)
            {
                waveIn.StopRecording();
            }
        }
    }
}
