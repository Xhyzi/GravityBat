using UnityEngine;
using System.Collections;
using GravityBat_Constants;

namespace GravityBat_Audio
{
    /// <summary>
    /// Clase que representa una cancion.
    /// </summary>
    public class Song : MonoBehaviour
    {
        #region Attributes
        /// <summary>
        /// Fuente del audio (desde fichero)
        /// </summary>
        private AudioSource source;
        private AudioManager AM;

        /// <summary>
        /// Estado de reproduccion de la cancion
        /// </summary>
        private AudioState state;

        /// <summary>
        /// Bool utilizado para cortar el FadeIn en caso de pedir un FadeOut
        /// </summary>
        private bool keepFadingIn;

        /// <summary>
        /// Bool utilizado para cortar el FadeOut en caso de pedir un FadeIn.
        /// </summary>
        private bool keepFadingOut;

        /// <summary>
        /// Bool que indica si se esta produciendo un fade o no.
        /// </summary>
        private bool fade;
        #endregion

        /// <summary>
        /// Se ejecuta al instanciar el MonoBehaviour.
        /// Recoge instancias de source y AudioManager e inicializa el state de la cancion.
        /// </summary>
        private void Awake()
        {
            source = GetComponent<AudioSource>();
            AM = AudioManager.Instance;

            state = AudioState.IDLE;
        }

        /// <summary>
        /// Se suscribe a los eventos de AudioManager.
        /// </summary>
        private void OnEnable()
        {
            AM.OnMusicVolumeChanged += HandleOnMusicVolumeChanged;
            AM.OnMusicMutedChanged += HandleOnMusicMutedChanged;
            AM.OnFadeOut += HandleOnMusicFadeOut;
        }

        /// <summary>
        /// Se desuscribe a los eventos del AudioManager
        /// </summary>
        private void OnDisable()
        {
            AM.OnMusicVolumeChanged -= HandleOnMusicVolumeChanged;
            AM.OnMusicMutedChanged -= HandleOnMusicMutedChanged;
            AM.OnFadeOut -= HandleOnMusicFadeOut;
        }

        #region Event Handlers
        /// <summary>
        /// Actualiza el volumen de la cancion en funcion del volumen establecido en la 
        /// configuracion del juego
        /// </summary>
        /// <param name="volume">volumen al que se reproducira la cancion</param>
        private void HandleOnMusicVolumeChanged(float volume)
        {
            source.volume = volume;
        }

        /// <summary>
        /// Actualiza el atributo muted de la cancion en funcion de la configuracion del juego.
        /// </summary>
        /// <param name="muted">bool que indica si se muteara la cancion o no</param>
        private void HandleOnMusicMutedChanged(bool muted)
        {
            source.mute = muted;
        }

        /// <summary>
        /// Lleva a cabo el fadeOut de la musica en una corrutina.
        /// </summary>
        private void HandleOnMusicFadeOut()
        {
            StartCoroutine(FadeOut(Constants.MUSIC_FADE_SPEED));
        }
        #endregion

        #region Methods
        /// <summary>
        /// Reproduce la cancion.
        /// </summary>
        /// <param name="pitch">pitch/tono del sonido</param>
        /// <param name="loop">indica si la cancion se reproduce en bucle o no.</param>
        /// <param name="fade">indica si se debe hacer fadeIn al cargar la cancion o no.</param>
        public void Play(float pitch, bool loop, bool fade)
        {
            this.fade = fade;

            source.volume = GameManager.Instance.Data.MusicVolume;
            source.pitch = pitch;
            source.loop = loop;
            source.mute = !GameManager.Instance.Data.Music;

            source.Play();

            if (fade)
                StartCoroutine(FadeIn(Constants.MUSIC_FADE_SPEED, source.volume));

            state = AudioState.STARTED;
        }

        /// <summary>
        /// Pausa la cancion.
        /// </summary>
        public void Pause()
        {
            if (state != AudioState.PAUSED)
            {
                source.Pause();
                state = AudioState.PAUSED;
            }
        }

        /// <summary>
        /// Continua la reproduccion de la cancion.
        /// </summary>
        public void Resume()
        {
            if (state == AudioState.PAUSED)
            {
                source.UnPause();
                state = AudioState.STARTED;
            }
        }

        /// <summary>
        /// Para la reproduccion de la cancion
        /// </summary>
        public void Stop()
        {
            if (state != AudioState.IDLE)
            {
                source.Stop();
                state = AudioState.STOPPED;
            }
        }

        /// <summary>
        /// Corrutina que lleva a cabo el FadeIn de la cancion.
        /// </summary>
        /// <param name="speed">Velocidad a la que se lleva a cabo el FadeIn</param>
        /// <param name="maxVolume">Volumen maximo que alcanzara la cancion</param>
        /// <returns>cede la ejecucion de la corutina mediante yield</returns>
        public IEnumerator FadeIn(float speed, float maxVolume)
        {
            keepFadingIn = true;
            keepFadingOut = false;

            source.volume = 0;
            float audioVolume = source.volume;

            while (source.volume < maxVolume && keepFadingIn)
            {
                source.volume += speed;
                yield return new WaitForFixedUpdate();
            }
        }

        /// <summary>
        /// Corrutina que lleva a cabo el FadeOut en la cancion.
        /// </summary>
        /// <param name="speed">Velocidad a la que se lleva a cabo el FadeOut</param>
        /// <returns>Cede la ejecucion de la corrutina mediante yield</returns>
        public IEnumerator FadeOut(float speed)
        {
            keepFadingIn = false;
            keepFadingOut = true;

            while (source.volume > 0 && keepFadingOut)
            {
                source.volume -= speed;
                yield return new WaitForFixedUpdate();
            }
        }
        #endregion
    }
}
