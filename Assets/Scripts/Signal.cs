using System.Collections;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Signal : MonoBehaviour
{
    public int resources;
    Vector2 dir;
    [SerializeField] float timeOfLive;

    private void Start()
    {
        dir = G.player.transform.position + new Vector3(Random.Range(-2, 2), Random.Range(-2, 2)) - transform.position;
    }

    private void Update()
    {
        //Vector3 target = G.player.transform.position + new Vector3(Random.Range(-2,2), Random.Range(-2,2));
        MoveTo(dir);
    }

    void MoveTo(Vector3 target)
    {
        transform.Translate(dir * Time.deltaTime);
    }


    IEnumerator Alive()
    {
        yield return new WaitForSeconds(timeOfLive);
        G.objectPool.ReturnObject(gameObject);
    }

}
