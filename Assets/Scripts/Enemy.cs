using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDieable
{
    public float speed;
    public Vector2 direction = Vector2.left;

    private Rigidbody2D _rigidbody;

    public void Die()
    {
        Destroy(gameObject);
    }

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        _rigidbody.velocity = direction * speed;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        string tag = collision.gameObject.tag;
        if (tag == "Ground")
        {
            foreach (var item in collision.contacts)
            {
                if (item.normal == Vector2.right || item.normal == Vector2.left)
                {
                    direction = item.normal;
                    break;
                }
            }
        }
        else if (tag == "Player")
        {
            if (Physics2D.Raycast(transform.position, Vector2.up, 1, LayerMask.GetMask("Mario")))
            {
                Die();
            }
            else
            {
                collision.gameObject.GetComponent<Mario>().Die();
            }
        }
    }
}
