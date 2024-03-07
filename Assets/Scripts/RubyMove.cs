using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public enum RubyMoveEnum
{
    IdleDown, IdleUp, IdleSide, MoveDown, MoveUp, MoveSide
}

public class RubyMove : MonoBehaviour
{
    // Start is called before the first frame update
    private Animator animator;
    private Rigidbody2D rb;
    private string animationState = "State";
    private bool isLeft = false;
    private bool isUp = false;
    private bool isHorizontal = false;
    [SerializeField] float speed;
    [SerializeField] float jupmForce;
    [SerializeField] private LayerMask jumpableGround;
    [SerializeField] private AudioSource moveAudio;
    [SerializeField] private AudioSource finishLevel;
    private bool isFinish = false;
    private SpriteRenderer sp;
    private BoxCollider2D coli;
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        sp = GetComponent<SpriteRenderer>();
        coli = GetComponent<BoxCollider2D>();
    }

    private void FixedUpdate()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        bool checkJump = Input.GetButtonDown("Jump") && IsGround();
        RubyMovement(horizontal, checkJump);
        AnimatorControl(horizontal, vertical);
    }

    private bool IsGround()
    {
        return Physics2D.BoxCast(coli.bounds.center, 
            coli.bounds.size, 
            0f, Vector2.down, 0.1f, jumpableGround);
    }

    private void AnimatorControl(float horizontal, float vertical)
    {
        if (vertical > 0)
        {
            animator.SetInteger(animationState, (int)RubyMoveEnum.MoveUp);
            isHorizontal = false;
            isUp = true;
        }
        else if (vertical < 0)
        {
            animator.SetInteger(animationState, (int)RubyMoveEnum.MoveDown);
            isHorizontal = false;
            isUp = false;
        }
        else if (horizontal < 0)
        {
            animator.SetInteger(animationState, (int)RubyMoveEnum.MoveSide);
            isLeft = false;
            isHorizontal = true;
            sp.flipX = true;
        }
        else if (horizontal > 0)
        {
            animator.SetInteger(animationState, (int)RubyMoveEnum.MoveSide);
            isLeft = true;
            isHorizontal = true;
            sp.flipX = false;
        }
        else if (isUp && !isHorizontal && horizontal == 0 && vertical == 0)
        {
            animator.SetInteger(animationState, (int)RubyMoveEnum.IdleUp);
        }
        else if (!isUp && !isHorizontal && horizontal == 0 && vertical == 0)
        {
            animator.SetInteger(animationState, (int)RubyMoveEnum.IdleDown);
        }
        else if (!isLeft && isHorizontal && horizontal == 0 && vertical == 0)
        {
            animator.SetInteger(animationState, (int)RubyMoveEnum.IdleSide);
            sp.flipX = true;
        }
        else if (isLeft && isHorizontal && horizontal == 0 && vertical == 0)
        {
            animator.SetInteger(animationState, (int)RubyMoveEnum.IdleSide);
            sp.flipX = false;
        }
    }

    private void RubyMovement(float horizontal, bool jump)
    {
        if (jump)
            rb.velocity = new Vector2(rb.velocity.x, jupmForce);
        
        if (horizontal != 0 && !moveAudio.isPlaying)
            moveAudio.Play();
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Finish") && !isFinish)
        {
            isFinish = true;
            MoveToNextScene();
        }
    }

    private async void MoveToNextScene()
    {
        finishLevel.Play();
        await Task.Delay(1000);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        await Console.Out.WriteLineAsync("Hello world");
    }
}
