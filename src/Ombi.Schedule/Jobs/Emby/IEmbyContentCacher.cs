using System.Threading.Tasks;

namespace Ombi.Schedule.Jobs.Emby
{
    public interface IEmbyContentCacher
    {
        Task Start();
    }
}