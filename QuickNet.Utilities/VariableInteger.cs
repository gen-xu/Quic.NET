using QuicNet.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickNet.Utilities
{
    public class VariableInteger
    {
        public const ulong MaxValue = 4611686018427387903;

        private ulong _integer;
        public ulong Value { get { return _integer; } }

        public VariableInteger(ulong integer)
        {
            _integer = integer;
        }

        public static implicit operator byte[](VariableInteger integer)
        {
            return Encode(integer._integer);
        }

        public static implicit operator VariableInteger(byte[] bytes)
        {
            return new VariableInteger(Decode(bytes));
        }

        public static implicit operator VariableInteger(ulong integer)
        {
            return new VariableInteger(integer);
        }

        public static implicit operator ulong(VariableInteger integer)
        {
            return integer._integer;
        }

        public static implicit operator VariableInteger(StreamId streamId)
        {
            return new VariableInteger(streamId.IntegerValue);
        }

        public static int Size(byte firstByte)
        {
            int result = (int)Math.Pow(2, (firstByte >> 6));

            return result;
        }

        public byte[] ToByteArray()
        {
            return Encode(this._integer);
        }

        public static byte[] Encode(ulong integer)
        {
            int requiredBytes = 0;
            if (integer <= byte.MaxValue >> 2) /* 63 */
                requiredBytes = 1;
            else if (integer <= UInt16.MaxValue >> 2) /* 16383 */
                requiredBytes = 2;
            else if (integer <= UInt32.MaxValue >> 2) /* 1073741823 */
                requiredBytes = 4;
            else if (integer <= ulong.MaxValue >> 2) /* 4611686018427387903 */
                requiredBytes = 8;
            else
                throw new ArgumentOutOfRangeException("Value is larger than VariableInteger.MaxValue.");

            int offset = 8 - requiredBytes;

            byte[] ulongBytes = ByteUtilities.GetBytes(integer);
            byte first = ulongBytes[offset];
            first = (byte)(first | (requiredBytes / 2) << 6);
            ulongBytes[offset] = first;

            byte[] result = new byte[requiredBytes];
            Buffer.BlockCopy(ulongBytes, offset, result, 0, requiredBytes);

            return result;
        }

        public static ulong Decode(byte[] bytes)
        {
            int i = 8 - bytes.Length;
            byte[] buffer = new byte[8];

            Buffer.BlockCopy(bytes, 0, buffer, i, bytes.Length);
            buffer[i] = (byte)(buffer[i] & (255 >> 2));

            ulong res = ByteUtilities.Toulong(buffer);

            return res;
        }
    }
}
