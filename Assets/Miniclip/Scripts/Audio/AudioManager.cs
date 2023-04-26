using UnityEngine;
using UnityEngine.EventSystems;

namespace Miniclip.Audio
{
    public class AudioManager : MonoBehaviour
    {
        static AudioManager instance;
        public static AudioManager Instance { get { return instance; } }

        [SerializeField] AudioSource _backgroundSource; 
        [SerializeField] AudioSource _oneShotSource; 
        
        [SerializeField] AudioClip _goodHitAudio; 
        [SerializeField] AudioClip _metalHitAudio; 
        [SerializeField] AudioClip _badHitAudio;
        [SerializeField] AudioClip _buttonUIAudio; 
        
        
        void Awake()
        {
            instance = this;
        }

        public void EnableBackgroundMusic(bool enable)
        {
            if (enable)
            {
                _backgroundSource.Play();
            }
            else
            {
                _backgroundSource.Stop();
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
