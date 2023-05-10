using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public ADManager ADManager;
    public SpawnManager spawnManager;
    public TweenManager tweenManager;
    public DataManager dataManager;
    public LevelManager levelManager;

    public Image level1PauseImage;
    public Image level2PauseImage;
    public Image level1WonImage;
    public Image level1LostImage;
    public Image bonusLevelWonImage;

    private bool isLevel1;

    private void Start()
    {
        ADManager = ADManager.Instance;
        Application.targetFrameRate = 60;
    }
    private void Update()
    {
        isLevel1 = (levelManager.levels == LevelManager.Levels.Level1) ? true : false;
        if (dataManager.isFinal == true) { tweenManager.DoorOpenTween(); }
        if(dataManager.isWon == true) { level1WonImage.gameObject.SetActive(isLevel1); bonusLevelWonImage.gameObject.SetActive(!isLevel1); }
        if(level1PauseImage.gameObject.activeInHierarchy == true || level2PauseImage.gameObject.activeInHierarchy == true) { Time.timeScale = 0; }
    }
    
    public void NextLevel()
    {
        levelManager.levels = LevelManager.Levels.BonusLevel;
    }
    public void GoToMainMenu()
    {
        levelManager.levels = LevelManager.Levels.Menu;
    }
    public void LoadScene()
    {
        SceneManager.LoadScene(0);
        ADManager.interstitialAd.Show();
    }
    public void Settings()
    {
        level1PauseImage.gameObject.SetActive(isLevel1);
        level2PauseImage.gameObject.SetActive(!isLevel1);
        Time.timeScale = 0;
        ADManager.interstitialAd.Show();
    }
    public void Continue()
    {
        Time.timeScale = 1;
        level1PauseImage.gameObject.SetActive(false);
        level2PauseImage.gameObject.SetActive(false);
    }
    public void Play()
    {
        levelManager.levels = LevelManager.Levels.Level1;
    }
    public void Exit()
    {
        Application.Quit();
    }
    public void Support()
    {
        string url = $"https://twitter.com/FOXYYGAMESS";
        Application.OpenURL(url);
        ADManager.interstitialAd.Show();
    }
}
