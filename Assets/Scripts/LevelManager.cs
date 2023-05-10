using UnityEngine;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour
{
    public List<GameObject> stages;
    public DataManager dataManager;
    private GameObject currentStage;

    public int stageCount = 1;

    private void Start()
    {
        stageCount = PlayerPrefs.GetInt("StageCount");
        levels = (Levels)System.Enum.Parse(typeof(Levels), PlayerPrefs.GetString("Level"));
    }
    public enum Levels
    {
        Menu,
        Level1,
        BonusLevel,
    }
    public  Levels levels;
    private void Update()
    {
        ActiveScene(); 
        switch (levels)
        {
            case Levels.Menu:
                stageCount = 1;
                break;
            case Levels.Level1:
                stageCount = 2;
                break;
            case Levels.BonusLevel:
                stageCount = 3;
                break;
            default: stageCount = 1;
                levels = Levels.Menu;
                PlayerPrefs.SetInt("StageCount", stageCount);
                break;
        }
    }
    private void ActiveScene()
    {
        currentStage = stages[stageCount];
        currentStage.SetActive(true);
        PlayerPrefs.SetString("Level", levels.ToString());
        PlayerPrefs.SetInt("StageCount", stageCount);
        PlayerPrefs.Save();
        for (int i = 0; i < stages.Count; i++)
        {
            if(currentStage != stages[i].gameObject) { stages[i].SetActive(false); }
        }
    }
}
