using UnityEngine;

public class WallBouncyness : MonoBehaviour
{
    Rigidbody2D body;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x > 8 || transform.position.x < -8) // bounce off sides
        {
            if (transform.position.x < 0) body.linearVelocityX = 10;
            else body.linearVelocityX = -10;
        }
    }
}
