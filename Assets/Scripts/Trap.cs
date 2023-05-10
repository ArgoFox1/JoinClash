using UnityEngine;
using DG.Tweening;
public class Trap : MonoBehaviour
{
    public PoolManager poolManager;
    public EffectManager effectManager;
    
    public TrapScriptableObject trapScriptable;

    [SerializeField] private float endValue;
    [SerializeField] private float trapDuration;

    private void Start()
    {
        this.name = trapScriptable.trapName;
        DOTween.Init();
        InitializeTrapType();
    }
    private void Update()
    {
        if(this.name == "SpikeRoller") { SpikeSpin(); }
        if(this.name == "BladeRoller") { BladeSpin(); }
    }
    private void OnTriggerEnter(Collider ot)
    {
        if (ot.gameObject.CompareTag("Friendly"))
        {
            effectManager.SpawnEffect(ot.transform, poolManager.deadStickFxs[0],effectManager.audioManager.deadSound);
            poolManager.deadFriendies.Add(ot.gameObject.GetComponent<Character>());
        }       
    }
    public void InitializeTrapType()
    {
        switch (trapScriptable.trapTypes)
        {
            case TrapScriptableObject.TrapTypes.Static:
                transform.position = transform.position;
                break;
            case TrapScriptableObject.TrapTypes.Dynamic:
                TrapMovement();
                break;
        }
    }
    private void TrapMove()
    {
        transform.DOMoveX(endValue, trapDuration).SetLoops(-1, LoopType.Yoyo);
    }
    private void SpikeSpin()
    {
        float z = transform.rotation.z;
        z += Time.time * 500;
        transform.rotation = Quaternion.Euler(-90, 0, z);
    }
    private void BladeSpin()
    {
        float y = transform.rotation.y;
        y += Time.time * 500;
        transform.rotation = Quaternion.Euler(0, y, 0);
    }
    public void TrapMovement()
    {
        if (this.name == "Wall") { TrapMove(); }
        else if (this.name == "SpikeRoller")
        {
            TrapMove();
        }
        else if (this.name == "BladeRoller")
        {
            TrapMove();
        }
    }
}
