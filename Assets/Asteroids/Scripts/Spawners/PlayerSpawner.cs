using Asteroids.Scripts.OwnPhysics;
using Asteroids.Scripts.PlayerShipMovement;
using Asteroids.Scripts.SaveSystem;
using Asteroids.Scripts.ViewFactories.PlayerShip;
using UnityEngine;
using Zenject;

namespace Asteroids.Scripts.Spawners
{
    public class PlayerSpawner : MonoBehaviour, IInitializable
    {
        [SerializeField] private PlayerShipView playerShipView;

        private PhysicsRouter _physicsRouter;
        private ShipMovement _shipMovement;
        private CollisionsRecords _collisionsRecords;
        private SaveDataRepository _saveDataRepository;
        private Camera _camera;
        
        public PlayerShipView CurrentPlayerShipView { get; private set; }

        [Inject]
        private void Construct(
            PhysicsRouter physicsRouter,
            ShipMovement shipMovement,
            CollisionsRecords collisionsRecords,
            SaveDataRepository saveDataRepository,
            Camera camera)
        {
            _physicsRouter = physicsRouter;
            _shipMovement = shipMovement;
            _collisionsRecords = collisionsRecords;
            _saveDataRepository = saveDataRepository;
            _camera = camera;
        }

        public void Initialize()
        {
            GameObject playerContainer = new GameObject("PlayerContainer");
            CurrentPlayerShipView = Instantiate(
                playerShipView.gameObject,
                new Vector3(_saveDataRepository.PlayerPosition.x, _saveDataRepository.PlayerPosition.y),
                Quaternion.identity,
                playerContainer.transform).GetComponent<PlayerShipView>();
            CurrentPlayerShipView.Init(_camera);
            PlayerPhysicsEventsBroadcaster playerPhysicsEventsBroadcaster =
                CurrentPlayerShipView.GetComponent<PlayerPhysicsEventsBroadcaster>();
            playerPhysicsEventsBroadcaster.Init(_physicsRouter, _shipMovement, _collisionsRecords, _saveDataRepository);
        }
    }
}