using _Scripts.Factories;
using _Scripts.Game.AI;
using _Scripts.Game.InventorySystem;
using _Scripts.Game.PlayerCore;
using _Scripts.Services;
using _Scripts.Services.DataService;
using _Scripts.Services.SceneLoadService;
using _Scripts.Services.StateMachines.LevelStateMachine;
using _Scripts.Services.StateMachines.LevelStateMachine.LevelStates;
using _Scripts.UI;
using UnityEngine;
using UnityEngine.Tilemaps;
using Zenject;

namespace _Scripts.Game
{
    public class LevelBootstrap : MonoBehaviour
    {
        [SerializeField] private BaseEnemy[] _enemiesForSpawning;
        [SerializeField] private int _enemiesAmount = 3;
        [SerializeField] private Tilemap _tilemap;
        
        private ILevelStateMachine _levelStateMachine;
        private IDataReader _dataReader;
        private InventoryView _inventoryView;
        private EquipmentInventoryView _equipmentInventoryView;
        private PlayerController _playerController;
        private ISceneLoadService _sceneLoadService;
        private EnemiesHasher _enemiesHasher;
        private BaseEnemy.Factory _enemyFactory;

        [Inject]
        private void Construct(
            ILevelStateMachine levelStateMachine,
            IDataReader dataReader,
            InventoryView inventoryView,
            EquipmentInventoryView equipmentInventoryView,
            PlayerController playerController,
            ISceneLoadService sceneLoadService,
            EnemiesHasher enemiesHasher, 
            BaseEnemy.Factory enemyFactory)
        {
            _enemyFactory = enemyFactory;
            _enemiesHasher = enemiesHasher;
            _sceneLoadService = sceneLoadService;
            _playerController = playerController;
            _levelStateMachine = levelStateMachine;
            _dataReader = dataReader;
            _inventoryView = inventoryView;
            _equipmentInventoryView = equipmentInventoryView;
        }

        private void Start()
        {
            InitLevelStates();
            _inventoryView.Init();
            _equipmentInventoryView.Init();
            SpawnEnemies();
            _levelStateMachine.ChangeState<LevelStartState>();
        }

        private void InitLevelStates()
        {
            _levelStateMachine.RegisterState(new LevelStartState(_levelStateMachine));
            _levelStateMachine.RegisterState(new LevelRunState(_levelStateMachine, _playerController, _enemiesHasher));
            _levelStateMachine.RegisterState(new LevelWinState());
            _levelStateMachine.RegisterState(new LevelLoseState(_dataReader, _sceneLoadService));
            _levelStateMachine.RegisterState(new LevelPauseState(_levelStateMachine));
        }

        private void SpawnEnemies()
        {
            BoundsInt tilemapBounds = _tilemap.cellBounds;

            for (int i = 0; i < _enemiesAmount; i++)
            {
                Vector3Int randomTilePosition = new Vector3Int(
                    Random.Range(tilemapBounds.x, tilemapBounds.x + tilemapBounds.size.x),
                    Random.Range(tilemapBounds.y, tilemapBounds.y + tilemapBounds.size.y),
                    0
                );

                TileBase tile = _tilemap.GetTile(randomTilePosition);

                if (tile != null)
                {
                    Vector3 spawnPosition = _tilemap.GetCellCenterWorld(randomTilePosition);
                    BaseEnemy enemyPrefab = _enemiesForSpawning[Random.Range(0, _enemiesForSpawning.Length)];
                    BaseEnemy enemyInstance = _enemyFactory.Create(enemyPrefab.gameObject);
                    enemyInstance.transform.position = spawnPosition;
                }
            }
        }
    }
}