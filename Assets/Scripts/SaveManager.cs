using System.Diagnostics;
using System.IO;
using UnityEngine;
using Debug = UnityEngine.Debug;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveManager : Singleton<SaveManager>
{
    float overallTime = 0.0f;

    private string pathBin;
    private string pathJSON;

    public bool useBinary = true;
    public GameSaveData saveData;

    public struct GameSaveData
    {
        public static float timeSinceLastSave;
    }

    public void Start()
    {
        GameSaveData.timeSinceLastSave = 0.0f;
        LoadSettings();

        pathBin = Path.Combine(Application.persistentDataPath, "save.bin");
        pathJSON = Path.Combine(Application.persistentDataPath, "save.JSON");
    }

    public void Update()
    {
        GameSaveData.timeSinceLastSave += Time.deltaTime;
    }

    public void SaveSettings()
    {
        overallTime += GameSaveData.timeSinceLastSave;
        Debug.Log("Saving overall time value: " + overallTime);
        PlayerPrefs.SetFloat("OverallTime", overallTime);
        GameSaveData.timeSinceLastSave = 0.0f;

        if(useBinary)
        {
            FileStream file = new FileStream(pathBin, FileMode.OpenOrCreate);
            BinaryFormatter binFormat = new BinaryFormatter();
            binFormat.Serialize(file, saveData);
            file.Close();
        }
    }

    public void LoadSettings()
    {
        overallTime = PlayerPrefs.GetFloat("OverallTime", 0.0f);
        Debug.Log("Loaded overall time: " + overallTime);

        if(useBinary && File.Exists(pathBin))
        {
            FileStream file = new FileStream(pathBin, FileMode.Open);
            BinaryFormatter binFormat = new BinaryFormatter();
            saveData = (GameSaveData)binFormat.Deserialize(file);
            file.Close();
        }
    }
}


