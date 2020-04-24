using Google.Protobuf.WellKnownTypes;
using System;

namespace CustomTypesGRPC
{
    public partial class DECIMAL
    {
        private const decimal NanoFactor = 1_000_000_000;
        public DECIMAL(long units, int nanos)
        {
            Units = units;
            Nanos = nanos;
        }

        public static implicit operator decimal(DECIMAL grpcDecimal)
        {
            return grpcDecimal.Units + grpcDecimal.Nanos / NanoFactor;
        }

        public static implicit operator DECIMAL(decimal value)
        {
            var units = decimal.ToInt64(value);
            var nanos = decimal.ToInt32((value - units) * NanoFactor);
            return new DECIMAL(units, nanos);
        }
    }

    public partial class GUID
    {
        public GUID(string value)
        {
            Value = value;
        }

        public static implicit operator Guid(GUID grpcGuid)
        {
            if (string.IsNullOrWhiteSpace(grpcGuid.Value))
                return Guid.Empty;

            return Guid.Parse(grpcGuid.Value);
        }

        public static implicit operator GUID(Guid value)
        {
            return new GUID(value.ToString());
        }
    }

    public partial class DATETIME
    {
        public DATETIME(Timestamp value)
        {
            Value = value;
        }

        public static implicit operator DateTime(DATETIME grpcDatetime)
        {
            return grpcDatetime.Value.ToDateTime();
        }

        public static implicit operator DATETIME(DateTime value)
        {
            return new DATETIME(value);
        }
    }

    public partial class BOOL
    {
        public BOOL(bool value)
        {
            Value = value;
        }

        public static implicit operator bool(BOOL grpcDatetime)
        {
            return grpcDatetime;
        }

        public static implicit operator BOOL(bool value)
        {
            return new BOOL(value);
        }
    }
}
