using System;

namespace Campaignr.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class EnabledAttribute : Attribute
    {
        public bool Value { get; private set; }

        public EnabledAttribute(bool value)
        {
            this.Value = value;

        }
    }
}