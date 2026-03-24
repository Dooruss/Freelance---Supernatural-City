using UnityEngine;

[CreateAssetMenu(fileName = "Researchable", menuName = "Scriptable Objects/Researchable")]
public class Researchable : ScriptableObject
{
    public string ObjectName;
    public int PointCost;

    public bool IsUnlocked;
    public bool IsVisible;
}
