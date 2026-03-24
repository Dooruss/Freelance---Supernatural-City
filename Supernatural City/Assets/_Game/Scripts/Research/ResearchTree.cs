using UnityEngine;

public class ResearchTree : MonoBehaviour
{
    private GameManager gameManager;

    [SerializeField] private GameObject[] AllUnlockables;

    void Start()
    {
        gameManager = FindFirstObjectByType<GameManager>();
    }

    public void Unlock(Researchable Object)
    {
        if (gameManager.Research_Points >= Object.PointCost && Object.IsVisible)
        {
            gameManager.Research_Points -= Object.PointCost;
            Object.IsUnlocked = true;
            for (int i = 0; i < AllUnlockables.Length; i++)
            {
                string[] x = Object.ObjectName.Split("(");
                if (x[0] == AllUnlockables[i].name)
                {
                    AllUnlockables[i].SetActive(true);
                }
            }
        }
    }
}
