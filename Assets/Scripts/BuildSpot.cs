using UnityEngine;

public class BuildSpot : MonoBehaviour
{
    public GameObject tower;
    public int spotIndex;
    public bool isOccupied;

    void Build(GameObject prefab)
    {
        Instantiate(prefab);
    }

    private void OnMouseDown()
    {
        G.circuleBuilder.TryBuildOnSpot(spotIndex, tower);
    }
}
