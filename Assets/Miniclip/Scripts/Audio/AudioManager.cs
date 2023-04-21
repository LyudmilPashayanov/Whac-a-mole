using UnityEngine;

namespace Miniclip.Audio
{
    public class AudioManager : MonoBehaviour
    {
        static AudioManager instance;
        public static AudioManager Instance { get { return instance; } }
        
        void Awake()
        {
            instance = this;
        }

        public void PlayBackgroundMusic()
        {
            
        }

        public void StopBackgroundMusic()
        {
            
        }
        
        public void PlayGoodHitSound()
        {
            
        }
        
        public void PlayBadHitSound()
        {
            
        }

        public void PlayButtonClickSound()
        {
            
        }
    }
}
