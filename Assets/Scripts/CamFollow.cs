using UnityEngine;
using DG.Tweening;
public class CamFollow : MonoBehaviour
{
    public GameManager gameManager;
    public LevelManager levelManager;
    public DataManager dataManager;
    public TweenManager tweenManager;
    public SpawnManager spawnManager;

    public Transform castlePos;
    public Camera cam;
    public AudioListener listener;
    [SerializeField] Vector3 distance;

    private bool isActive;

    private void Awake()
    {
        DOTween.SetTweensCapacity(500, 250);
        DOTween.Init();
    }
    private void LateUpdate()
    {
        isActive = (levelManager.levels != LevelManager.Levels.BonusLevel) ? true : false;
        cam.enabled = isActive;
        listener.enabled = isActive;
        TheFollowing();
    }
    private void TheFollowing()
    {
        if (dataManager.isMoving == true)
        {
            if (spawnManager.friendlySticks.Count != 0)
            {
                transform.DOMove(spawnManager.friendlySticks[0].transform.position + distance, tweenManager.defaultCamFollowDuration);
            }
            else if (dataManager.isLost == true)
            {
                transform.DOMove(spawnManager.enemySticks[0].transform.position + distance, tweenManager.lostCamFollowDuraiton).OnComplete(() => { gameManager.level1LostImage.gameObject.SetActive(true); });
            }
        }
        if (dataManager.isNeutr == true )
        {
            dataManager.isMoving = false;
            transform.DOMove(castlePos.position + new Vector3(0, 15, -100), tweenManager.neutralizedCamFollowDuration).OnComplete(() => { transform.DORotateQuaternion(Quaternion.Euler(0, 0, 0), tweenManager.neutralizedCamFollowDuration); gameManager.level1LostImage.gameObject.SetActive(true); });
        }
    }
}
