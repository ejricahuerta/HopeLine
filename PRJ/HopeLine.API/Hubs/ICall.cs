
using System.Threading.Tasks;
namespace HopeLine.API.Hubs
{
    public interface ICall
    {
        Task CallDisconnected(string roomId);
        Task ConnectCall(string roomId);
        Task RequestToVideoCall(string roomId);
    }
}