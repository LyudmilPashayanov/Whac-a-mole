using UnityEngine;

namespace Miniclip.Audio
{
    /// <summary>
    /// Audio manager, which is singleton, so that you can play audio from anywhere in the app you want.
    /// </summary>
    public class AudioManager : MonoBehaviour
    {
        private static AudioManager _instance;
        public static AudioManager Instance { get { return _instance; } }

        [SerializeField] AudioSource _oneShotSource; 
        
        [SerializeField] AudioClip _goodHitAudio; 
        [SerializeField] AudioClip _metalHitAudio; 
        [SerializeField] AudioClip _badHitAudio;
        [SerializeField] AudioClip _buttonUIAudio; 
        
        
        void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(this);
            }
            else
            {
                _instance = this;
            }
        }

        public void PlayNormalHitSound()
        {
            _oneShotSource.PlayOneShot(_goodHitAudio);
        }
        
        public void PlayBombHitSound()
        {
            _oneShotSource.PlayOneShot(_badHitAudio);
        }
        
        public void PlayMetalHitSound()
        {
            _oneShotSource.PlayOneShot(_metalHitAudio);
        }

        public void PlayButtonClickSound()
        {
            _oneShotSource.PlayOneShot(_buttonUIAudio);
        }
    }
}
