using System.Diagnostics;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;
using System.Runtime.Serialization.Formatters.Binary;

public struct GameSaveData
{
    public static float timeSinceLastSave;
    public float masterVolume;
}

public class SaveManager : Singleton<SaveManager>
{
    public GameSaveData gameSaveData;

    float overallTime = 0.0f;

    private string pathBin;
    private string pathJSON;

    public bool useBinary = true;

    public void Start()
    {
        GameSaveData.timeSinceLastSave = 0.0f;
        LoadSettings();

        pathBin = Path.Combine(Application.persistentDataPath, "save.bin");
        pathJSON = Path.Combine(Application.persistentDataPath, "save.JSON");

        gameSaveData.masterVolume = AudioListener.volume;
        LoadSettings();
    }

    public void Update()
    {
        GameSaveData.timeSinceLastSave += Time.deltaTime;
    }

    public void SaveSettings()
    {
        GameSaveData.timeSinceLastSave = 0.0f;
        overallTime += GameSaveData.timeSinceLastSave;
        Debug.Log("Saving overall time value: " + overallTime);
        PlayerPrefs.SetFloat("OverallTime", overallTime);

        if(useBinary)
        {
            FileStream file = new FileStream(pathBin, FileMode.OpenOrCreate);
            BinaryFormatter binFormat = new BinaryFormatter();
            binFormat.Serialize(file, gameSaveData);
            file.Close();
        }
        else
        {
            string saveData = JsonUtility.ToJson(gameSaveData);
            File.WriteAllText(pathJSON, saveData);
        }
    }

    public void LoadSettings()
    {
        if(useBinary && File.Exists(pathBin))
        {
            FileStream file = new FileStream(pathBin, FileMode.Open);
            BinaryFormatter binFormat = new BinaryFormatter();
            gameSaveData = (GameSaveData)binFormat.Deserialize(file);
            file.Close();
            ApplySettings();
        }
        else if(!useBinary && File.Exists(pathJSON))
        {
            ApplySettings();
            string saveData = File.ReadAllText(pathJSON);
            gameSaveData = JsonUtility.FromJson<GameSaveData>(saveData);
        }
        else
        {
            GameSaveData.timeSinceLastSave = 0.0f;
            AudioListener.volume = gameSaveData.masterVolume;
        }
    }

    public void ApplySettings()
    {
        AudioListener.volume = gameSaveData.masterVolume;
    }
}


