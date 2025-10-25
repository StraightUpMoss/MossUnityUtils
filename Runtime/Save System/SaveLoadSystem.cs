
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using MossUnityUtils.GenericUtil;

namespace MossUnityUtils.SaveSystem
{
    public class SaveLoadSystem : SingletonDontDestroy<SaveLoadSystem>
    {
        [SerializeField] private string[] ignoreSaveLoad;
        [SerializeField] public GameData gameData;
        private Dictionary<string, ILoader> _loaders = new Dictionary<string, ILoader>();
        
        private IDataService _dataService;
        private float _startTime;
        protected override void Awake()
        {
            base.Awake();
            _dataService = new FileDataService(new JsonSerializer());
            
        }

        public void AddLoader(string loaderName, ILoader loader)
        {
            _loaders.TryAdd(loaderName, loader);
        }

         void OnSceneLoaded()
         {
             Debug.Log("Binding data");
         }
        //Helpful for binding to singletons
        public void Bind<T, TData>(TData data) where T : MonoBehaviour, IBind<TData> where TData : ISaveAble, new()
        {
            var entity = FindObjectsByType<T>(FindObjectsSortMode.None).FirstOrDefault();
            Debug.Log($"Attempting to bind: {entity}");
            if (!entity)
            {
                return;
            }
            if (data == null)
            {
                data = new TData { ID = entity.ID };
            }
            entity.Bind(data);
        }
       
        //Binds to all objects of type T
        public void Bind<T, TData>(List<TData> datas) where T : MonoBehaviour, IBind<TData> where TData : ISaveAble, new()
        {
            T[] entities = FindObjectsByType<T>(FindObjectsSortMode.None);
            foreach (T entity in entities)
            {
                Debug.Log($"Attempting to bind: {entity}");
                TData data = datas.FirstOrDefault(d => d.ID == entity.ID);
                if (data == null)
                {
                    data = new TData { ID = entity.ID };
                    datas.Add(data);
                }
                entity.Bind(data);
            }
        }
        
        //Bind a single entity to gamedata
        public void Bind<T, TData>(T entity, TData data) where T : MonoBehaviour, IBind<TData> where TData : ISaveAble, new()
        {
            if (entity == null) return;
            Debug.Log($"Attempting to bind: {entity}");
            if (data == null)
            {
                data = new TData() { ID = entity.ID };
            }
            entity.Bind(data);
        }
        //bind an entity to a list in gamedata
        public void Bind<T, TData>(T entity, List<TData> data) where T : MonoBehaviour, IBind<TData> where TData : ISaveAble, new()
        {
            if (!entity) return;
            //Debug.Log($"Attempting to bind: {entity}");
            if (data == null)
            {
                return;
            }
            TData newData = new TData() { ID = entity.ID };
            data.Add(newData);
            entity.Bind(newData);
        }
        //helpful when loading in data, can sync it back up with saved ids if you don't want to throw away gamestate data
        public bool TryReBind<T, TData>(T entity, SerializableGuid guid, List<TData> data) where T : MonoBehaviour, IBind<TData> where TData : ISaveAble, new()
        {
            for (int i = data.Count-1; i >= 0; i--)
            {
                if (data[i].ID == guid)
                {
                    entity.Bind(data[i]);
                    return true;
                }
            }
            return false;
        }
        //Unbinds entity from an existing list 
        public void UnBind<TData>(SerializableGuid id, List<TData> data)  where TData : ISaveAble, new()
        {
            for (int i = data.Count-1; i >= 0; i--)
            {
                if (data[i].ID != id) continue;
                data.RemoveAt(i);
                return;
            }
        }
        
        public void NewGame()
        {
            gameData = new GameData
            {
                name = "New Game",
                currentLevel = "SampleScene",
                timePlayed = 0f,
            };
            LoadGame(gameData);
        }
        public void NewGame(string saveName)
        {
            gameData = new GameData
            {
                name = saveName,
                currentLevel = "SampleScene",
                timePlayed = 0f
            };
            LoadGame(gameData);
        }

        public void SaveGame()
        {
            gameData.timePlayed += Time.time - _startTime;
            _dataService.SaveData(gameData);
        }

        public async void LoadGame(string gameName)
        {
            await SceneManager.LoadSceneAsync(gameData.currentLevel);
            gameData = null;
            gameData = _dataService.LoadData(gameName);
            Debug.Log("Initializing Assets");
            foreach(KeyValuePair<string, ILoader> loader in _loaders)
            {
                loader.Value.LoadData(gameData);
            }
            OnSceneLoaded();
            _startTime = Time.time;
        }
        public async void LoadGame(GameData game)
        {
            gameData = game;
            await SceneManager.LoadSceneAsync(gameData.currentLevel);
            Debug.Log("Initializing Assets");
            foreach(KeyValuePair<string, ILoader> loader in _loaders)
            {
                loader.Value.LoadData(game);
            }
            OnSceneLoaded();
            _startTime = Time.time;
        }

        public void Reload()
        {
            gameData = _dataService.LoadData(gameData.name);
        }

        public void DeleteGame(string gameName)
        {
            _dataService.Delete(gameName);
        }
    }


}

[System.Serializable]
public class GameData
{
    public string name;
    public string currentLevel;
    public float timePlayed;
}

public interface ISaveAble
{
    SerializableGuid ID { get; set; }
}

public interface IBind<TData> where TData : ISaveAble
{
    SerializableGuid ID { get; set; }
    void Bind(TData data);
}

public interface ILoader  
{
    void LoadData(GameData data);
}