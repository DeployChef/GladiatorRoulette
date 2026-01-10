using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Presentation
{

    public class ArenaAudioController : MonoBehaviour
    {
        [Header("Music")]
        [SerializeField] private AudioSource musicSource;
        [SerializeField] private AudioClip arenaMusic;

        [Header("SFX")]
        [SerializeField] private AudioSource sfxSource;
        [SerializeField] private AudioClip fightStartClip;
        [SerializeField] private AudioClip fightLoopClip;
        [SerializeField] private AudioClip victoryClip;

        public void PlayArenaMusic()
        {
            musicSource.clip = arenaMusic;
            musicSource.loop = true;
            musicSource.Play();
        }

        public void StopArenaMusic()
        {
            musicSource.Stop();
        }

        public void PlayFightStart()
        {
            sfxSource.PlayOneShot(fightStartClip);
        }

        public void PlayFightLoop()
        {
            sfxSource.clip = fightLoopClip;
            sfxSource.loop = true;
            sfxSource.Play();
        }

        public void StopFightLoop()
        {
            sfxSource.Stop();
        }

        public void PlayVictory()
        {
            sfxSource.PlayOneShot(victoryClip);
        }
    }
}
