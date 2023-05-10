using UnityEngine;
using DG.Tweening;
public class TweenManager : MonoBehaviour
{
    public GameObject leftDoor;

    public GameObject rightDoor;

    public float doorDuration;

    public float lostCamFollowDuraiton;

    public float defaultCamFollowDuration;

    public float neutralizedCamFollowDuration;

    public bool isDoorOpen = false;

    public void DoorOpenTween()
    {
        if(isDoorOpen == false)
        {
            isDoorOpen = true;
            leftDoor.transform.DOLocalMoveX(-2.4f, doorDuration).OnComplete(() => { leftDoor.transform.DOLocalMoveX(-0.770f, doorDuration); });
            rightDoor.transform.DOLocalMoveX(2.4f, doorDuration).OnComplete(() => { rightDoor.transform.DOLocalMoveX(0.770f, doorDuration); });
        }
    }
}
