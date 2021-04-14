using QuicNet.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickNet.Utilities
{
    public class GranularInteger
    {
        public const ulong MaxValue = 18446744073709551615;

        private ulong _integer;
        public ulong Value { get { return _integer; } }
        public byte Size { get { return RequiredBytes(Value); } }

        public GranularInteger(ulong integer)
        {
            _integer = integer;
        }

        public byte[] ToByteArray()
        {
            return Encode(this._integer);
        }

        public static implicit operator byte[](GranularInteger integer)
        {
            return Encode(integer._integer);
        }

        public static implicit operator GranularInteger(byte[] bytes)
        {
            return new GranularInteger(Decode(bytes));
        }

        public static implicit operator GranularInteger(ulong integer)
        {
            return new GranularInteger(integer);
        }

        public static implicit operator ulong(GranularInteger integer)
        {
            return integer._integer;
        }

        public static byte[] Encode(ulong integer)
        {
            byte requiredBytes = RequiredBytes(integer);
            int offset = 8 - requiredBytes;

            byte[] ulongBytes = ByteUtilities.GetBytes(integer);

            byte[] result = new byte[requiredBytes];
            Buffer.BlockCopy(ulongBytes, offset, result, 0, requiredBytes);

            return result;
        }

        public static ulong Decode(byte[] bytes)
        {
            int i = 8 - bytes.Length;
            byte[] buffer = new byte[8];

            Buffer.BlockCopy(bytes, 0, buffer, i, bytes.Length);

            ulong res = ByteUtilities.Toulong(buffer);

            return res;
        }

        private static byte RequiredBytes(ulong integer)
        {
            byte result = 0;

            if (integer <= byte.MaxValue) /* 255 */
                result = 1;
            else if (integer <= UInt16.MaxValue) /* 65535 */
                result = 2;
            else if (integer <= UInt32.MaxValue) /* 4294967295 */
                result = 4;
            else if (integer <= ulong.MaxValue) /* 18446744073709551615 */
                result = 8;
            else
                throw new ArgumentOutOfRangeException("Value is larger than GranularInteger.MaxValue.");

            return result;
        }
    }
}
