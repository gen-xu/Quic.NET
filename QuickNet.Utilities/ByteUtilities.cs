using System;
using System.Text;

namespace QuicNet.Utilities
{
    public static class ByteUtilities
    {
        public static byte[] GetBytes(ulong integer)
        {
            byte[] result = BitConverter.GetBytes(integer);
            if (BitConverter.IsLittleEndian)
                Array.Reverse(result);

            return result;
        }

        public static byte[] GetBytes(uint integer)
        {
            byte[] result = BitConverter.GetBytes(integer);
            if (BitConverter.IsLittleEndian)
                Array.Reverse(result);

            return result;
        }

        public static byte[] GetBytes(ushort integer)
        {
            byte[] result = BitConverter.GetBytes(integer);
            if (BitConverter.IsLittleEndian)
                Array.Reverse(result);

            return result;
        }

        public static byte[] GetBytes(string str)
        {
            byte[] result = Encoding.UTF8.GetBytes(str);

            return result;
        }

        public static ulong Toulong(byte[] data)
        {
            if (BitConverter.IsLittleEndian)
                Array.Reverse(data);

            ulong result = BitConverter.ToUInt64(data, 0);

            return result;
        }

        public static uint ToUInt32(byte[] data)
        {
            if (BitConverter.IsLittleEndian)
                Array.Reverse(data);

            uint result = BitConverter.ToUInt32(data, 0);

            return result;
        }

        public static ushort ToUInt16(byte[] data)
        {
            if (BitConverter.IsLittleEndian)
                Array.Reverse(data);

            ushort result = BitConverter.ToUInt16(data, 0);

            return result;
        }

        public static string GetString(byte[] str)
        {
            string result = Encoding.UTF8.GetString(str);

            return result;
        }

    }
}
