using System.Drawing;
using UnityEngine;

public class AxeSpin : MonoBehaviour
{   
    public float rotationSpeed;
    private Vector3 rotationVector = new(0, 0, 100);

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(rotationVector * rotationSpeed * Time.deltaTime);
    }
}
