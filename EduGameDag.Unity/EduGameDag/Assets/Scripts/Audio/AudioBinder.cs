using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Audio
{
    public class AudioBinder : MonoBehaviour
    {
        [SerializeField] AudioSource _audioSource;
        [SerializeField] string _currentAudioPath;

        [SerializeField] List<string> _audioQuee;

        public static AudioBinder Instance;

        private void Awake() => Instance = this;

        private void Start()
        {
            // PlayerPrefs.SetString("CURRENT_AUDIO_KEY","Intro");
            _audioSource = GetComponent<AudioSource>();
            SyncAudio("Intro");
        }
        
        public void SyncAudio(string s)
        {
            _audioQuee.Add(s);
            //Смотрим - есть ли у нас еще какие-то аудио или нет. Если нет, то запускатеся тот, что есть, а если есть, то запускает корутину
            //которая ждет окончания проигрывания аудио и потом запускает некст на очереди
            //по хорошему тут лучше бы пробрасывать все через колбэки, но на это надо чуть больше времени
            if (_audioQuee.Count > 1)
                StartCoroutine(AudioWaiter(_audioQuee[0]));
            else
                SyncAudio();
        }

        private void SyncAudio()
        {
            _currentAudioPath = _audioQuee[0].Replace("-", "");
            _audioSource.clip = AudioGetter(_currentAudioPath);
            _audioSource.Play();
        }

        private IEnumerator AudioWaiter(string s)
        {
            yield return new WaitWhile(() => _audioSource.isPlaying);
            _audioQuee.Remove(s);
            SyncAudio();
        }

        private AudioClip AudioGetter(string currentAudio)
        {
            var path = "Audio/Find/" + currentAudio;
            return Resources.Load<AudioClip>(path) as AudioClip;
        }
    }
}
