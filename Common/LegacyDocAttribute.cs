using System;

namespace Common
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class LegacyDocAttribute : Attribute, IEquatable<LegacyDocAttribute>
    {
        public string Order { get; set; } = string.Empty;

        public string Caption { get; set; } = string.Empty;

        public string Content { get; set; } = string.Empty;


        public bool Equals(LegacyDocAttribute other)
        {
            if (other == null) return false;
            return Order == other.Order &&
                   Caption == other.Caption &&
                   Content == other.Content;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as LegacyDocAttribute);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + Order.GetHashCode();
                hash = hash * 23 + Caption.GetHashCode();
                hash = hash * 23 + Content.GetHashCode();
                return hash;
            }
        }
    }
}