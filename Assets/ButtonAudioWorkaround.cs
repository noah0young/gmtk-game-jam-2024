using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonAudioWorkaround : MonoBehaviour
{
    public void PlaySFX(string name)
    {
        AudioManager.PlaySFX(name);
    }
}
