using UnityEngine;
using GravityBat_Constants;

namespace GravityBat_Audio
{   
    /// <summary>
    /// Clase que representa un efecto de sonido.
    /// </summary>
    public class AudioFX : MonoBehaviour
    {
        #region Attributes
        /// <summary>
        /// Fuente de audio (desde fichero)
        /// </summary>
        private AudioSource source;
        private AudioManager AM;
        #endregion

        /// <summary>
        /// Ejecutado al instanciar el MonoBehaviour.
        /// Recoge referencias al source y al AudioManager.
        /// </summary>
        private void Awake()
        {
            source = GetComponent<AudioSource>();
            AM = AudioManager.Instance;
        }

        /// <summary>
        /// Se suscribe a eventos del AudioManager
        /// </summary>
        private void OnEnable()
        {
            AM.OnSfxVolumeChanged += HandleOnSfxVolumeChanged;
            AM.OnSfxMutedChanged += HandleOnSfxMutedChanged;
        }

        /// <summary>
        /// Se desuscribe a los eventos del AudioManager
        /// </summary>
        private void OnDisable()
        {
            AM.OnMusicVolumeChanged -= HandleOnSfxVolumeChanged;
            AM.OnMusicMutedChanged -= HandleOnSfxMutedChanged;
        }

        #region Event Handlers
        /// <summary>
        /// Actualiza el volumen del efecto de sonido dependiendo del volumen que se 
        /// ha establecido en la configuracion del juego
        /// </summary>
        /// <param name="volume">volumen al que se reproducira el sfx</param>
        private void HandleOnSfxVolumeChanged(float volume)
        {
            source.volume = volume;
        }

        /// <summary>
        /// Actualiza el atributo muted del sfx en funcion de la configuracion del juego.
        /// </summary>
        /// <param name="muted">bool que indica si se han muteado los efectos de sonido o no.</param>
        private void HandleOnSfxMutedChanged(bool muted)
        {
            source.mute = muted;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Reproduce el efecto de sonido con la configuracion establecida en el juego.
        /// </summary>
        public void Play()
        {
            source.volume = GameManager.Instance.Data.SfxVolume;
            source.pitch = Constants.DEFAULT_SONG_PITCH;
            source.mute = !GameManager.Instance.Data.Sfx;
            source.loop = false;
            source.Play();
        }
        #endregion
    }
}
