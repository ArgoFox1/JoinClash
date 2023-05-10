using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;
using System;

public class Character : MonoBehaviour 
{
    public SpawnManagerScriptableObject spawnScripatable;
    public SpawnManagerScriptableObject friendlyScr;

    public Renderer _renderer;
    public NavMeshAgent cAgent;
    public Animator animator;
    public NavMeshAgent agent;

    public LevelManager levelManager;
    public SpawnManager spawnManager;
    public PoolManager poolManager;
    public DataManager dataManager;
    public EffectManager effectManager;

    private int friendlyCount;
    private int enemyCount;

    private void Update()
    {
        friendlyCount = spawnManager.friendlySticks.Count - 1;
        enemyCount = spawnManager.enemySticks.Count - 1;
        _renderer.sharedMaterial = spawnScripatable.material;
        cAgent.speed = spawnScripatable.speed;
        var mytuple = Tuple.Create(spawnManager.enemySticks,spawnManager.friendlySticks);
        CombineColor();
        if (this.tag == "Friendly" && dataManager.isFinal != true)
        {
            Movement();
            LookDirection();
        }
        if (this.tag == "Enemy" && spawnManager.friendlySticks.Count != 0)
        {
            ForwardToStick(enemyCount, spawnManager.friendlySticks[friendlyCount].gameObject, spawnManager.enemySticks[0].gameObject);
        }
        Final(mytuple);
        BonusLevel();
    }
    private void OnTriggerEnter(Collider ot)
    {
        if (ot.gameObject.CompareTag("Default") && this.tag == "Friendly")
        {
            spawnManager.friendlySticks.Add(ot.gameObject.GetComponent<Character>());
            spawnManager.defaultSticks.Remove(ot.gameObject);
        }
        else if (ot.gameObject.CompareTag("Enemy") && this.tag == "Friendly")
        {
            effectManager.SpawnEffect(transform, poolManager.deadStickFxs[0],effectManager.audioManager.deadSound);
            poolManager.deadEnemies.Add(ot.gameObject);
            poolManager.deadFriendies.Add(this);
        }
        else if (ot.gameObject.CompareTag("Final") && this.tag == "Friendly") { dataManager.isFinal = true; transform.rotation = Quaternion.Euler(0, 0, 0); }
        else if(levelManager.levels == LevelManager.Levels.BonusLevel && ot.gameObject.CompareTag("Castle"))
        {
            effectManager.SpawnEffect(transform, poolManager.deadStickFxs[0], effectManager.audioManager.deadSound);
            poolManager.deadEnemies.Add(gameObject);
        }
    }
    private void BonusLevel()
    {
        if(levelManager.levels == LevelManager.Levels.BonusLevel)
        {
            agent.enabled = false;
            animator.SetBool("Run", true);
            transform.position += transform.forward * Time.deltaTime * 6;
        }
    }
    private void Final(Tuple<List<GameObject> , List<Character>> mytuple)
    {
        if (dataManager.isFinal == true && spawnManager.friendlySticks.Count != 0 && spawnManager.enemySticks.Count != 0)
        {
            animator.SetBool("Run", true);
            agent.enabled = false;
            foreach (GameObject enemy in mytuple.Item1)
            {
                enemy.transform.position = Vector3.Lerp(enemy.transform.position, spawnManager.friendlySticks[friendlyCount].transform.position, Time.deltaTime / 8);
            }
            foreach (Character friendly in mytuple.Item2)
            {
                friendly.transform.position += transform.forward * Time.deltaTime * 2;
            }
        }
        else if (dataManager.isLost == true || dataManager.isWon == true)
        {
            animator.SetBool("Run", false);
            animator.SetBool("Dance", true);
            dataManager.isMoving = true;
        }
    }
    private void CombineColor()
    {
        Color result = new Color(0, 0, 0, 0);
        List<Color> colors = new List<Color> { spawnScripatable.material.color, friendlyScr.material.color };
        foreach (Color c in colors)
        {
            result += c;
        }
        result /= colors.Count;
        var main = poolManager.deadStickFxs[0].main;
        main.startColor = result;
    }
    private void ForwardToStick(int count,GameObject target,GameObject ourObject)
    {
        if (count >= 0)
        {
            if (gameObject == ourObject.gameObject)
            {
                float distance = Vector3.Distance(ourObject.transform.position, target.transform.position);
                if (distance <= 40)
                {
                    agent.enabled = false;
                    animator.SetBool("Run", true);
                    ourObject.transform.LookAt(new Vector3(target.gameObject.transform.position.x, 8, target.gameObject.transform.position.z));
                    transform.position = Vector3.Lerp(ourObject.transform.position, target.transform.position, Time.deltaTime * 2);
                }
            }
        }
    }
    private void Movement()
    {
        if (Input.GetMouseButton(0) && dataManager.isFinal != true)
        {
            dataManager.isMoving = true;
            cAgent.nextPosition += transform.forward * Time.deltaTime * spawnScripatable.speed;
            animator.SetBool("Run", true);
        }
        if (Input.GetMouseButtonUp(0))
        {
            dataManager.isMoving = false;
            cAgent.nextPosition = transform.position;
            animator.SetBool("Run", false);
        }
    }
    private void LookDirection()
    {
        if (Input.GetMouseButton(0) && dataManager.isFinal != true)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            float x = ray.direction.x * Time.deltaTime * cAgent.speed * 20;
            transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(0, x, 0), cAgent.speed * Time.deltaTime);
            transform.Rotate(Vector3.up * x * cAgent.speed);
        }
    }
}
