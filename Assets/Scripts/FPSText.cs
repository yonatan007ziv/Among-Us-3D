using UnityEngine;
using TMPro;
public class FPSText : MonoBehaviour
{
    public TextMeshProUGUI fps;
    void Start()
    {
        fps = GetComponent<TextMeshProUGUI>();
    }
    void Update()
    {
        fps.text = "FPS: " + ((int)(1f / Time.unscaledDeltaTime)).ToString();
    }
}