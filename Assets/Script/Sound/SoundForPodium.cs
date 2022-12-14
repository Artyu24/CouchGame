using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundForPodium : MonoBehaviour
{
    void Start()
    {
        FindObjectOfType<AudioManager>().PlayRandom(SoundState.WinThemeLeaderboardSound);       

    }
}
