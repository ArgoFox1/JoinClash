using UnityEngine;

public class EffectManager : MonoBehaviour
{
    public ParticleSystem cannonBallExplosionFX;

    public AudioManager audioManager;
    public PoolManager poolManager;

    public void SpawnEffect(Transform pos , ParticleSystem p,AudioClip chosedClip)
    {
        p.gameObject.SetActive(true);
        p.gameObject.transform.position = pos.position;
        p.Play();
        audioManager.SpawnSound(chosedClip);
    }
}
