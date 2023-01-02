using UnityEngine;
using System;

[RequireComponent(typeof(AudioSource))]

public class EventsManager : MonoBehaviour
{
    public static EventsManager eventInstance;

    public Action BallHitGround;
    public Action DropBullets;

    public AudioSource backgroundMusic;

    private void Awake()
    {
        if(eventInstance != null)
        {
            Destroy(gameObject);
            return;
        }

        eventInstance = this;

        DontDestroyOnLoad(this.gameObject);
    }
}
