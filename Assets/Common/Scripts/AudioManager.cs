using SuperMaxim.Messaging;
using UnityEngine;

namespace Common.Scripts
{
    public struct AudioPlayOneShotPayload
    {
        public AudioClip AudioClip;
    }
    public struct AudioPlayLoopPayload
    {
        public AudioClip AudioClip;
    }
    public class AudioManager : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSourceLoop;
        [SerializeField] private AudioSource _audioSourceOneShot;

        private void Awake()
        {
            Messenger.Default.Subscribe<AudioPlayOneShotPayload>(PlayOneShot);
            Messenger.Default.Subscribe<AudioPlayLoopPayload>(PlayLoop);
        }
        private void OnDestroy()
        {
            Messenger.Default.Unsubscribe<AudioPlayOneShotPayload>(PlayOneShot);
            Messenger.Default.Unsubscribe<AudioPlayLoopPayload>(PlayLoop);
        }
        private void PlayOneShot(AudioPlayOneShotPayload payload)
        {
            _audioSourceOneShot.PlayOneShot(payload.AudioClip);
        }
        private void PlayLoop(AudioPlayLoopPayload payload)
        {
            _audioSourceLoop.clip = payload.AudioClip;
            _audioSourceLoop.loop = true;
            _audioSourceLoop.Play();
        }
    }
}
