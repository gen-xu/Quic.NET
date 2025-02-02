﻿using QuickNet.Utilities;
using QuicNet.Utilities;
using System.Collections.Generic;

namespace QuicNet.Infrastructure.Frames
{
    public class ConnectionCloseFrame : Frame
    {
        public byte ActualType { get; set; }
        public override byte Type => 0x1c;
        public VariableInteger ErrorCode { get; set; }
        public VariableInteger FrameType { get; set; }
        public VariableInteger ReasonPhraseLength { get; set; }
        public string ReasonPhrase { get; set; }

        public ConnectionCloseFrame()
        {
            ErrorCode = 0;
            ReasonPhraseLength = new VariableInteger(0);
        }

        /// <summary>
        /// 0x1d not yet supported (Application Protocol Error)
        /// </summary>
        public ConnectionCloseFrame(ErrorCode error, byte frameType, string reason)
        {
            ActualType = 0x1c;

            ErrorCode = (ulong)error;
            FrameType = new VariableInteger((ulong)frameType);
            if (!string.IsNullOrWhiteSpace(reason))
            {
                ReasonPhraseLength = new VariableInteger((ulong)reason.Length);
            }
            else
            {
                ReasonPhraseLength = new VariableInteger(0);
            }

            ReasonPhrase = reason;
        }

        public override void Decode(ByteArray array)
        {
            ActualType = array.ReadByte();
            ErrorCode = array.ReadVariableInteger();
            if (ActualType == 0x1c)
            {
                FrameType = array.ReadVariableInteger();
            }

            ReasonPhraseLength = array.ReadVariableInteger();

            byte[] rp = array.ReadBytes((int)ReasonPhraseLength.Value);
            ReasonPhrase = ByteUtilities.GetString(rp);
        }

        public override byte[] Encode()
        {
            List<byte> result = new List<byte>();
            result.Add(ActualType);
            result.AddRange(ErrorCode.ToByteArray());
            if (ActualType == 0x1c)
            {
                result.AddRange(FrameType.ToByteArray());
            }

            if (string.IsNullOrWhiteSpace(ReasonPhrase) == false)
            {
                byte[] rpl = new VariableInteger((ulong)ReasonPhrase.Length);
                result.AddRange(rpl);

                byte[] reasonPhrase = ByteUtilities.GetBytes(ReasonPhrase);
                result.AddRange(reasonPhrase);
            }

            return result.ToArray();
        }
    }
}
