using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/potions")]
public class ScriptablePotion : ScriptableObject
{
    public int Id;
    public string Name;
    public string Description;
    public string Effect;
    public Sprite Icon;
    public GameObject Prefab;
    public int Quantity;
}
