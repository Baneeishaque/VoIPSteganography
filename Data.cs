using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace VoIPSteganography
{
    /// <summary>
    /// The commands for interaction between the two parties.
    /// SIP Commands
    /// </summary>
    public enum Command
    {
        /// <summary>
        /// Invite For a Call.
        /// </summary>
        Invite,
        /// <summary>
        /// End a call.
        /// </summary>
        Bye,
        /// <summary>
        /// User busy.
        /// </summary>
        Busy,
        /// <summary>
        /// Response to an invite message. OK is send to indicate that call is accepted.
        /// </summary>
        OK,
        /// <summary>
        /// No command.
        /// </summary>
        Null,
    }

    /// <summary>
    /// Voice Codec.
    /// </summary>
    public enum Vocoder
    {
        /// <summary>
        /// A-Law codec.
        /// </summary>
        ALaw,
        /// <summary>
        /// u-Law Codec.
        /// </summary>
        uLaw,
        /// <summary>
        /// Don't use any vocoder.
        /// </summary>
        None,
    }

    /// <summary>
    /// SIP Data
    /// </summary>
    [Serializable]
    class Data
    {
        /// <summary>
        /// Name by which the client Calls.
        /// </summary>
        public string strName;

        /// <summary>
        /// Stores the User IP.
        /// </summary>
        public string myip;

        /// <summary>
        /// Command type (INVITE, OK etc).
        /// </summary>
        public Command cmdCommand;

        /// <summary>
        /// The codec while calling.
        /// </summary>
        public Vocoder vocoder;

        /// <summary>
        /// Default Constructor.
        /// </summary>
        public Data()
        {
            // Set Default Values for Variables.
            cmdCommand = Command.Null;
            strName = null;
            myip = null;
            vocoder = Vocoder.None;
        }

        /// <summary>
        /// Returns Data Object From byte array.
        /// </summary>
        /// <param name="data">Data Byte Array.</param>
        /// <returns>Data Object</returns>
        public static Data FromBytes(byte[] data)
        {
            // Create a memory stream.
            using (var stream = new MemoryStream(data))
            {
                var bf = new BinaryFormatter();
                // Deserialize Data Object from stream using binary Formatter.
                return (Data)bf.Deserialize(stream);
            }
        }

        /// <summary>
        /// Converts the Data structure into an array of bytes.
        /// </summary>
        /// <returns>Byte Array.</returns>
        public byte[] ToByte()
        {
            byte[] bytes; // Store Bytes.
            // Create an empty stream.
            using (var stream = new MemoryStream()) {
                var bf = new BinaryFormatter();
                // Serialize Current object into Binary Format.
                bf.Serialize(stream, this);
                bytes = stream.ToArray();
            }
            return bytes;
        }
    }
}
