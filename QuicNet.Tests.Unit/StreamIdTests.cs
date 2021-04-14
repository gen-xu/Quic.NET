
using QuickNet.Utilities;
using Xunit;

namespace QuicNet.Tests.Unit
{
    public class StreamIdTests
    {
        [Fact]
        public void ClientBidirectional()
        {
            StreamId id = new StreamId(123, StreamType.ClientBidirectional);
            byte[] data = id;

            Assert.NotNull(data);
            Assert.Equal(8, data.Length);
            Assert.Equal(1, data[6]);
            Assert.Equal(236, data[7]);
        }

        [Fact]
        public void ClientUnidirectional()
        {
            StreamId id = new StreamId(123, StreamType.ClientUnidirectional);
            byte[] data = id;

            Assert.NotNull(data);
            Assert.Equal(8, data.Length);
            Assert.Equal(1, data[6]);
            Assert.Equal(238, data[7]);
        }

        [Fact]
        public void ServerBidirectional()
        {
            StreamId id = new StreamId(123, StreamType.ServerBidirectional);
            byte[] data = id;

            Assert.NotNull(data);
            Assert.Equal(8, data.Length);
            Assert.Equal(1, data[6]);
            Assert.Equal(237, data[7]);
        }

        [Fact]
        public void ServerUnidirectional()
        {
            StreamId id = new StreamId(123, StreamType.ServerUnidirectional);
            byte[] data = id;

            Assert.NotNull(data);
            Assert.Equal(8, data.Length);
            Assert.Equal(1, data[6]);
            Assert.Equal(239, data[7]);
        }

        [Fact]
        public void VariableIntegerTest()
        {
            StreamId id = new StreamId(123, StreamType.ClientBidirectional);

            VariableInteger integer = id;

            StreamId converted = integer;

            Assert.Equal(id.Id, converted.Id);
        }
    }
}
