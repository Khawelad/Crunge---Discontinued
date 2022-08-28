using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class ItemInfo : ScriptableObject
{
    public string Name = "New Item";
    public string Lore = "will literally kill you";
    public Sprite Icon = null;
    public bool IsDefaultItem = false;

}
