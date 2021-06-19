using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VoIPSteganography
{
    class Steganograph
    {
        /// <summary>
        /// True if text is already hidden.
        /// </summary>
        bool done;

        /// <summary>
        /// The text which needs to be hidden.
        /// </summary>
        byte[] text;

        /// <summary>
        /// How much text is hidden.
        /// if till > length + 1 then done is true.
        /// </summary>
        int till;
        int start;
        private List<byte> sb = null;
        private Action<byte[]> onTextDecoded = null;

        public Steganograph(byte[] text)
        {
            this.text = new byte[text.Length + 1];

            for (int i = 0; i < text.Length; ++i) this.text[i] = text[i];
            this.text[text.Length] = 0;

            done = false;
            till = text.Length + 1;
            start = 0;
        }

        public Steganograph(Action<byte[]> onTextDecoded)
        {
            text = null;
            done = false;
            till = 0;
            start = 0;
            sb = new List<byte>();
            this.onTextDecoded = onTextDecoded;
        }

        public void Reset()
        {
            text = null;
            done = false;
            till = 0;
            start = 0;
        }

        public bool Done()
        {
            return done;
        }

        public void Hide(ref byte[] mod)
        {
            if (done || text == null || start >= till) return;

            int upto = mod.Length / 8;
            upto = Math.Min(upto, till);
            int k = 0;
            for (int i = start; i < upto; ++i)
            {
                int tmp = text[i];
                for (int j = 7; j >= 0; --j)
                {
                    if (((tmp >> j) & 1) == 1)
                    {
                        mod[k] = (byte)(mod[k] | 1);
                    }
                    else mod[k] = (byte)(mod[k] & ~(1));
                    ++k;
                }
            }
            start += upto;
            till -= upto;

            if (start >= till)
            {
                done = true;
            }

        }

        public void Show(byte[] data)
        {
            if (done || sb == null || onTextDecoded == null) return;

            byte tmp = 0;
            for (int i = 0; i < data.Length; ++i)
            {
                if (i > 0 && (i & 7) == 0)
                {
                    if (tmp == 0)
                    {
                        done = true;
                        onTextDecoded(sb.ToArray());
                        sb = null;
                        break;
                    }
                    sb.Add(tmp);
                    tmp = 0;
                }
                tmp <<= 1;
                if ((data[i] & 1) == 1)
                {
                    tmp |= 1;
                }
            }
        }

    }
}
