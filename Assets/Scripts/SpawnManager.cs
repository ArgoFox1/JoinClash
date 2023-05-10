using System.Collections.Generic;
using UnityEngine;
using System.Collections;
public class SpawnManager : MonoBehaviour
{

    public Quaternion ballRot;

    public Cannon cannon;
    public EffectManager effectManager;
    public LevelManager levelManager;
    public PoolManager poolManager;
    public DataManager dataManager;

    public List<Transform> enemyPossies;
    public List<GameObject> cannonBalls;
    public List<GameObject> defaultSticks;
    public List<Character> friendlySticks;
    public List<GameObject> enemySticks;

    private void Start()
    {
        StartCoroutine(nameof(BonusLevelSpawnment));
    }
    private void Update()
    {
        dataManager.isLost = (friendlySticks.Count == 0 && enemySticks.Count != 0) ? true : false;
        dataManager.isWon = (friendlySticks.Count != 0 && enemySticks.Count == 0) ? true : false;
        dataManager.isNeutr = (friendlySticks.Count == 0 && enemySticks.Count == 0) ? true : false;
        if (levelManager.levels != LevelManager.Levels.BonusLevel && levelManager.levels != LevelManager.Levels.Menu )
        {
            foreach (GameObject dSticks in defaultSticks)
            {
                dSticks.gameObject.tag = "Default";
                dSticks.SetActive(true);
            }
            foreach (Character friendly in friendlySticks)
            {
                friendly.gameObject.SetActive(true);
                friendly.spawnScripatable = friendly.friendlyScr;
                friendly.gameObject.tag = "Friendly";
            }
            foreach (GameObject enemy in enemySticks)
            {
                enemy.gameObject.SetActive(true);
                enemy.tag = "Enemy";
            }
        }
    }
    private IEnumerator BonusLevelSpawnment()
    {
        yield return StartCoroutine(nameof(Wait));
        if (cannon.enabled == true && cannon.gameObject.activeInHierarchy == true)
        {
            for (int i = 0; i < enemySticks.Count; i++)
            {
                enemySticks[i].transform.position = enemyPossies[i].position;
                enemySticks[i].transform.rotation = enemyPossies[i].rotation;
                enemySticks[i].SetActive(true);
                enemySticks[i].tag = "Enemy";
            }
        }
    }
    public void SpawnCannonBall()
    {
        if(levelManager.levels == LevelManager.Levels.BonusLevel)
        {
            dataManager.canFire = false;
            effectManager.SpawnEffect(cannon.cannonSpawnPos, cannon.cannonFireFx,effectManager.audioManager.cannonFireSound);
            cannonBalls[0].transform.position = cannon.cannonSpawnPos.position;
            cannonBalls[0].transform.rotation = cannon.cannonSpawnPos.rotation;
            ballRot = cannonBalls[0].transform.rotation;
            cannonBalls[0].SetActive(true);
            cannonBalls.RemoveAt(0);
            StartCoroutine(nameof(Cooldown4CannonFire));
        }
    }
    IEnumerator Cooldown4CannonFire()
    {        
        yield return new WaitForSeconds(3f);
        dataManager.canFire = true;
    }
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.5f);
    }
}
