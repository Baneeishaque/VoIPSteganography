using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Net;

namespace VoIPSteganography
{
    public partial class VoiceChat : Form
    {
        private SIPTransport transport;
        private RTPTransport voice = null;
        private Vocoder globalVocoder = Vocoder.None;


        public VoiceChat()
        {
            InitializeComponent();
            Initialize();
        }

        private void Initialize()
        {
            cmbCodecs.SelectedIndex = 0;

            transport = new SIPTransport(gotACall, startCall, userBusy, endCall);
            voice = null;
        }

        private void endCall(Data obj)
        {
            if (voice != null)
            {
                voice.Stop();
                voice.Close();
                voice = null;
            }
        }

        private void userBusy(Data obj)
        {
            MessageBox.Show("The User you are calling is Busy!");
            this.Invoke(new MethodInvoker(delegate
            {
                btnEndCall_Click(null, null);
            }));
        }

        private void startCall()
        {
            voice.Start();
        }

        private void onMessageRecevied(string message)
        {
            this.Invoke(new MethodInvoker(delegate
            {
                this.lblSecretMessage.Text = message;
            }));
        }

        private void gotACall(Data data)
        {
            var username = (data.strName == null || data.strName.Equals("")) ? "Somebody" : ('"' + data.strName + '"');
            IPAddress addr = IPAddress.Parse(data.myip);

            DialogResult dr = MessageBox.Show(username + " is Calling.\r\n Would you like to Pick Up?", "Call", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
            if (dr == DialogResult.Yes)
            {
                // Pick the call
                if (voice == null)
                {
                    // Get Caller IP
                    voice = new RTPTransport(data.myip, data.vocoder, txtMessage.Text, onMessageRecevied);
                    voice.Start();
                }
                transport.setOnCall(true);
                transport.sendCommand(Command.OK, data.vocoder, "", addr);
            }
            else
            {
                transport.setOnCall(false);
                transport.sendCommand(Command.Busy, Vocoder.None, "", addr);
            }
        }

        private void btnCall_Click(object sender, EventArgs e)
        {

            try
            {
                btnCall.Enabled = false;
                btnEndCall.Enabled = true;
                voice = new RTPTransport(txtCallToIp.Text, globalVocoder, txtMessage.Text, onMessageRecevied);
                IPAddress addr = IPAddress.Parse(txtCallToIp.Text);
                transport.sendCommand(Command.Invite, globalVocoder, txtName.Text, addr);
            }
            catch (System.FormatException _)
            {
                MessageBox.Show("Invalid IP Address");
            }
        }

        private void btnEndCall_Click(object sender, EventArgs e)
        {
            voice.Stop();
            btnEndCall.Enabled = false;
            btnCall.Enabled = true;
            voice.Close();
            voice = null;
        }

        private void VoiceChat_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (transport != null)
            {
                transport.Close();
                transport = null;
            }

            if (voice != null)
            {
                voice.Close();
                voice = null;
            }
        }

        private void cmbCodecs_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cmbCodecs.SelectedIndex)
            {
                case 0:
                    globalVocoder = Vocoder.None;
                    break;
                case 1:
                    globalVocoder = Vocoder.ALaw;
                    break;
                case 2:
                    globalVocoder = Vocoder.uLaw;
                    break;
            }
        }
    }
}