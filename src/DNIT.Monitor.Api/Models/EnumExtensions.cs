using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace DNIT.Monitor.Api.Models
{
    public static class EnumExtensions
    {
        public static string Description(this Enum @enum)
        {
            var description = string.Empty;
            var fields = @enum.GetType().GetFields();
            foreach (var field in fields)
            {
                if (!(Attribute.GetCustomAttribute(field,
                        typeof(DescriptionAttribute)) is DescriptionAttribute descriptionAttribute) ||
                    !field.Name.Equals(@enum.ToString(), StringComparison.InvariantCultureIgnoreCase)) continue;
                description = descriptionAttribute.Description;
                break;
            }
            return description;
        }
    }
}
