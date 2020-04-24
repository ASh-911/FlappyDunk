
namespace FlappyDank.Controllers
{
    public interface IInitializable
    {
        bool IsInited { get; }
        void Init();
    }
}