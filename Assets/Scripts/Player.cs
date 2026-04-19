using UnityEngine;

public class Player : MonoBehaviour
{
    int currentResources;

    private void Awake()
    {
        G.player = this;
    }

    public void AddResources(Signal signal)
    {
        currentResources += signal.resources;
        Debug.Log("currentRes:" +  currentResources);
    }

}
