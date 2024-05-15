using System.Threading.Tasks;

namespace Workspace.Services.SceneManagement
{
    public interface ILoadingCurtain
    {
        Task Show();
        Task Hide();
        void ShowProgress(float progress, float ratio);
    }
}