using System.Diagnostics;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

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
    public GameObject ps;

    bool once = false;


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

        if (transform.position.y > highestPoint) // move bar and stuff
        {
            highestPoint = transform.position.y;
            scoreBar.transform.position = new Vector3(scoreBar.transform.position.x, highestPoint, scoreBar.transform.position.z);
            scoreBar.GetComponent<PlankSpawning>().TrySpawnPlank();
            scoreBar.GetComponent<ScoreTextUpdater>().UpdateText(highestPoint);
        }

        if (scoreBar.transform.position.y - transform.position.y > 5) // DIE!!!
        {
            UnityEngine.Debug.Log("so uh we died");
            
            if (!once)
            {
                GameObject spawnedPartical = Instantiate(ps);
                spawnedPartical.transform.position = transform.position + new Vector3(0,3,0);
                once = true;
                Invoke(nameof(ReloadScene), 1);
            }
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

    private void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
