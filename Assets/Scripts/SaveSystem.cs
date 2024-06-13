// using System.IO;
// using UnityEngine;

// public static class SaveSystem
// {
//     private static readonly string path = Application.persistentDataPath + "/player.json";

//     public static void SavePlayer(GameManager gameManager)
//     {
//         try
//         {
//             PlayerData data = new PlayerData(gameManager);
//             string json = JsonUtility.ToJson(data, true);
//             File.WriteAllText(path, json);
//         }
//         catch (IOException e)
//         {
//             Debug.LogError("Failed to save player data: " + e.Message);
//         }
//     }

//     public static PlayerData LoadPlayer()
//     {
//         if (File.Exists(path))
//         {
//             try
//             {
//                 string json = File.ReadAllText(path);
//                 PlayerData data = JsonUtility.FromJson<PlayerData>(json);
//                 return data;
//             }
//             catch (IOException e)
//             {
//                 Debug.LogError("Failed to load player data: " + e.Message);
//                 return null;
//             }
//         }
//         else
//         {
//             Debug.LogError("Save file not found in " + path);
//             return null;
//         }
//     }
// }
using System.IO;
using UnityEngine;

public static class SaveSystem
{
    private static readonly string path = Application.persistentDataPath + "/player.json";

    public static void SavePlayer(GameManager gameManager)
    {
        try
        {
            PlayerData data = new PlayerData(gameManager);
            string json = JsonUtility.ToJson(data, true);
            File.WriteAllText(path, json);
            Debug.Log("Game saved to " + path);
        }
        catch (IOException e)
        {
            Debug.LogError("Failed to save player data: " + e.Message);
        }
    }

    public static PlayerData LoadPlayer()
    {
        if (File.Exists(path))
        {
            try
            {
                string json = File.ReadAllText(path);
                PlayerData data = JsonUtility.FromJson<PlayerData>(json);
                Debug.Log("Game loaded from " + path);
                return data;
            }
            catch (IOException e)
            {
                Debug.LogError("Failed to load player data: " + e.Message);
                return null;
            }
        }
        else
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }
}
