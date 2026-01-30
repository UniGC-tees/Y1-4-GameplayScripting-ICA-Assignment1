using UnityEngine;

public class Grapple : MonoBehaviour
{
    private Rigidbody2D playerRb;
    public int launchPower = 16;
    public float flingPower = 10f;
    public void Launch()
    {
        playerRb = transform.parent.GetComponent<Rigidbody2D>();
        transform.parent = null;

        Debug.Log(playerRb);

        GetComponent<Rigidbody2D>().simulated = true;
        GetComponent<Rigidbody2D>().AddForce(new Vector2(transform.right.x, transform.right.y) * launchPower, ForceMode2D.Impulse);

        Invoke(nameof(ResetArrow), 3);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GetComponent<Rigidbody2D>().simulated = false;
        playerRb.linearVelocity = (transform.position - playerRb.transform.position)*flingPower;
        Invoke(nameof(TellPlayerToSpawnGrapple), 0.5f);
        Invoke(nameof(KillBrah), 2);
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
