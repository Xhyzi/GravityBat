using UnityEngine;
using System.Collections.Generic;
using GravityBat_Constants;
using System;

namespace GravityBat_Audio
{
    /// <summary>
    /// Clase encargada de controlar el audio de la aplicacion.
    /// Hereda de ScriptableObject y sigue el patron Singleton.
    /// Es instanciado desde el GameManager.
    /// </summary>
    // TODO: Utilizar NDK para el audio. Las funciones de audio de Unity tienen ~0.15s de delay en Android.
    public class AudioManager : ScriptableObject
    {
        #region Singleton
        private static AudioManager instance;
        private AudioManager() { }
        public static AudioManager Instance
        {
            get
            {
                if (instance == null)
                {
                    AudioManager.instance = ScriptableObject.CreateInstance<AudioManager>();
                    DontDestroyOnLoad(AudioManager.instance);
                }
                return instance;
            }
        }
        #endregion

        #region Attributes
        /// <summary>
        /// Diccionario que contiene las canciones cargadas, con su tag identificador.
        /// </summary>
        private Dictionary<SongTags, GameObject> trackGOs;

        /// <summary>
        /// Diccionario que contiene los efectos de sonido cargados, con su tag identificador.
        /// </summary>
        private Dictionary<SfxTags, GameObject> sfxGOs;
        #endregion

        #region Events
        public delegate void VolumeChangedHandler(float volume);
        public delegate void VolumeMutedHandler(bool muted);
        public delegate void FadeOutHandler();
        public delegate void SceneLoadedHandler();
        public delegate void LevelEventHandler();
        public delegate void UIEventHandler();

        public event VolumeChangedHandler OnMusicVolumeChanged;
        public event VolumeChangedHandler OnSfxVolumeChanged;
        public event VolumeMutedHandler OnMusicMutedChanged;
        public event VolumeMutedHandler OnSfxMutedChanged;
        public event FadeOutHandler OnFadeOut;
        public event SceneLoadedHandler OnTitleScreenSceneLoaded;
        public event SceneLoadedHandler OnLevelSelectionSceneLoaded;
        public event SceneLoadedHandler OnLevelSceneLoaded;
        public event LevelEventHandler OnLevelPaused;
        public event LevelEventHandler OnLevelResumed;
        public event LevelEventHandler OnPlayerDeath;
        public event LevelEventHandler OnPlayerFlap;
        public event LevelEventHandler OnPlayerGravitySwap;
        public event LevelEventHandler OnItemPickupSmall;
        public event LevelEventHandler OnItemPickupBig;
        public event UIEventHandler OnUIElementInteracted;
        #endregion

        /// <summary>
        /// Inicializa los diccionarios y se suscribe a los eventos.
        /// </summary>
        private void OnEnable()
        {
            trackGOs = new Dictionary<SongTags, GameObject>();
            sfxGOs = new Dictionary<SfxTags, GameObject>();

            this.OnTitleScreenSceneLoaded += HandleOnTitleScreenSceneLoaded;
            this.OnLevelSelectionSceneLoaded += HandleOnLevelSelectionSceneLoaded;
            this.OnLevelSceneLoaded += HandleOnLevelSceneLoaded;
            this.OnLevelPaused += HandleOnPause;
            this.OnLevelResumed += HandleOnLevelResumed;
            this.OnPlayerFlap += HandleOnFlap;
            this.OnPlayerDeath += HandleOnDeath;
            this.OnPlayerGravitySwap += HandleOnGravitySwap;
            this.OnItemPickupSmall += HandleOnItemPickUpSmall;
            this.OnItemPickupBig += HandleOnItemPickupBig;
            this.OnUIElementInteracted += HandleOnUIInteracted;
        }

        /// <summary>
        /// Se desuscribe de los eventos.
        /// </summary>
        private void OnDisable()
        {
            this.OnTitleScreenSceneLoaded -= HandleOnTitleScreenSceneLoaded;
            this.OnLevelSelectionSceneLoaded -= HandleOnLevelSelectionSceneLoaded;
            this.OnLevelSceneLoaded -= HandleOnLevelSceneLoaded;
            this.OnLevelPaused -= HandleOnPause;
            this.OnLevelResumed -= HandleOnLevelResumed;
            this.OnPlayerFlap -= HandleOnFlap;
            this.OnPlayerDeath -= HandleOnDeath;
            this.OnPlayerGravitySwap -= HandleOnGravitySwap;
            this.OnItemPickupSmall -= HandleOnItemPickUpSmall;
            this.OnItemPickupBig -= HandleOnItemPickupBig;
            this.OnUIElementInteracted -= HandleOnUIInteracted;
        }

        #region Event Handlers
        /// <summary>
        /// Reproduce la cancion de la TitleScreen
        /// </summary>
        private void HandleOnTitleScreenSceneLoaded()
        {
            ResetAudioTracks();
            Play(SongTags.TITLE_THEME, 0.1f);
        }

        /// <summary>
        /// Reproduce la cancion del LevelSelection
        /// </summary>
        private void HandleOnLevelSelectionSceneLoaded()
        {
            ResetAudioTracks();
            Play(SongTags.MENU_THEME, 0.1f);
        }

        /// <summary>
        /// Reproduce la cancion del Level
        /// </summary>
        private void HandleOnLevelSceneLoaded()
        {
            ResetAudioTracks();
            Play(SongTags.WORLD_1, 0.1f);
        }

        /// <summary>
        /// Pausa la cancion al producirse la pausa del nivel
        /// </summary>
        private void HandleOnPause()
        {
            PauseSong(SongTags.WORLD_1);
        }

        /// <summary>
        /// Continua reproducciendo la cancion al continuar con el nivel.
        /// </summary>
        private void HandleOnLevelResumed()
        {
            ResumeSong(SongTags.WORLD_1);
        }

        /// <summary>
        /// Reproduce el sonido de muerte del personaje.
        /// </summary>
        private void HandleOnDeath()
        {
            Play(SfxTags.DEATH);
            StopSong(SongTags.WORLD_1);
        }

        /// <summary>
        /// Reproduce el sonido de volar del personaje.
        /// </summary>
        private void HandleOnFlap()
        {
            Play(SfxTags.FLAP);
        }

        /// <summary>
        /// Reproduce el sonido de cambio de gravedad.
        /// </summary>
        private void HandleOnGravitySwap()
        {
            Play(SfxTags.GRAVITY_SWAP);
        }

        /// <summary>
        /// Reproduce el sonido de recoger un item pequenio
        /// </summary>
        private void HandleOnItemPickUpSmall()
        {
            Play(SfxTags.PICKUP_SMALL);
        }

        /// <summary>
        /// Reproduce el sonido de recoger un item grande.
        /// </summary>
        private void HandleOnItemPickupBig()
        {
            Play(SfxTags.PICKUP_BIG);
        }

        /// <summary>
        /// Reproduce el sonido de interaccion con un elemento de la UI.
        /// </summary>
        private void HandleOnUIInteracted()
        {
            Play(SfxTags.UI_INTERACTED);
        }
        #endregion

        #region Raise Events
        //Dispara los eventos
        public void Raise_MusicVolumeChanged(float volume)
        {
            if (OnMusicVolumeChanged != null)
                OnMusicVolumeChanged(volume);
        }

        public void Raise_SfxVolumeChanged(float volume)
        {
            if (OnSfxVolumeChanged != null)
                OnSfxVolumeChanged(volume);
        }

        public void Raise_MusicMutedChanged(bool muted)
        {
            if (OnMusicMutedChanged != null)
                OnMusicMutedChanged(muted);
        }

        public void Raise_SfxMutedChanged(bool muted)
        {
            if (OnSfxMutedChanged != null)
                OnSfxMutedChanged(muted);
        }

        public void Raise_FadeOut()
        {
            if (OnFadeOut != null)
                OnFadeOut();
        }

        public void Raise_TitleScreenSceneLoaded()
        {
            if (OnTitleScreenSceneLoaded != null)
                OnTitleScreenSceneLoaded();
        }

        public void Raise_LevelSelectionSceneLoaded()
        {
            if (OnLevelSelectionSceneLoaded != null)
                OnLevelSelectionSceneLoaded();
        }

        public void Raise_LevelSceneLoaded()
        {
            if (OnLevelSceneLoaded != null)
                OnLevelSceneLoaded();
        }

        public void Raise_PausedLevel()
        {
            if (OnLevelPaused != null)
                OnLevelPaused();
        }

        public void Raise_ResumedLevel()
        {
            if (OnLevelResumed != null)
                OnLevelResumed();
        }

        public void Raise_PlayerDeath()
        {
            if (OnPlayerDeath != null)
                OnPlayerDeath();
        }

        public void Raise_PlayerFlap()
        {
            if (OnPlayerFlap != null)
                OnPlayerFlap();
        }

        public void Raise_PlayerGravitySwap()
        {
            if (OnPlayerGravitySwap != null)
                OnPlayerGravitySwap();
        }

        public void Raise_ItemPickupSmall()
        {
            if (OnItemPickupSmall != null)
                OnItemPickupSmall();
        }

        public void Raise_ItemPickupBig()
        {
            if (OnItemPickupBig != null)
                OnItemPickupBig();
        }

        public void Raise_UIElementInteracted()
        {
            if (OnUIElementInteracted != null)
                OnUIElementInteracted();
        }
        #endregion

        #region Methods
        /// <summary>
        /// Carga una cancion y la guarda en el diccionario.
        /// </summary>
        /// <param name="tag">Tag identificador de la cancion a cargar</param>
        public void LoadSong(SongTags tag)
        {
            GameObject go = new GameObject();
            go.AddComponent<AudioSource>();
            go.GetComponent<AudioSource>().clip = GetAudioFromResources(tag);
            go.AddComponent<Song>();
            trackGOs.Add(tag, go);
        }

        /// <summary>
        /// Carga un efecto de sonido y lo guarda en el diccionario.
        /// </summary>
        /// <param name="tag">Tag identificador del efecto de sonido a cargar.</param>
        public void LoadSfx(SfxTags tag)
        {
            GameObject go = new GameObject();
            go.AddComponent<AudioSource>();
            go.GetComponent<AudioSource>().clip = GetAudioFromResources(tag);
            go.AddComponent<AudioFX>();
            sfxGOs.Add(tag, go);
        }

        /// <summary>
        /// Carga una fichero de audio desde la carpeta Resources.
        /// </summary>
        /// <param name="tag">Tag identificador de la cancion a cargar</param>
        /// <returns>AudioClip con el fichero de audio</returns>
        private AudioClip GetAudioFromResources(SongTags tag)
        {
            string path = Constants.MUSIC_RESOURCES_PATH;

            switch (tag)
            {
                case SongTags.TITLE_THEME:
                    path += Constants.TITLE_THEME;
                    break;

                case SongTags.MENU_THEME:
                    path += Constants.MENU_THEME;
                    break;

                case SongTags.WORLD_1:
                    path += Constants.WORLD_1_THEME;
                    break;

                default:
                    path += Constants.TITLE_THEME;
                    break;
            }

            return Resources.Load(path) as AudioClip;
        }

        /// <summary>
        /// Carga una fichero de audio desde la carpeta Resources.
        /// </summary>
        /// <param name="tag">Tag identificador del efecto de sonido a cargar</param>
        /// <returns>AudioClip con el fichero de audio</returns>
        private AudioClip GetAudioFromResources(SfxTags tag)
        {
            string path = Constants.SFX_RESOURCES_PATH;

            switch (tag)
            {
                case SfxTags.FLAP:
                    path += Constants.FLAP_SFX;
                    break;

                case SfxTags.GRAVITY_SWAP:
                    path += Constants.GRAVITY_SFX;
                    break;

                case SfxTags.DEATH:
                    path += Constants.DEATH_SFX;
                    break;

                case SfxTags.PICKUP_BIG:
                    path += Constants.PICKUP_BIG_SFX;
                    break;

                case SfxTags.PICKUP_SMALL:
                    path += Constants.PICKUP_SMALL_SFX;
                    break;

                case SfxTags.UI_INTERACTED:
                    path += Constants.UI_INTERACTED_SFX;
                    break;

                default:
                    path += Constants.FLAP_SFX;
                    break;
            }

            return Resources.Load(path) as AudioClip;
        }

        /// <summary>
        /// Reproduce un audio. 
        /// Si no esta cargado en su diccionario correspondiente lo carga.
        /// </summary>
        /// <param name="tag">Tag idenficador de la cancion a reproducir.</param>
        /// <param name="volume">Volumen al que se reproduce el audio.</param>
        public void Play(SongTags tag, float volume)
        {
            try
            {
                if (!trackGOs.ContainsKey(tag))
                {
                    LoadSong(tag);
                }

                trackGOs[tag].GetComponent<Song>().Play(Constants.DEFAULT_SONG_PITCH, Constants.DEFAULT_SONG_LOOP, Constants.DEFAULT_SONG_FADE);
            }
            catch (NullReferenceException nre)
            {
                Debug.LogError("No se ha podido cargar la cancion. Game Object eliminado." +
                    "\n" + nre.ToString() +
                    "\n" + nre.HelpLink);
            }
        }

        /// <summary>
        /// Reproduce un audio. 
        /// Si no esta cargado en su diccionario correspondiente lo carga.
        /// </summary>
        /// <param name="tag">Tag idenficador del efecto de sonido a reproducir.</param>
        /// <param name="volume">Volumen al que se reproduce el audio.</param>
        private void Play(SfxTags tag)
        {

            try
            {
                if (!sfxGOs.ContainsKey(tag))
                {
                    LoadSfx(tag);
                }

                sfxGOs[tag].GetComponent<AudioFX>().Play();
            }
            catch (NullReferenceException nre)
            {
                Debug.LogError("No se ha podido carga el sfx. Game Object eliminado." +
                    "\n" + nre.ToString() +
                    "\n" + nre.HelpLink);
            }
        }

        /// <summary>
        /// Pausa una cancion cargada.
        /// </summary>
        /// <param name="tag">Tag identificador de la cancion a pausar.</param>
        public void PauseSong(SongTags tag)
        {
            if (trackGOs.ContainsKey(tag))
                trackGOs[tag].GetComponent<Song>().Pause();
        }

        /// <summary>
        /// Continua la reproduccion de una cancion.
        /// </summary>
        /// <param name="tag">Tag identificador de la cancion a continuar.</param>
        public void ResumeSong(SongTags tag)
        {
            if (trackGOs.ContainsKey(tag))
                trackGOs[tag].GetComponent<Song>().Resume();
        }

        /// <summary>
        /// Para la reproduccion de una cancion cargada.
        /// </summary>
        /// <param name="tag">Tag identificador de la cancion de parar.</param>
        public void StopSong(SongTags tag)
        {
            if (trackGOs.ContainsKey(tag))
                trackGOs[tag].GetComponent<Song>().Stop();

        }

        /// <summary>
        /// Crea nuevos diccionarios para las canciones y los efectos de sonido.
        /// </summary>
        private void ResetAudioTracks()
        {
            trackGOs = new Dictionary<SongTags, GameObject>();
            sfxGOs = new Dictionary<SfxTags, GameObject>();
        }
        #endregion
    }
}

