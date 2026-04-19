using UnityEngine;

public class RadioTower : MonoBehaviour
{
    Collider2D coll;

    private void Start()
    {
        coll = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<Signal>() is Signal signal)
        {
            G.player.AddResources(signal);
            Destroy(signal.gameObject);
            Debug.Log("destroy");
        }
    }
}
