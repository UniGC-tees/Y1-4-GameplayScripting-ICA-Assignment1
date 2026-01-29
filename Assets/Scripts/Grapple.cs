using UnityEngine;

public class Grapple : MonoBehaviour
{
    private Rigidbody2D playerRb;
    public int grapplePower = 16;
    public void Launch()
    {
        playerRb = transform.parent.GetComponent<Rigidbody2D>();
        transform.parent = null;

        Debug.Log(playerRb);

        GetComponent<Rigidbody2D>().simulated = true;
        GetComponent<Rigidbody2D>().AddForce(new Vector2(transform.right.x, transform.right.y) * grapplePower, ForceMode2D.Impulse);

        Invoke(nameof(ResetArrow), 5);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GetComponent<Rigidbody2D>().simulated = false;
        playerRb.linearVelocity = (transform.position - playerRb.transform.position)*1.45f;
        Invoke(nameof(TellPlayerToSpawnGrapple), 1);
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
