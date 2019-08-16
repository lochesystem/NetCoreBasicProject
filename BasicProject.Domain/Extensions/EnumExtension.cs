using System;
using System.Reflection;
using System.Resources;

namespace BasicProject.Domain.Extensions
{
    public static class EnumExtensions
    {
        public static string GetDescriptionProperty(this System.Enum myEnum)
        {
            try
            {
                Type type = myEnum.GetType();

                FieldInfo fieldInfo = type.GetField(myEnum.ToString());

                EnumDescriptionAttribute[] attribs = fieldInfo.GetCustomAttributes(typeof(EnumDescriptionAttribute), false) as EnumDescriptionAttribute[];

                return attribs != null ? attribs[0].Description : string.Empty;
            }
            catch
            {
                return string.Empty;
            }
        }
    }

    public class EnumDescriptionAttribute : Attribute
    {
        public Type ResourceType { get; set; }

        public EnumDescriptionAttribute() { Description = string.Empty; }
        public override string ToString()
        {
            return Description;
        }

        private string description;
        public string Description
        {
            get
            {
                if (ResourceType != null)
                {
                    var resourceManager = new ResourceManager(ResourceType);

                    if (resourceManager != null)
                        description = resourceManager.GetString(description);
                }

                return description;
            }
            set
            {
                description = value;
            }
        }
    }
}
