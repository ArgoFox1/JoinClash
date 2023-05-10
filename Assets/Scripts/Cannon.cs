using UnityEngine;

public class Cannon : MonoBehaviour
{
    [SerializeField] private float camSpeed;

    public ParticleSystem cannonFireFx;

    public Transform cannonSpawnPos;

    public DataManager dataManager;
    public SpawnManager spawnManager;
    public EffectManager effectManager;

    private void FixedUpdate()
    {
        LookDirection();
        if(dataManager.canFire == true) { spawnManager.SpawnCannonBall(); }
    }
    private void LookDirection()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        float x = ray.direction.x * Time.deltaTime * dataManager.cannonRotateSpeed;
        transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(20, 180, 0), dataManager.cannonRotateSpeed * Time.deltaTime);
        transform.Rotate(Vector3.down * x * dataManager.cannonRotateSpeed * camSpeed);
    }
}
