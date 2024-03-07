using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spine : MonoBehaviour
{
    [SerializeField] private float speed = 180f;
    private void Update()
    {
        transform.Rotate(0, 0, speed * Time.deltaTime);
    }
}
