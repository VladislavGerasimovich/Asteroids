using Asteroids.Scripts.Binders;
using MVVM;
using Zenject;

namespace Asteroids.Scripts.Infrastructure
{
    public class BindersInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BinderFactory.RegisterBinder<TextBinder>();
            BinderFactory.RegisterBinder<ViewSetterBinder<int>>();
            BinderFactory.RegisterBinder<ViewSetterBinder<bool>>();
            BinderFactory.RegisterBinder<ButtonBinder>();
        }
    }
}