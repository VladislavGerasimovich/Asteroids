using Zenject;
using Firebase.Extensions;

namespace Asteroids.Scripts.FirebaseIntegration
{
    public class FirebaseAnalyticsSetter : IInitializable
    {
        private const string PlayerDied = "playerDied";
        private const string PlayerScore = "playerScore";
        
        private bool _isFirebaseReady;
        private Firebase.FirebaseApp _app;

        public void Initialize()
        {
            Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task => {
                var dependencyStatus = task.Result;
                if (dependencyStatus == Firebase.DependencyStatus.Available) {
                    // Create and hold a reference to your FirebaseApp,
                    // where app is a Firebase.FirebaseApp property of your application class.
                    _app = Firebase.FirebaseApp.DefaultInstance;
                    _isFirebaseReady = true;
                    // Set a flag here to indicate whether Firebase is ready to use by your app.
                } else {
                    UnityEngine.Debug.LogError(System.String.Format(
                        "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
                    // Firebase Unity SDK is not safe to use here.
                }
            });
        }

        public void Set(int playerScore)
        {
            if (!_isFirebaseReady)
                return;
            
            Firebase.Analytics.FirebaseAnalytics.LogEvent(PlayerDied,
                new Firebase.Analytics.Parameter(PlayerScore, playerScore)
                );
        }
    }
}