using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "Scriptable Objects/ItemData")]
public class ItemData : ScriptableObject
{
    public string Name;
    [TextArea]
    public string Description;
    public GameObject itemObject;
}
