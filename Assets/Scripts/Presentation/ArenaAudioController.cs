using UnityEngine;
using Random = UnityEngine.Random;

namespace Presentation
{

    public class ArenaAudioController : MonoBehaviour
    {
        [Header("Music")]
        [SerializeField] private AudioSource musicSource;
        [SerializeField] private AudioClip arenaMusic;

        [Header("SFX")]
        [SerializeField] private AudioSource sfxSource;
        [SerializeField] private AudioClip fightClip;
        [SerializeField] private AudioClip screamClip;
        [SerializeField] private AudioClip[] crowdClips;

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

        public void PlayScream()
        {
            sfxSource.PlayOneShot(screamClip);
        }
        public void PlayFight()
        {
            sfxSource.PlayOneShot(fightClip);
        }

        public void PlayVictory()
        {
            sfxSource.PlayOneShot(crowdClips[Random.Range(0, crowdClips.Length)]);
        }
    }
}
