using UnityEngine;

public class SoundForLobby : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<AudioManager>().PlayRandom(SoundState.MusicLobbySound);
        FindObjectOfType<AudioManager>().PlayRandom(SoundState.SpaceAmbianceLobbySound);

    }

}
