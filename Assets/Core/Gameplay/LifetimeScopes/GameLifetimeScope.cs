using Core.Gameplay.SceneManagement;
using VContainer;
using VContainer.Unity;

namespace Core.Gameplay.LifetimeScopes
{
    public class GameLifetimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<SceneLoader>(Lifetime.Singleton);
        }
    }
}
