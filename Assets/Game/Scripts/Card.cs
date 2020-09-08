using UnityEngine;

[CreateAssetMenu(fileName = "New card", menuName = "Card")]
public class Card : ScriptableObject
{
    public new string name;
    public Sprite sprite;
    public float size;
}
