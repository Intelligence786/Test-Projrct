using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Poision : MonoBehaviour
{
    public Image poisonImage;
    public void ActivatePoisionImage(bool state)
    {
        poisonImage.enabled = state;
    }
}
