namespace _Scripts.Services.DataService
{
    public class DataReader : IDataReader
    {
        private IStorageService _storageService;
        private GameData _gameData;

        public DataReader(IStorageService storageService)
        {
            _storageService = storageService;
        }

        public GameData GetData()
        {
            return _gameData;
        }

        public void SaveData()
        {
            _storageService.Save(GlobalConstants.GameData, _gameData);
        }

        public void Init()
        {
            _gameData = _storageService.Load<GameData>(GlobalConstants.GameData);
            
            if (_gameData == null)
            {
                _gameData = new GameData();
                SaveData();
            }
        }
    }
}