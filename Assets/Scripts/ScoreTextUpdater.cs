using TMPro;
using UnityEngine;

public class ScoreTextUpdater : MonoBehaviour
{
    TextMeshPro text;

    private void Start()
    {
        text = GetComponent<TextMeshPro>();
    }



    public void UpdateText(float score)
    {
        text.text = ("score: " + score.ToString());
    }
}
