using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BruteController : MonoBehaviour
{
    private Animator animator;
    private bool isStanding, isBlocking;
    public float speed, hp = 100;
    public AudioClip clip;
    public AudioSource source;
    public AudioClip clipswing;
    public AudioSource sourceswing;
    public AudioClip clipcollision;
    public AudioSource sourcecollision;
    public AudioClip clipdeath;
    public AudioSource sourcedeath;
    public int dmg = 5;
    private bool isDead;

    void Start()
    {
        source.clip = clip;
        isDead = false;
        sourceswing.clip = clipswing;
        sourcedeath.clip = clipdeath;
        sourcecollision.clip = clipcollision;
        animator = GetComponent<Animator>();
        isStanding = true;
        isBlocking = false;
    }

    void Update()
    {
        if (!isDead)
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                isStanding = !isStanding;
                animator.SetBool("isStanding", isStanding);
            }
            if (Input.GetMouseButtonDown(1))
            {
                isBlocking = !isBlocking;
                animator.SetBool("block", isBlocking);
            }


            if ((Input.GetKeyDown(KeyCode.F) || Input.GetMouseButtonDown(0)) && isStanding && !isBlocking)
            {
                animator.ResetTrigger("attack1");
                animator.SetTrigger("attack1");
                if (!sourceswing.isPlaying && !animator.GetCurrentAnimatorStateInfo(0).IsTag("attack"))
                    sourceswing.Play();
            }

            float speed = Input.GetAxis("Vertical");
            animator.SetFloat("speed", speed);
            if (speed != 0)
            {
                animator.SetBool("isMoving", true);
                if (speed > 0.3 || speed < -.03)
                    if (!source.isPlaying && !isBlocking)
                        source.Play();
            }
            else
            {
                animator.SetBool("isMoving", false);
                source.Stop();
            }

            if (Input.GetKeyDown(KeyCode.Space) && isStanding)
            {
                animator.ResetTrigger("jump");
                animator.SetTrigger("jump");
            }

            if (Input.GetKeyDown(KeyCode.X))
            {
                isBlocking = !isBlocking;
                animator.SetBool("block", isBlocking);
            }

            transform.Rotate(new Vector3(0, Input.GetAxis("Horizontal")) * 100 * Time.deltaTime);
        }
        GetComponent<CharacterController>().Move(new Vector3(0, -10) * Time.deltaTime);
    }
    void OnTriggerEnter(Collider col)
    {
        if (!isDead)
        {
            Animator Another = col.GetComponentInParent<Animator>();
            if (Another != null && !animator.GetCurrentAnimatorStateInfo(0).IsTag("block") && Another.GetCurrentAnimatorStateInfo(0).IsTag("attack"))
            {
                if (!col.gameObject.tag.Contains("brute") && col.gameObject.tag.Contains("Hitbox"))
                {

                    if (!sourcecollision.isPlaying)
                        sourcecollision.Play();
                    hp -= dmg;
                    animator.Play("faceHit");

                    if (hp == 0)
                    {
                        isDead = true;
                        animator.SetTrigger("isDead");
                        if (!sourcedeath.isPlaying)
                            sourcedeath.Play();
                    }
                }
            }
        }
    }
}