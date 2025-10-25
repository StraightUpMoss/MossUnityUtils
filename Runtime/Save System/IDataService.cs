using System.Collections.Generic;

namespace MossUnityUtils.SaveSystem {
    public interface IDataService
    {
        public GameData LoadData(string name);
        public void SaveData(GameData data, bool overwrite = true);
        void Delete(string name);
        void DeleteAll();
        IEnumerable<string> ListSaves();
    }
}