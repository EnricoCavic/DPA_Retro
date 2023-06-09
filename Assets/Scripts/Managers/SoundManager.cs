using Retro.Generic;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    [SerializeField] private int totalSoundtrackChannels;

    [SerializeField] private float bpm = 140.0f;
    [SerializeField] private int numBeatsPerSegment = 8;
    [SerializeField] private float preScheduleTime = 1.0f;

    [SerializeField] List<AudioClip> clipsTrack1;
    [SerializeField] List<AudioClip> clipsTrack2;
    [SerializeField] List<AudioClip> clipsTrack3;

    private const float MINUTE = 60.0f;
    private double nextEventTime;

    private List<AudioSource> soundtrackChannels;

    private void Awake()
    {
        if (!InstanceSetup(this)) return;

        soundtrackChannels = new();
        GameObject soundChannel;
        for (int i = 0; i < totalSoundtrackChannels; i++)
        {
            soundChannel = new GameObject("SoundChannel"+i);
            soundtrackChannels.Add(soundChannel.AddComponent<AudioSource>());
            soundChannel.transform.parent = transform;
        }
    }

    private void Update()
    {
        double currentTime = AudioSettings.dspTime;

        if (currentTime + preScheduleTime > nextEventTime)
        {
            // lógica para escolher as próxima musica
            // possibilidade de existir uma fila de faixas populada dinamicamente
            soundtrackChannels[0].clip = clipsTrack1[Random.Range(0, clipsTrack1.Count)];
            soundtrackChannels[1].clip = clipsTrack2[Random.Range(0, clipsTrack2.Count)];
            soundtrackChannels[2].clip = clipsTrack3[Random.Range(0, clipsTrack3.Count)];

            soundtrackChannels[0].PlayScheduled(nextEventTime);
            soundtrackChannels[1].PlayScheduled(nextEventTime);
            soundtrackChannels[2].PlayScheduled(nextEventTime);
            

            nextEventTime += MINUTE / bpm * numBeatsPerSegment;
        }
    }
}
