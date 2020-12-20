using System;

namespace Campaignr.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class FriendlyNameAttribute : Attribute
    {
        public string Value { get; private set; }

        public FriendlyNameAttribute(string value)
        {
            this.Value = value;

        }
    }
}