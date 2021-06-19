using System.Windows.Forms;

namespace VoIPSteganography
{
    partial class VoiceChat
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblSecretMessage = new System.Windows.Forms.Label();
            this.btnEndCall = new System.Windows.Forms.Button();
            this.btnCall = new System.Windows.Forms.Button();
            this.cmbCodecs = new System.Windows.Forms.ComboBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.txtCallToIp = new System.Windows.Forms.TextBox();
            this.txtMessage = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblSecretMessage
            // 
            this.lblSecretMessage.AutoSize = true;
            this.lblSecretMessage.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblSecretMessage.Location = new System.Drawing.Point(95, 185);
            this.lblSecretMessage.Name = "lblSecretMessage";
            this.lblSecretMessage.Size = new System.Drawing.Size(0, 17);
            this.lblSecretMessage.TabIndex = 21;
            // 
            // btnEndCall
            // 
            this.btnEndCall.Enabled = false;
            this.btnEndCall.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnEndCall.Location = new System.Drawing.Point(235, 138);
            this.btnEndCall.Name = "btnEndCall";
            this.btnEndCall.Size = new System.Drawing.Size(142, 37);
            this.btnEndCall.TabIndex = 20;
            this.btnEndCall.Text = "End Call";
            this.btnEndCall.UseVisualStyleBackColor = true;
            this.btnEndCall.Click += new System.EventHandler(this.btnEndCall_Click);
            // 
            // btnCall
            // 
            this.btnCall.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnCall.Location = new System.Drawing.Point(95, 138);
            this.btnCall.Name = "btnCall";
            this.btnCall.Size = new System.Drawing.Size(134, 37);
            this.btnCall.TabIndex = 19;
            this.btnCall.Text = "Call";
            this.btnCall.UseVisualStyleBackColor = true;
            this.btnCall.Click += new System.EventHandler(this.btnCall_Click);
            // 
            // cmbCodecs
            // 
            this.cmbCodecs.FormattingEnabled = true;
            this.cmbCodecs.Items.AddRange(new object[] {
            "None",
            "Alaw",
            "MuLaw"});
            this.cmbCodecs.Location = new System.Drawing.Point(95, 80);
            this.cmbCodecs.Name = "cmbCodecs";
            this.cmbCodecs.Size = new System.Drawing.Size(282, 24);
            this.cmbCodecs.TabIndex = 18;
            this.cmbCodecs.SelectedIndexChanged += new System.EventHandler(this.cmbCodecs_SelectedIndexChanged);
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(95, 49);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(282, 22);
            this.txtName.TabIndex = 17;
            this.txtName.Text = "Alice";
            // 
            // txtCallToIp
            // 
            this.txtCallToIp.Location = new System.Drawing.Point(95, 19);
            this.txtCallToIp.Name = "txtCallToIp";
            this.txtCallToIp.Size = new System.Drawing.Size(282, 22);
            this.txtCallToIp.TabIndex = 16;
            this.txtCallToIp.Text = "127.0.0.1";
            // 
            // txtMessage
            // 
            this.txtMessage.Location = new System.Drawing.Point(95, 110);
            this.txtMessage.MaxLength = 100;
            this.txtMessage.Name = "txtMessage";
            this.txtMessage.Size = new System.Drawing.Size(282, 22);
            this.txtMessage.TabIndex = 15;
            this.txtMessage.Text = "This is a test";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label4.Location = new System.Drawing.Point(16, 113);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 17);
            this.label4.TabIndex = 14;
            this.label4.Text = "Message : ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label3.Location = new System.Drawing.Point(16, 84);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 17);
            this.label3.TabIndex = 13;
            this.label3.Text = "Codec : ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label2.Location = new System.Drawing.Point(16, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 17);
            this.label2.TabIndex = 12;
            this.label2.Text = "Name : ";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label1.Location = new System.Drawing.Point(16, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 17);
            this.label1.TabIndex = 11;
            this.label1.Text = "Call To : ";
            // 
            // VoiceChat
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(392, 213);
            this.Controls.Add(this.lblSecretMessage);
            this.Controls.Add(this.btnEndCall);
            this.Controls.Add(this.btnCall);
            this.Controls.Add(this.cmbCodecs);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.txtCallToIp);
            this.Controls.Add(this.txtMessage);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.Name = "VoiceChat";
            this.Text = "VoiceChat";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.VoiceChat_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label lblSecretMessage;
        private Button btnEndCall;
        private Button btnCall;
        private ComboBox cmbCodecs;
        private TextBox txtName;
        private TextBox txtCallToIp;
        private TextBox txtMessage;
        private Label label4;
        private Label label3;
        private Label label2;
        private Label label1;
    }
}

