using System.Drawing;
using UnityEngine;

public class AxeSpin : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    public Vector3 rotationSpeed = new(0, 0, 500);

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(rotationSpeed * Time.deltaTime);
    }
}
