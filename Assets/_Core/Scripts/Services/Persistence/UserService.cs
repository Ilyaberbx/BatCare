using System.Threading;
using System.Threading.Tasks;
using Better.Services.Runtime;

namespace Workspace.Services.Persistence
{
    public class UserService : PocoService<UserServiceSettings>
    {
        protected override Task OnInitializeAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        protected override Task OnPostInitializeAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}