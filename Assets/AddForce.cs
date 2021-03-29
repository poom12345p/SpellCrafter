using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddForce : MonoBehaviour
{
    // Start is called before the first frame update
    public enum Dir { Left, Right, Up, Down };
    public Dir direction;
    public float force;

    Rigidbody2D rb2d;

    private void OnEnable()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (direction == Dir.Left) rb2d.AddForce(Vector2.left * force);
        else if (direction == Dir.Right) rb2d.AddForce(Vector2.right * force);
        else if (direction == Dir.Up) rb2d.AddForce(Vector2.up * force);
        else if (direction == Dir.Down) rb2d.AddForce(Vector2.down * force);
    }

}
