using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    MENU,
    WAIT,
    INIT,
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

public enum SoundState
{
    Music,
    FallSound,
    SpawnSound,
    HurtSound,
    ChocWaveSound,
    MeteoriteSound,
    DisapointedSound,
    EatingSound,
    EffortSound,
    SatisfySound,
    SpecialVoiceSound,
    ChargedSpecialSound,
    PunchSpecialSound,
    LoopSpecialSound,
    NormalPunchSound,
    BeingHitSound,
    FootstepsSound,
}