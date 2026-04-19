using NUnit.Framework;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class CirculeBuilder : MonoBehaviour
{
    [SerializeField] int numOfBuilds;
    [SerializeField] float radius;
    [SerializeField] GameObject spotPrefab;


    List<BuildSpot> spots = new List<BuildSpot>();

    private void Awake()
    {
        G.circuleBuilder = this;
    }
    private void Start()
    {
        CreateBuildSpot();
    }

    void CreateBuildSpot()
    {
        for (int i = 0; i < numOfBuilds; i++)
        {
            float angle = (360 / numOfBuilds) * i;
            float rad = angle * Mathf.Deg2Rad;

            Vector2 pos = new Vector2(Mathf.Cos(rad) * radius, Mathf.Sin(rad) * radius);

            Vector3 directionFromCenter = pos.normalized;

            Quaternion rotation = Quaternion.LookRotation(Vector3.forward, directionFromCenter);

            GameObject spot = Instantiate(spotPrefab, pos, rotation);
            //spot.transform.Rotate(Vector3.up * angle);
            spot.transform.SetParent(transform);

            BuildSpot spotScript = spot.GetComponent<BuildSpot>();
            spotScript.spotIndex = i;
            spotScript.isOccupied = false;

            spots.Add(spotScript);
        }
    }

    public void TryBuildOnSpot(int index, GameObject towerPrefab)
    {
        if (index >= 0 && index < spots.Count && !spots[index].isOccupied)
        {
            GameObject newTower = Instantiate(towerPrefab, spots[index].transform.position, spots[index].transform.rotation);
            spots[index].tower = newTower;
            spots[index].isOccupied = true;
            newTower.transform.SetParent(spots[index].transform);
            Debug.Log($"Построено на слоте {index}");
        }
        else
        {
            Debug.Log("Место занято или не существует");
        }

    }
}
