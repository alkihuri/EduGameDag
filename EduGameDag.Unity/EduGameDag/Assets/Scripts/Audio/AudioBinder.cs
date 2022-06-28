using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Audio
{
    public class AudioBinder : MonoBehaviour
    {
        [SerializeField]
        private AudioSource _audioSource;
        [SerializeField]
        private string _currentAudioPath;

        private Queue<string> _audioQueue = new();

        public static AudioBinder Instance;

        private void Awake() => Instance = this;

        private void Start() => _audioSource = GetComponent<AudioSource>();

        public void SyncAudio(string s)
        {
            _audioQueue.Enqueue(s);
            Debug.Log("Added audioqueue " + s);
            //Смотрим - есть ли у нас еще какие-то аудио или нет. Если нет, то запускатеся тот, что есть, а если есть, то запускает корутину
            //которая ждет окончания проигрывания аудио и потом запускает некст на очереди
            //по хорошему тут лучше бы пробрасывать все через колбэки, но на это надо чуть больше времени
            // if (_audioQueue.Count > 1)
            //     StartCoroutine(AudioWaiter(_audioQueue[0]));
            // else
            //     SyncAudio();
        }

        private void Update()
        {
            if (!_audioSource.isPlaying  && _audioQueue.Count > 0) {
                _audioSource.clip = AudioGetter(_audioQueue.Dequeue());
                _audioSource.Play();
            }
        }
        

        // private void SyncAudio()
        // {
        //     _currentAudioPath = _audioQueue[0].Replace("-", "");
        //     _audioSource.clip = AudioGetter(_currentAudioPath);
        //     Debug.Log(_audioSource.clip.name);
        //     _audioSource.Play();
        // }
        //
        // private IEnumerator AudioWaiter(string s)
        // {
        //     yield return new WaitWhile(() => _audioSource.isPlaying);
        //     _audioQueue.Remove(s);
        //     SyncAudio();
        // }

        private AudioClip AudioGetter(string currentAudio)
        {
            var path = "Audio/Find/" + currentAudio;
            return Resources.Load<AudioClip>(path) as AudioClip;
        }
    }
}
