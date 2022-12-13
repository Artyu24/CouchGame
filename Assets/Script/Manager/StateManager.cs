using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    LOBBY,
    MENU,
    WAIT,
    INIT,
    INGAME,
    PAUSED,
    ENDROUND
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
    Timer,
    StepSound,
    HitSound,
    NormalPunch,
    SpecialPunchHit,
    SpecialChargedId,
    SpecialChargedLoop,
    WinSound,
    CountdownFinal5sSound,
    BeginingPublicSound,
    RechargedShockwaveSound,
    SpaceAmbianceSound,
    EndPublicSound,
    RotateCircleSound,
    SwitchCircleSound,
    TakeIglooControlSound,
    EjectPlayerIglooSound,
    IglooInControlIdleSound,
    ShieldAttackedSound,
    ShieldDestroyedSound,
    TransitionLoopSound,
    TransitionEndSound,
    BombExplosionSound,
    BumperTouchedSound,
    EauToxiquePlayerInSound,
    IglooInterrupteurPressedSound,
    StarSerringuePickUpSound,
    TeleportRespawnSound,
    UIConfirmSound,
    UISwitchSound,

}