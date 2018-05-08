using System;

namespace Copter.Infrastructure.Generator
{
    /// <summary>
    /// 唯一Id 生成类
    /// </summary>
    public class IdGenerator
    {
        private static long twepoch = 1288834974657L;
        private static long workerIdBits = 5L;
        private static long datacenterIdBits = 5L;
        private static long maxWorkerId = -1L ^ (-1L << (int)workerIdBits);
        private static long maxDatacenterId = -1L ^ (-1L << (int)datacenterIdBits);
        private static long sequenceBits = 12L;
        private static long workerIdShift = sequenceBits;
        private static long datacenterIdShift = sequenceBits + workerIdBits;
        private static long timestampLeftShift = sequenceBits + workerIdBits + datacenterIdBits;
        private static long sequenceMask = -1L ^ (-1L << (int)sequenceBits);

        private long workerId;
        private long datacenterId;
        private long sequence = 0L;
        private long lastTimestamp = -1L;

        public IdGenerator(long workerId, long datacenterId)
        {
            if (workerId > maxWorkerId || workerId < 0)
            {
                throw new Exception(String.Format("worker Id can't be greater than {0} or less than 0", maxWorkerId));
            }
            if (datacenterId > maxDatacenterId || datacenterId < 0)
            {
                throw new Exception(String.Format("datacenter Id can't be greater than {0} or less than 0", maxDatacenterId));
            }
            this.workerId = workerId;
            this.datacenterId = datacenterId;
        }

        public /*synchronized*/ long NextId()
        {
            long timestamp = TimeGen();
            if (timestamp < lastTimestamp)
            {
                throw new Exception(String.Format("Clock moved backwards.  Refusing to generate id for {0} milliseconds", lastTimestamp - timestamp));
            }
            if (lastTimestamp == timestamp)
            {
                sequence = (sequence + 1) & sequenceMask;
                if (sequence == 0)
                {
                    timestamp = TilNextMillis(lastTimestamp);
                }
            }
            else
            {
                sequence = 0L;
            }

            lastTimestamp = timestamp;

            return ((timestamp - twepoch) << (int)timestampLeftShift) | (datacenterId << (int)datacenterIdShift) | (workerId << (int)workerIdShift) | sequence;
        }

        protected long TilNextMillis(long lastTimestamp)
        {
            long timestamp = TimeGen();
            while (timestamp <= lastTimestamp)
            {
                timestamp = TimeGen();
            }
            return timestamp;
        }

        protected long TimeGen()
        {
            return (long)(DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds;
        }
    }
}
