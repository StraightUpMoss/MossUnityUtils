using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

namespace MossUnityUtils.SaveSystem
{
    public class FileDataService : IDataService
    {
        private ISerializer _serializer;
        private string _dataPath;
        private string _fileExtension;

        public FileDataService(ISerializer serializer)
        {
            this._dataPath = Application.persistentDataPath;
            _fileExtension = "json";
            this._serializer = serializer;
        }

        public string GetDataPath()
        {
            return _dataPath + _fileExtension;
        }
        
        string GetPathToFile(string fileName)
        {
            return Path.Combine(_dataPath, string.Concat(fileName, ".", _fileExtension));
        }

        public GameData LoadData(string name)
        {
            string fileLocation = GetPathToFile(name);
            if (!File.Exists(fileLocation))
            {
                throw new IOException(
                    $"No saved GameData with name {name}");

            }

            return _serializer.Deserialize<GameData>(File.ReadAllText(fileLocation));
        }

        public void SaveData(GameData data, bool overwrite = true)
        {
            string fileLocation = GetPathToFile(data.name);
            if (!overwrite && File.Exists(fileLocation))
            {
                throw new IOException(
                    $"The file {data.name}.{_fileExtension} already exists and cannot be overwritten");

            }

            File.WriteAllText(fileLocation, _serializer.Serialize(data));
        }

        public void Delete(string name)
        {
            string fileLocation = GetPathToFile(name);
            if (File.Exists(fileLocation))
            {
                File.Delete(fileLocation);
            }
        }

        public void DeleteAll()
        {
            throw new System.NotImplementedException();
            //probs not worth adding bc it could delete important files
            // foreach (string fp in Directory.GetFiles(_dataPath))
            // {
            //     File.Delete(fp);
            // }
        }

        public IEnumerable<string> ListSaves()
        {
            
            List<FileInfo> sortedFiles = Directory.EnumerateFiles(_dataPath).Select(f => new FileInfo(f))
                .OrderBy(f => f.LastWriteTime)
                .ToList();
            sortedFiles.Reverse();
            foreach (FileInfo file in sortedFiles)
            {
                //Debug.Log(path);
                string path = file.FullName;
                Debug.Log(path);
                if (Path.GetExtension(path) != "."+ _fileExtension && Path.GetExtension(path) != _fileExtension) continue;
                Debug.Log(Path.GetFileNameWithoutExtension(path));
                yield return Path.GetFileNameWithoutExtension(path);
            }
        }
    }
}