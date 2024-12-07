using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateBlades : MonoBehaviour
{
    [SerializeField] private float rotationSpeed;
    [SerializeField] private GameObject MainPropeller;
    [SerializeField] private GameObject TailPropeller;
    
    void Update()
    {
        MainPropeller.transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
        TailPropeller.transform.Rotate(Vector3.right, rotationSpeed * Time.deltaTime);
    }
}
