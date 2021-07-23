using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereOffsetFollower : MonoBehaviour
{
    [SerializeField] private GameObject car;

    private Vector3 _offset;
    // Start is called before the first frame update
    void Start()
    {
        _offset = transform.position - car.transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = car.transform.position + _offset;
    }
}
