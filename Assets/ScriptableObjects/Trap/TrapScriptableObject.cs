using UnityEngine;

[CreateAssetMenu(fileName = "TrapScr", menuName = "TrapScriptable")]
public class TrapScriptableObject : ScriptableObject
{

    public string trapName;

    public enum TrapTypes
    {
       Static,
       Dynamic,
    }
    public TrapTypes trapTypes;
}
