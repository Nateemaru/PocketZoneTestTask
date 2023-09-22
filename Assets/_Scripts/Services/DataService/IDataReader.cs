namespace _Scripts.Services.DataService
{
    public interface IDataReader
    {
        public GameData GetData();
        public void SaveData();
        public void Init();
    }
}