using System.Collections;
using UnityEngine;

public class SignalGenerator : MonoBehaviour
{
    [SerializeField] GameObject signalPrefab;
    [SerializeField] float delay;
    bool wait = false;


    private void Update()
    {
        if( wait)
        {
            return;
        }
        StartCoroutine(GenerateSignal());
    }

    Vector3 GenRandomPos()
    {

        var randomPos = new Vector3(
            Random.Range(-20f, 20f),
            Random.Range(-15f, 15f)
            );

        if( randomPos.x > -3 && randomPos.x < 3 || randomPos.y > -3 && randomPos.y < 3)
        {
            GenRandomPos();
        }
        return randomPos;
    }

    IEnumerator GenerateSignal()
    {
        wait = true;
        Vector3 randomPos = GenRandomPos();

        G.objectPool.GetObject(randomPos, Quaternion.identity);

        yield return new WaitForSeconds(delay);
        wait = false;
    }
}
