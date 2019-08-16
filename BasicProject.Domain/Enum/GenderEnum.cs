using BasicProject.Domain.Extensions;

namespace BasicProject.Domain.Enum
{
    public enum GenderEnum
    {
        [EnumDescription(Description = "Male")]
        Male = 1,
        [EnumDescription(Description = "Female")]
        Female = 2,
    }
}
