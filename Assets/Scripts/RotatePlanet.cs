using UnityEngine;

public class RotatePlanet : MonoBehaviour
{
    [SerializeField]  float speedRotate = 1f;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            speedRotate = Mathf.Abs(speedRotate);
        }
        else if(Input.GetKeyDown(KeyCode.A))
        {
            speedRotate = -speedRotate;
        }
        transform.rotation *= Quaternion.Euler(0, 0, -speedRotate);
    }
}
