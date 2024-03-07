using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MoveState
{
    State1, State2
}
public class EagleMove : MonoBehaviour
{
    private string animationState = "State";
    public Animator animator { get; set; }
    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>(); 
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            animator.SetInteger(animationState, (int)MoveState.State1);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            animator.SetInteger(animationState, (int)MoveState.State2);
        }
    }
}
