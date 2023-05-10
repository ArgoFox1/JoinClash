using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{

    public List<ParticleSystem> deadStickFxs;
    public List<ParticleSystem> cannonBallExplosionFxs;

    public List<Character> deadFriendies;
    public List<GameObject> deadEnemies;
    public List<GameObject> cannonBalls;

    public DataManager dataManager;
    public SpawnManager spawnManager;

    private void Start()
    {
        foreach (GameObject stick in deadEnemies)
        {
            spawnManager.enemySticks.AddRange(deadEnemies);
        }
        foreach (Character stick in deadFriendies)
        {
            spawnManager.friendlySticks.AddRange(deadFriendies);
        }
    }

    private void Update()
    {
        ImpactedCannonBalls();
        if(dataManager.isFinal != true)
        {
            foreach (GameObject stick in deadEnemies)
            {
                stick.SetActive(false);
                spawnManager.enemySticks.Remove(stick);
            }
            foreach (Character stick in deadFriendies)
            {
                stick.gameObject.SetActive(false);
                spawnManager.friendlySticks.Remove(stick);
            }
        }
        else
        {
            foreach (GameObject stick in deadEnemies)
            {
                stick.SetActive(false);
                spawnManager.enemySticks.Remove(stick);
            }
            foreach (Character stick in deadFriendies)
            {
                stick.gameObject.SetActive(false);
                spawnManager.friendlySticks.Remove(stick);
            }
        }
    }

    private void ImpactedCannonBalls()
    {
        if(cannonBalls.Count != 0)
        {
            cannonBalls[0].SetActive(false);
            spawnManager.cannonBalls.Add(cannonBalls[0]);
            cannonBalls.RemoveAt(0);
        }
    }

    public  delegate TOutput Converter<in TInput, out TOutput>(TInput ýnput);
}
