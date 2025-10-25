using System;
using UnityEngine;
/// <summary>
/// Represents a globally unique identifier (GUID) that is serializable with Unity and usable in game scripts.
/// </summary>
namespace MossUnityUtils.GenericUtil
{
    [Serializable]
    public struct SerializableGuid : IEquatable<SerializableGuid>
    {
        [SerializeField] private uint part1;
        [SerializeField] private uint part2;
        [SerializeField] private uint part3;
        [SerializeField] private uint part4;
        [field: SerializeField] public string ID { get; private set; }
        public static SerializableGuid Empty => new(0, 0, 0, 0);

        public SerializableGuid(uint val1, uint val2, uint val3, uint val4)
        {
            part1 = val1;
            part2 = val2;
            part3 = val3;
            part4 = val4;
            ID = string.Concat(part1.ToString(), part2.ToString(), part3.ToString(), part4.ToString());
        }

        public SerializableGuid(Guid guid)
        {
            byte[] bytes = guid.ToByteArray();
            part1 = BitConverter.ToUInt32(bytes, 0);
            part2 = BitConverter.ToUInt32(bytes, 4);
            part3 = BitConverter.ToUInt32(bytes, 8);
            part4 = BitConverter.ToUInt32(bytes, 12);
            ID = string.Concat(part1.ToString(), part2.ToString(), part3.ToString(), part4.ToString());
        }

        public static SerializableGuid NewGuid() => Guid.NewGuid();

        public static SerializableGuid FromHexString(string hexString)
        {
            if (hexString.Length != 32)
            {
                return Empty;
            }

            return new SerializableGuid
            (
                Convert.ToUInt32(hexString.Substring(0, 8), 16),
                Convert.ToUInt32(hexString.Substring(8, 8), 16),
                Convert.ToUInt32(hexString.Substring(16, 8), 16),
                Convert.ToUInt32(hexString.Substring(24, 8), 16)
            );
        }

        public string ToHexString()
        {
            return $"{part1:X8}{part2:X8}{part3:X8}{part4:X8}";
        }

        public Guid ToGuid()
        {
            var bytes = new byte[16];
            BitConverter.GetBytes(part1).CopyTo(bytes, 0);
            BitConverter.GetBytes(part2).CopyTo(bytes, 4);
            BitConverter.GetBytes(part3).CopyTo(bytes, 8);
            BitConverter.GetBytes(part4).CopyTo(bytes, 12);
            return new Guid(bytes);
        }

        public static implicit operator Guid(SerializableGuid serializableGuid) => serializableGuid.ToGuid();
        public static implicit operator SerializableGuid(Guid guid) => new SerializableGuid(guid);

        public override bool Equals(object obj)
        {
            return obj is SerializableGuid guid && this.Equals(guid);
        }

        public bool Equals(SerializableGuid other)
        {
            return part1 == other.part1 && part2 == other.part2 && part3 == other.part3 && part4 == other.part4;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(part1, part2, part3, part4);
        }

        public static bool operator ==(SerializableGuid left, SerializableGuid right) => left.Equals(right);
        public static bool operator !=(SerializableGuid left, SerializableGuid right) => !(left == right);
    }
}