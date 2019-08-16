using BasicProject.Service.DTOs.Log;

namespace BasicProject.Service.Interface
{
    public interface IServiceLog
    {
        void Add(LogCreateDTO log);
    }
}
