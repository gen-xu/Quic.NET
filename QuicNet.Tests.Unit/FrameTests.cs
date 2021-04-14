using QuickNet.Utilities;
using QuicNet.Constants;
using QuicNet.Infrastructure;
using QuicNet.Infrastructure.Frames;
using Xunit;

namespace QuicNet.Tests.Unit
{
    public class FrameTests
    {
        [Fact]
        public void ConnectionCloseFrameTest()
        {
            var ccf = new ConnectionCloseFrame(ErrorCode.PROTOCOL_VIOLATION, 0x00, ErrorConstants.PMTUNotReached);
            byte[] data = ccf.Encode();

            var nccf = new ConnectionCloseFrame();
            nccf.Decode(new ByteArray(data));

            Assert.Equal(ccf.ActualType, nccf.ActualType);
            Assert.Equal(ccf.FrameType.Value, nccf.FrameType.Value);
            Assert.Equal(ccf.ReasonPhraseLength.Value, nccf.ReasonPhraseLength.Value);
            Assert.Equal(ccf.ReasonPhrase, nccf.ReasonPhrase);
        }
    }
}
