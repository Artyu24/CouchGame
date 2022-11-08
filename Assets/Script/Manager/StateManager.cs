using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    MENU,
    INGAME,
    PAUSED
}

public enum PlayerState
{
    INIT,
    FIGHTING,
    FLYING,
    MIDDLE,
    DEAD
}

public enum CenterState
{
    PROTECTION,
    ACCESS,
    USE,
    REGENERATION
}
