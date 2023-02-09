using System;
using System.IO;
using Cysharp.Threading.Tasks;
using LifeGame.Services.GameData;
using Newtonsoft.Json;
using UnityEditor;
using UnityEngine;

namespace LifeGame.Services.PlayerData
{
    public class PlayerDataService : ServiceBase, IPlayerDataService
    {
        private const string SAVE_FILE_NAME = "Save.json";
        private string _path;
        private Data _data;
        public DataAccessProvider Data { get; private set; }
        private IGameDataService GameDataService => ServiceProvider.GameData;

        public override UniTask InitializeAsync()
        {
            _path = Path.Combine(Application.persistentDataPath, SAVE_FILE_NAME);
            return base.InitializeAsync();
        }

        public override UniTask StartAsync()
        {
            LoadData();
            return base.StartAsync();
        }


        public void LoadData()
        {
            _data = LoadDataInternal();
            var config = GameDataService.Config;

            Data = new DataAccessProvider(_data, config);
        }

        public void SaveData()
        {
            _data.QuitTime = DateTime.Now;
            File.WriteAllText(_path, JsonConvert.SerializeObject(_data, Formatting.Indented));
        }

        private Data LoadDataInternal()
        {
            try
            {
                if (File.Exists(_path))
                {
                    string dataString = File.ReadAllText(_path);
                    var jsonSettings = new JsonSerializerSettings
                    {
                        ObjectCreationHandling = ObjectCreationHandling.Replace
                    };
                    return JsonConvert.DeserializeObject<Data>(dataString, jsonSettings);
                }
            }
            catch (Exception)
            {
                return new Data();
            }

            return new Data();
        }

        private void OnApplicationQuit()
        {
            SaveData();
        }

        private void OnApplicationPause(bool pauseStatus)
        {
#if !UNITY_EDITOR
            SaveData();
#endif
        }

#if UNITY_EDITOR
        [MenuItem("Tools/Player Data/Delete save")]
        public static void DeleteSave()
        {
            var path = Path.Combine(Application.persistentDataPath, "Save.json");
            if (File.Exists(path))
            {
                File.Delete(path);
                Debug.Log("Deleted: " + path);
            }
            else
            {
                Debug.Log("File does not exist");
            }
        }

        [MenuItem("Tools/Player Data/Open save path")]
        public static void OpenSavePath()
        {
            if (Directory.Exists(Application.persistentDataPath))
                Application.OpenURL(Application.persistentDataPath);
        }

        [MenuItem("Tools/Player Data/Print save")]
        public static void PrintSave()
        {
            var path = Path.Combine(Application.persistentDataPath, "Save.json");
            if (File.Exists(path))
                Debug.Log(File.ReadAllText(path));
        }
#endif
    }
}