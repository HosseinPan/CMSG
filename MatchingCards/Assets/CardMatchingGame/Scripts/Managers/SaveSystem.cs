using System.Collections.Generic;
using UnityEngine;

public class SaveSystem : MonoBehaviour
{
    private const string LEVEL_DATA_KEY = "MY_LEVEL_DATA_KEY";

    private void OnEnable()
    {
        EventBus.OnSaveLevel += SaveLevelData;
        EventBus.OnRequestLoadLevel += LoadLevelData;
    }

    private void OnDisable()
    {
        EventBus.OnSaveLevel -= SaveLevelData;
        EventBus.OnRequestLoadLevel -= LoadLevelData;
    }

    private void LoadLevelData()
    {
        string json = PlayerPrefs.GetString(LEVEL_DATA_KEY, "");

        if (string.IsNullOrEmpty(json))
        {
            EventBus.RaiseLoadedLevel(null);
        }
        else 
        {
            LevelData data = JsonUtility.FromJson<LevelData>(json);
            EventBus.RaiseLoadedLevel(data);
        }
    }

    private void SaveLevelData(LevelData data)
    {
        if (data == null)
        {
            PlayerPrefs.SetString(LEVEL_DATA_KEY, "");
        }
        else 
        {
            string json = JsonUtility.ToJson(data);
            PlayerPrefs.SetString(LEVEL_DATA_KEY, json);            
        }
        PlayerPrefs.Save();
    }

}

[System.Serializable]
public class LevelData
{
    public int columns;
    public int rows;
    public List<int> cardIds;
    public List<int> cardStates;
}


