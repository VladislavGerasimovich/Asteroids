using Asteroids.Scripts.FirebaseIntegration;
using Asteroids.Scripts.GameLoop;
using Asteroids.Scripts.GameTime;
using Asteroids.Scripts.ResolutionHandler;
using Asteroids.Scripts.RewardSystem;
using Asteroids.Scripts.SDK;
using Asteroids.Scripts.Signals;
using Asteroids.Scripts.Utils;
using Asteroids.Scripts.ViewModels;
using Zenject;

namespace Asteroids.Scripts.Infrastructure
{
    public class GameInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<YandexSDK>().AsSingle();
            Container.Bind<PositionUtils>().AsSingle();
            Container.BindInterfacesTo<ScreenResolutionHandler>().AsSingle();
            Container.BindInterfacesAndSelfTo<FirebaseAnalyticsSetter>().AsSingle();
            Container.BindInterfacesTo<GameTimeHandler>().AsSingle();
            Container.BindInterfacesAndSelfTo<GameLoopHandler>().AsSingle();
            Container.BindInterfacesAndSelfTo<RewardHandler>().AsSingle();
            Container.BindInterfacesAndSelfTo<RewardViewModel>().AsSingle();
            
            SignalBusInstaller.Install(Container);
            DeclareSignals();
        }

        private void DeclareSignals()
        {
            Container.DeclareSignal<OnRestartButtonClickedSignal>();
            Container.DeclareSignal<PlayerDiedSignal>();
        }
    }
}