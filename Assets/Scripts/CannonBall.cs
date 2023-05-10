using UnityEngine;
using System.Collections;

public class CannonBall : MonoBehaviour
{
    public DataManager dataManager;
    public PoolManager poolManager;

    public SpawnManager spawnManager;
    public EffectManager effectManager;

    private void Update()
    {
        transform.position += transform.forward * Time.deltaTime * dataManager.cannonBallSpeed;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            StartCoroutine(Cooldown4EnemyImpact(other));
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor") && collision.gameObject.tag != "Enemy") { StartCoroutine(nameof(Cooldown4CannonImpactItself)); }
    }
    IEnumerator Cooldown4EnemyImpact(Collider enemy)
    {
        effectManager.SpawnEffect(transform, poolManager.deadStickFxs[0],effectManager.audioManager.cannonExplosionSound);
        effectManager.SpawnEffect(transform, poolManager.cannonBallExplosionFxs[0], effectManager.audioManager.deadSound);
        poolManager.deadEnemies.Add(enemy.gameObject);
        yield return new WaitForSeconds(0.1f);
        poolManager.cannonBalls.Add(gameObject);
    }
    IEnumerator Cooldown4CannonImpactItself()
    {
        transform.rotation = Quaternion.Euler(0, spawnManager.ballRot.eulerAngles.y, spawnManager.ballRot.eulerAngles.z);
        spawnManager.cannonBalls.Remove(gameObject);
        yield return new WaitForSeconds(2.9f);
        effectManager.SpawnEffect(transform, effectManager.cannonBallExplosionFX, effectManager.audioManager.cannonExplosionSound);
        yield return new WaitForSeconds(0.1f);
        poolManager.cannonBalls.Add(gameObject);
    }
}
