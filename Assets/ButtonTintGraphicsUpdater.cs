using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonTintGraphicsUpdater : MonoBehaviour
{
    private Image localImage;
    public List<Image> targetImages;

    void Start()
    {
        localImage = GetComponent<Image>();
    }

    void Update()
    {
        foreach (Image i in targetImages)
            i.color = localImage.color;
    }
}
