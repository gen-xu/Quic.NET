using System;
using QuickNet.Utilities;
using Xunit;

namespace QuicNet.Tests.Unit
{
    public class GranularIntegerTests
    {
        [Fact]
        public void Zero()
        {
            GranularInteger integer = new GranularInteger(0);
            byte[] bin = integer;
            ulong num = integer;

            Assert.NotNull(bin);
            Assert.Single(bin);
            Assert.Equal(bin[0], (byte)0);
            Assert.Equal(num, (ulong)0);
        }

        [Fact]
        public void One()
        {
            GranularInteger integer = new GranularInteger(1);
            byte[] bin = integer;
            ulong num = integer;

            Assert.NotNull(bin);
            Assert.Single(bin);
            Assert.Equal(bin[0], (byte)1);
            Assert.Equal(num, (ulong)1);
        }

        [Fact]
        public void Test255()
        {
            GranularInteger integer = new GranularInteger(255);
            byte[] bin = integer;
            ulong num = integer;

            Assert.NotNull(bin);
            Assert.Single(bin);
            Assert.Equal(bin[0], (byte)255);
            Assert.Equal(num, (ulong)255);
        }

        [Fact]
        public void Test256()
        {
            GranularInteger integer = new GranularInteger(256);
            byte[] bin = integer;
            ulong num = integer;

            Assert.NotNull(bin);
            Assert.Equal(2, bin.Length);
            Assert.Equal(bin[0], (byte)1);
            Assert.Equal(bin[1], (byte)0);
            Assert.Equal(num, (ulong)256);
        }

        [Fact]
        public void TestGranularIntegerMaxValue()
        {
            GranularInteger integer = new GranularInteger(GranularInteger.MaxValue);
            byte[] bin = integer;
            ulong num = integer;

            Assert.NotNull(bin);
            Assert.Equal(8, bin.Length);
            Assert.Equal(num, GranularInteger.MaxValue);
        }
    }
}
