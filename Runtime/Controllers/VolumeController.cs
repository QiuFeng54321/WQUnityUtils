using UnityEngine;
using UnityEngine.Audio;

namespace WilliamQiufeng.UnityUtils.Controllers
{
    public class VolumeController : MonoBehaviour
    {
        public AudioMixer audioMixer;
        public string paramName;
        public VolumeSlider volumeSlider;

        private void Start()
        {
            if (audioMixer.GetFloat(paramName, out var value))
                volumeSlider.SetWithoutNotify(value);
        }

        public void ValueChange(float val)
        {
            audioMixer.SetFloat(paramName, val);
        }
    }
}