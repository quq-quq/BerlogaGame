using Core.Gameplay.SceneManagement;
using Core.Gameplay.UISystem;
using UnityEngine.SceneManagement;
using VContainer;
using VContainer.Unity;

namespace Core.Gameplay.LifetimeScopes
{
    public class GameLifetimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<SceneLoader>(Lifetime.Singleton);
            builder.Register<UIPanelController>(Lifetime.Singleton);
        }
    }
}
