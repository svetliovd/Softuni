using System;

namespace Bashsoft.Attributes
{
    [AttributeUsage(AttributeTargets.Field)]
    public class InjectAttribute : Attribute
    {
        public InjectAttribute()
        {

        }
    }
}
