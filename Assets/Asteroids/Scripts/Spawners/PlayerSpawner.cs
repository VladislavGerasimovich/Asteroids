using Asteroids.Scripts.OwnPhysics;
using Asteroids.Scripts.PlayerShip;
using Asteroids.Scripts.ViewFactories.Player;
using UnityEngine;
using Zenject;

public class PlayerSpawner : MonoBehaviour, IInitializable
{
    [SerializeField] private PlayerShipView _playerShipView;

    public PlayerShipView CurrentPlayerShipView { get; private set; }

    [Inject]
    private PhysicsRouter _physicsRouter;
    [Inject]
    private ShipMovement _shipMovement;
    [Inject]
    private CollisionsRecords _collisionsRecords;
    
    public void Initialize()
    {
        GameObject playerContainer = new GameObject("PlayerContainer");
        CurrentPlayerShipView = Instantiate(
            _playerShipView.gameObject,
            new Vector3(0.5f, 0.5f),
            Quaternion.identity,
            playerContainer.transform).GetComponent<PlayerShipView>();
        PlayerPhysicsEventsBroadcaster playerPhysicsEventsBroadcaster = CurrentPlayerShipView.GetComponent<PlayerPhysicsEventsBroadcaster>();
        playerPhysicsEventsBroadcaster.Init(_physicsRouter, _shipMovement, _collisionsRecords);
    }
}