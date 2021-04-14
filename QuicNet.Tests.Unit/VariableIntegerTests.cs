using System;
using QuickNet.Utilities;
using Xunit;

namespace QuicNet.Tests.Unit
{
    public class VariableIntegerTests
    {
        [Fact]
        public void Zero()
        {
            VariableInteger integer = new VariableInteger(0);
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
            VariableInteger integer = new VariableInteger(1);
            byte[] bin = integer;
            ulong num = integer;

            Assert.NotNull(bin);
            Assert.Single(bin);
            Assert.Equal(bin[0], (byte)1);
            Assert.Equal(num, (ulong)1);
        }

        [Fact]
        public void Test63()
        {
            VariableInteger integer = new VariableInteger(63);
            byte[] bin = integer;
            ulong num = integer;

            Assert.NotNull(bin);
            Assert.Single(bin);
            Assert.Equal(bin[0], (byte)63);
            Assert.Equal(num, (ulong)63);
        }

        [Fact]
        public void Test64()
        {
            VariableInteger integer = new VariableInteger(64);
            byte[] bin = integer;
            ulong num = integer;

            Assert.NotNull(bin);
            Assert.Equal(2, bin.Length);
            Assert.Equal(bin[0], (byte)64);
            Assert.Equal(bin[1], (byte)64);
            Assert.Equal(num, (ulong)64);
        }

        [Fact]
        public void Test256()
        {
            VariableInteger integer = new VariableInteger(256);
            byte[] bin = integer;
            ulong num = integer;

            Assert.NotNull(bin);
            Assert.Equal(2, bin.Length);
            Assert.Equal(bin[0], (byte)65);
            Assert.Equal(bin[1], (byte)0);
            Assert.Equal(num, (ulong)256);
        }

        [Fact]
        public void TestVariableIntegerMaxValue()
        {
            VariableInteger integer = new VariableInteger(VariableInteger.MaxValue);
            byte[] bin = integer;
            ulong num = integer;

            Assert.NotNull(bin);
            Assert.Equal(8, bin.Length);
            Assert.Equal(num, VariableInteger.MaxValue);
        }

        [Fact]
        public void TestulongMaxValue()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                VariableInteger integer = new VariableInteger(ulong.MaxValue);
                byte[] bin = integer;
                ulong num = integer;
            });
        }
    }
}
