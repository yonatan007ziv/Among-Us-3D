using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationState : MonoBehaviour
{
    public float deltaSpeed;
    private Animator anim;
    private Vector3 lastPosition;
    private float deltaX;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        Vector3 transformPosition = transform.position;
        deltaX = (transformPosition - lastPosition).magnitude;
        deltaSpeed = deltaX / Time.deltaTime;
        anim.SetBool("isMoving", deltaSpeed >= 0.1f);
        lastPosition = transformPosition;
    }
}