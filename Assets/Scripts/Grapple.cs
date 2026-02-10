using UnityEngine;

public class Grapple : MonoBehaviour
{
    private Rigidbody2D playerRb;
    public int launchPower = 16;
    public float flingPower = 10f;
    public AnimationCurve curve;
    public void Launch()
    {
        playerRb = transform.parent.GetComponent<Rigidbody2D>();
        transform.parent = null;

        Debug.Log(playerRb);

        GetComponent<Rigidbody2D>().simulated = true;
        GetComponent<Rigidbody2D>().linearVelocity = new Vector2(transform.right.x, transform.right.y) * launchPower;

        Invoke(nameof(ResetArrow), 3);
    }

    private void OnTriggerEnter2D(Collider2D collision) // pull player
    {
        GetComponent<Rigidbody2D>().simulated = false;

        if (playerRb  != null) // incase the player is alr dead
        {
            playerRb.linearVelocity = new Vector3(curve.Evaluate(transform.position.x - playerRb.transform.position.x), curve.Evaluate(transform.position.y - playerRb.transform.position.y), curve.Evaluate(transform.position.z - playerRb.transform.position.z)) * flingPower;
            Invoke(nameof(TellPlayerToSpawnGrapple), 0.5f);
            Invoke(nameof(KillBrah), 2);
        }
    }

    void KillBrah()
    {
        Destroy(gameObject);
    }

    void TellPlayerToSpawnGrapple()
    {
        playerRb.GetComponent<Movement>().SpawnGrapple();
    }

    void ResetArrow()
    {
        if (GetComponent<Rigidbody2D>().simulated)
        {
            TellPlayerToSpawnGrapple();
            KillBrah();
        }
    }
}