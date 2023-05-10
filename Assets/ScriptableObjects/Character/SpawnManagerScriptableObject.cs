using UnityEngine;

[CreateAssetMenu(fileName = "SpawnScr",menuName = "SpawnScriptable")]
public class SpawnManagerScriptableObject : ScriptableObject
{
    public float speed;

    public string Tag;

    public Material material;

    public Quaternion firstRot;

    public enum CharacterTypes
    {
        Default,
        Friendly,
        Enemy
    }
    public CharacterTypes characterTypes;
    
}