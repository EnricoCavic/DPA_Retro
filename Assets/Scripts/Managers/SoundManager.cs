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
    private int flip;

    private List<AudioSource> soundtrackChannels;

    private void Awake()
    {
        if (!InstanceSetup(this)) return;

        soundtrackChannels = new();
        GameObject soundChannel;
        for (int i = 0; i < totalSoundtrackChannels; i++)
        {
            soundChannel = new GameObject("SoundChannel"+i);
            soundChannel.transform.parent = transform;
            var audioSource = soundChannel.AddComponent<AudioSource>();
            audioSource.playOnAwake = false; 
            soundtrackChannels.Add(audioSource);
        }

        nextEventTime = AudioSettings.dspTime + 2 * preScheduleTime;
    }

    private void Update()
    {
        if (AudioSettings.dspTime + preScheduleTime > nextEventTime)
        {
            //Debug.Log("Schedule next event");
            // l�gica para escolher as pr�xima musica
            // possibilidade de existir uma fila de faixas populada dinamicamente
            // tamb�m ser� necess�rio fazer uma pool audio sources liberadas
            RandomizeFromList();

            nextEventTime += MINUTE / bpm * numBeatsPerSegment;
        }
    }

    private void RandomizeFromList()
    {
        soundtrackChannels[0 + flip].clip = clipsTrack1[Random.Range(0, clipsTrack1.Count)];
        soundtrackChannels[1 + flip].clip = clipsTrack2[Random.Range(0, clipsTrack2.Count)];
        soundtrackChannels[2 + flip].clip = clipsTrack3[Random.Range(0, clipsTrack3.Count)];

        soundtrackChannels[0 + flip].PlayScheduled(nextEventTime);
        soundtrackChannels[1 + flip].PlayScheduled(nextEventTime);
        soundtrackChannels[2 + flip].PlayScheduled(nextEventTime);
        flip = 3 - flip;
    }
}
