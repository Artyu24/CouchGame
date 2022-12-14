using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundForGame : MonoBehaviour
{
    void Start()
    {
        FindObjectOfType<AudioManager>().PlayRandom(SoundState.SpaceAmbianceSound);
        //FindObjectOfType<AudioManager>().Stop(SoundState.MusicLobbySound);
        //FindObjectOfType<AudioManager>().Stop(SoundState.SpaceAmbianceLobbySound);

    }




}
