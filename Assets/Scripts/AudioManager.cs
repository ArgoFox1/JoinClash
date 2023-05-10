using UnityEngine;
public class AudioManager : MonoBehaviour
{

    public AudioSource soundFolder;

    public AudioClip cannonExplosionSound;
    public AudioClip cannonFireSound;
    public AudioClip deadSound;
    public AudioClip lostLaughSound;

    public void SpawnSound(AudioClip chosedClip)
    {
        soundFolder.PlayOneShot(chosedClip);
    }
}
