using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveSystem
{
    public static void SaveData (GameMaster gameMaster)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/Data.fun";
        FileStream stream = new FileStream(path, FileMode.Create);

        gameMasterData data = new gameMasterData(gameMaster);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static gameMasterData LoadData()
    {
        string path = Application.persistentDataPath + "/Data.fun";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            gameMasterData data = formatter.Deserialize(stream) as gameMasterData;
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("Save fule not found in " + path);
            return null;
        }
    }
}
