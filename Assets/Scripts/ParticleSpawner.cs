using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ParticleSpawner : MonoBehaviour
{
    public GameObject particleType;
    public bool reloadsScene = false;

    private void Start()
    {
        Debug.Log("hey im here im alive");
    }

    public IEnumerator SpawnParticleAfter(float seconds)
    {
        Debug.Log("im spawning it im spawning it");
        yield return new WaitForSeconds(seconds);
        GameObject spawnedParticle = Instantiate(particleType);
        if (!reloadsScene) Destroy(gameObject);
        else
        {
            Invoke(nameof(ReloadScene), 1);
        }
    }
    public void StartUpSpawning(float seconds)
    {
        SpawnParticleAfter(seconds);
    }

    private void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}