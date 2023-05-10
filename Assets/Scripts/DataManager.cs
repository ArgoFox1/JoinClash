using UnityEngine;
public class DataManager : MonoBehaviour
{

    public float cannonRotateSpeed;

    public float cannonBallSpeed;

    public bool isFinal;

    public bool canFire;

    public bool isMoving = false;

    public bool isLost;

    public bool isWon;

    public bool isNeutr;

    private void Start()
    {
        canFire = true;
    }
}
