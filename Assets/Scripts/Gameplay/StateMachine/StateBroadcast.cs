using System;
using UnityEngine;

namespace Gameplay.StateMachine
{
    [CreateAssetMenu(menuName = "StateMachine/StateBroadcast")]
    public class StateBroadcast : ScriptableObject
    {
        public States state = States.None;
        public States was = States.None;

        public void ExitGame()
        {
            state = States.None;
        }
    }

    public enum States
    {
        None,
        Idle,
        Grounded,
        Jumping,
        Running,
        Crouch,
        Roofed,
        RoofRunning,
        WallSlide,
        Stacked,
        Falling,
        HitToRight,
        HitToLeft,
        NeverHitted,
        Bounced,
        Plated,
        Wasted,
        Recycled,
    }
}
