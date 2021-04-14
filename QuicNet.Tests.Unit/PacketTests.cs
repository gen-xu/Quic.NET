using QuicNet.Infrastructure.Frames;
using QuicNet.Infrastructure.Packets;
using Xunit;

namespace QuicNet.Tests.Unit
{
    public class PacketTests
    {
        [Fact]
        public void LongeHeaderPacketTest()
        {
            LongHeaderPacket packet = new LongHeaderPacket(Infrastructure.PacketType.Handshake, 123415332, 1);
            packet.Version = 32;

            for (int i = 0; i < 123; i++)
            {
                packet.AttachFrame(new PaddingFrame());
            }

            byte[] data = packet.Encode();

            LongHeaderPacket result = new LongHeaderPacket();
            result.Decode(data);

            Assert.Equal(packet.Type, result.Type);
            Assert.Equal(packet.Version, result.Version);
            Assert.Equal(packet.DestinationConnectionIdLength, result.DestinationConnectionIdLength);
            Assert.Equal(packet.DestinationConnectionId.Value, result.DestinationConnectionId.Value);
            Assert.Equal(packet.SourceConnectionIdLength, result.SourceConnectionIdLength);
            Assert.Equal(packet.SourceConnectionId.Value, result.SourceConnectionId.Value);
            Assert.Equal(packet.PacketType, result.PacketType);
            Assert.Equal(packet.GetFrames().Count, result.GetFrames().Count);
        }
    }
}
