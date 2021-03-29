using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    Rigidbody2D rigid;
    [SerializeField]
    protected float speed=100f;
    [SerializeField]
    protected Vector2 direction;

    // Start is called before the first frame update
    void Start()
    {
        rigid = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    public void Move()
    {
        rigid.velocity = transform.right * speed * Time.deltaTime;
    }
}
