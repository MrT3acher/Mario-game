using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public interface IDieable
{
    void Die();
}

public class Mario : MonoBehaviour, IDieable
{
    public float speed;
    public float jump;
    public Transform groundSign;
    public LayerMask groundMask;

    private const float RAYCASTDISTANCE = 0.2f;

    private Rigidbody2D _rigidbody;
    private bool _grounded;
    private Animator _animator;

    public void GetBig()
    {
        transform.localScale = Vector2.one * 2;
    }

    public void Die()
    {
        _animator.SetTrigger("Die");

        StartCoroutine(AfterDie());
    }

    IEnumerator AfterDie()
    {
        yield return new WaitForSeconds(2);

        SceneManager.LoadScene("SampleScene");
    }

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        // variables
        float rl = Input.GetAxis("Horizontal");
        Vector2 vel = _rigidbody.velocity;
        vel.x = rl * speed;
        _rigidbody.velocity = vel;


        // Animation
        Vector2 scale = transform.localScale;
        if (rl > 0)
        {
            scale.x = -1;
        }
        else if (rl != 0)
        {
            scale.x = 1;
        }
        transform.localScale = scale;
        if (Mathf.Abs(rl) > 0.05f)
            _animator.SetBool("Moving", true);
        else
            _animator.SetBool("Moving", false);



        // Grounded check
        if (Physics2D.Raycast(groundSign.position, Vector2.down, RAYCASTDISTANCE, groundMask.value))
        {
            _grounded = true;
            _animator.SetBool("Jumping", false);
        }
        else
        {
            _grounded = false;
        }


        // Jump
        if (Input.GetButtonDown("Jump") && _grounded)
        {
            _rigidbody.AddForce(Vector2.up * jump);
            _animator.SetBool("Jumping", true);
        }
    }
}