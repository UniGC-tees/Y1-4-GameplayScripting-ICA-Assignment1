using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    private Rigidbody2D body;
    private Vector2 power;
    public Object grapple;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        body.AddForce(power); // movement force
    }

    void OnMove(InputValue hey)
    {
        power = (10*hey.Get<Vector2>());
    }

    void OnAttack()
    {
        grapple.GetComponent<Rigidbody2D>().simulated = true;
        grapple.GetComponent<Rigidbody2D>().AddForce(new Vector2 (500,0));
    }
}
