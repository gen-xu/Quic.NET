using QuicNet.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickNet.Utilities
{
    public enum StreamType
    {
        ClientBidirectional = 0x0,
        ServerBidirectional = 0x1,
        ClientUnidirectional = 0x2,
        ServerUnidirectional = 0x3
    }

    public class StreamId
    {
        public ulong Id { get; }
        public ulong IntegerValue { get; }
        public StreamType Type { get; private set; }

        public StreamId(ulong id, StreamType type)
        {
            Id = id;
            Type = type;
            IntegerValue = id << 2 | (ulong)type;
        }

        public static implicit operator byte[](StreamId id)
        {
            return Encode(id.Id, id.Type);
        }

        public static implicit operator StreamId(byte[] data)
        {
            return Decode(data);
        }

        public static implicit operator ulong(StreamId streamId)
        {
            return streamId.Id;
        }

        public static implicit operator StreamId(VariableInteger integer)
        {
            return Decode(ByteUtilities.GetBytes(integer.Value));
        }

        public static byte[] Encode(ulong id, StreamType type)
        {
            ulong identifier = id << 2 | (ulong)type;

            byte[] result = ByteUtilities.GetBytes(identifier);

            return result;
        }

        public static StreamId Decode(byte[] data)
        {
            StreamId result;
            ulong id = ByteUtilities.Toulong(data);
            ulong identifier = id >> 2;
            ulong type = (ulong)(0x03 & id);
            StreamType streamType = (StreamType)type;

            result = new StreamId(identifier, streamType);

            return result;
        }
    }
}
