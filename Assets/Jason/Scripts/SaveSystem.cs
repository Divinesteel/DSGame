using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class BinaryCharacterSaver : MonoBehaviour
{
    public static WorldData worldData;
    const string folderName = "BinaryWorldData";
    const string fileExtension = ".dat";

    static void SaveState(WorldData data, string path)
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();

        using (FileStream fileStream = File.Open(path, FileMode.OpenOrCreate))
        {
            binaryFormatter.Serialize(fileStream, data);
        }
    }

    static WorldData LoadState(string path)
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();

        using (FileStream fileStream = File.Open(path, FileMode.Open))
        {
            return (WorldData)binaryFormatter.Deserialize(fileStream);
        }
    }

    static string[] GetFilePaths()
    {
        string folderPath = Path.Combine(Application.persistentDataPath, folderName);

        return Directory.GetFiles(folderPath, fileExtension);
    }

    public static void SaveGame()
    {
        string folderPath = Path.Combine(Application.persistentDataPath, folderName);
        if (!Directory.Exists(folderPath))
            Directory.CreateDirectory(folderPath);

        string dataPath = Path.Combine(folderPath, fileExtension);
        SaveState(worldData, dataPath);
    }

    public static void LoadGame()
    {
        string[] filePaths = GetFilePaths();

        if (filePaths.Length > 0)
        worldData = LoadState(filePaths[0]);
    }
}
