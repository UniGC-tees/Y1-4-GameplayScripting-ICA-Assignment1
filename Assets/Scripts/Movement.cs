using System.Diagnostics;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    private Rigidbody2D body;
    private Vector2 power;
    private bool grappleLaunched = false;
    private Vector2 mousePosition;
    private float highestPoint = -1000;
    public GameObject scoreBar;
    public Grapple assetGrapple;
    private Grapple currentGrapple;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        SpawnGrapple();
    }

    // Update is called once per frame
    void Update() //frames yo
    {
        if (!grappleLaunched) // only rotate grapple when its on the player obv
        {
            int grapRotSpeed = 20;

            Vector2 direction = Camera.main.ScreenToWorldPoint(Mouse.current.position.value) - currentGrapple.transform.position; //cache this

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            // ik its framerate dependant still ok..
            currentGrapple.transform.rotation = Quaternion.Slerp(currentGrapple.transform.rotation, rotation, grapRotSpeed * Time.deltaTime);
        }

        if (transform.position.y > highestPoint)
        {
            highestPoint = transform.position.y;
            scoreBar.transform.position = new Vector3(scoreBar.transform.position.x, highestPoint, scoreBar.transform.position.z);
            scoreBar.GetComponent<PlankSpawning>().TrySpawnPlank();
        }
    }

    private void FixedUpdate()
    {
        body.AddForce(power); // movement force
    }

    public void OnMove(InputValue hey)
    {
        power = (4*hey.Get<Vector2>());
    }

    public void OnAttack()
    {
        if (grappleLaunched) return;
        currentGrapple.GetComponent<Grapple>().Launch();
        grappleLaunched = true;
    }

    public void SpawnGrapple()
    {
        currentGrapple = Instantiate(assetGrapple);
        currentGrapple.transform.parent = transform;
        currentGrapple.transform.localPosition = Vector3.zero;


        Vector2 direction = Camera.main.ScreenToWorldPoint(Mouse.current.position.value) - currentGrapple.transform.position; //cache this

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        // ik its framerate dependant still ok..
        currentGrapple.transform.rotation = rotation;


        grappleLaunched = false;
    }
}
