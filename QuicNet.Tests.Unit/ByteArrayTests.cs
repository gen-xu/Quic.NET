using QuickNet.Utilities;
using System;
using System.Linq;
using Xunit;

namespace QuicNet.Tests.Unit
{
    public class ByteArrayTests
    {
        [Fact]
        public void SingleByte()
        {
            byte[] data = { 1, 1, 2, 3, 5, 8, 13, 21, 34 };
            ByteArray array = new ByteArray(data);

            byte peek = array.PeekByte();
            byte result = array.ReadByte();

            Assert.Equal(peek, (byte)1);
            Assert.Equal(result, (byte)1);
        }

        [Fact]
        public void SingleConsecutiveBytes()
        {
            byte[] data = { 1, 1, 2, 3, 5, 8, 13, 21, 34 };
            ByteArray array = new ByteArray(data);

            byte r1 = array.ReadByte();
            byte r2 = array.ReadByte();
            byte r3 = array.ReadByte();

            Assert.Equal(r1, (byte)1);
            Assert.Equal(r2, (byte)1);
            Assert.Equal(r3, (byte)2);
        }

        [Fact]
        public void MultipleBytes()
        {
            byte[] data = { 1, 1, 2, 3, 5, 8, 13, 21, 34 };
            ByteArray array = new ByteArray(data);

            byte[] result = array.ReadBytes(6);

            Assert.Equal(6, result.Length);
            Assert.True(result.SequenceEqual(new byte[] { 1, 1, 2, 3, 5, 8 }));
        }

        [Fact]
        public void ReadShort()
        {
            byte[] data = { 1, 1, 2, 3, 5, 8, 13, 21, 34 };
            ByteArray array = new ByteArray(data);

            UInt16 result = array.ReadUInt16();
            Assert.Equal(result, (UInt16)257);
        }

        [Fact]
        public void ReadInteger()
        {
            byte[] data = { 1, 1, 2, 3, 5, 8, 13, 21, 34 };
            ByteArray array = new ByteArray(data);

            UInt32 result = array.ReadUInt32();
            Assert.Equal(result, (UInt32)16843267);
        }

        [Fact]
        public void ReadTooMany()
        {
            byte[] data = { 1, 1, 2, 3, 5, 8, 13, 21, 34 };
            ByteArray array = new ByteArray(data);

            Assert.Throws<ArgumentException>(() =>
            {
                byte[] result = array.ReadBytes(10);
            });
        }
    }
}
