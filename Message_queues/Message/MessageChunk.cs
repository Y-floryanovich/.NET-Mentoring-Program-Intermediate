using System;

namespace Message
{
    public class MessageChunk
    {
        public Guid MessageId;

        public int SequenceLength;

        public int Position;

        public byte[] Data;
    }
}
