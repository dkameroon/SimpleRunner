using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FPSCounter : MonoBehaviour
{
    public TextMeshProUGUI fpsText; // Assign the Text component in the Inspector.

    private float deltaTime = 0.0f;

    private void Update()
    {
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
        float fps = 1.0f / deltaTime;
        string fpsString = string.Format("{0:0.} FPS", fps);
        fpsText.text = fpsString;
    }
}