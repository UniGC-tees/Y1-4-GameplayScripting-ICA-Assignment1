using System.Diagnostics;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    private Rigidbody2D body;
    private Vector2 power;
    public GameObject grapple;
    private bool grappleLaunched = false;
    public int grapplePower = 1000;
    private Vector2 mousePosition;
    private float highestPoint = -1000;
    public GameObject scoreBar;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!grappleLaunched) // only rotate grapple when its on the player obv
        {
            int grapRotSpeed = 20;

            Vector2 direction = Camera.main.ScreenToWorldPoint(Mouse.current.position.value) - grapple.transform.position;

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            grapple.transform.rotation = Quaternion.Slerp(grapple.transform.rotation, rotation, grapRotSpeed * Time.deltaTime);
        }

        if (transform.position.y > highestPoint)
        {
            highestPoint = transform.position.y;
            scoreBar.transform.position = new Vector3(scoreBar.transform.position.x, highestPoint, scoreBar.transform.position.z);
        }
    }

    private void FixedUpdate()
    {
        body.AddForce(power); // movement force
    }

    public void OnMove(InputValue hey)
    {
        power = (10*hey.Get<Vector2>());
    }

    public void OnAttack()
    {
        grappleLaunched = true;
        grapple.GetComponent<Rigidbody2D>().simulated = true;
        grapple.GetComponent<Rigidbody2D>().AddForce(new Vector2 (grapple.transform.right.x, grapple.transform.right.y) * grapplePower);
    }
}
