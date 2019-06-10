using UnityEngine;

/// <summary>
/// Contiene las constantes y las enumeraciones de la aplicacion
/// </summary>
/// https://groups.google.com/d/forum/gravity-bat-alpha-1
namespace GravityBat_Constants
{
    internal static class Constants
    {
        internal const float PLAYER_COLLIDER_OFFSET_Y = -0.07f;         //Valor Y del collider -> -0.05
        internal const float PLAYER_COLLIDER_OFFSET_X = 0.02f;          //Valor X del collider -> 0.04
        internal const int PLAYER_FLYING_TILT_ANGLE = 35;               //Angulo de inclinacion maximo del jugador
        internal const int PLAYER_DEAD_TILT_ANGLE = 90;                 //Angulo de inclinacion maximo del jugador al morir
        internal const float AVATAR_AUTO_FLAP_UPPER_LIMIT = 0.75f;      //Limite superior del auto vuelo (evitara volar por encima de este punto)
        internal const float AVATAR_AUTO_FLAP_LOWER_LIMIT = -0.75f;    //Limite inferior del auto vuelo (forzara el vuelo por debajo de este punto)
        internal const float AVATAR_AUTO_FLAP_PERCENTAGE = 4;           //Frecuencia con la que se activa el auto vuelo en % por frame
        internal const float AVATAR_ALIVE_TILT_SPEED = 1f;              //Velocidad a la que se inclina el jugador vivo.
        internal const float AVATAR_DEAD_TILT_SPEED = 1.5f;             //Velocidad a la que se inclina el jugador muerto.
        internal const float AUTO_REPLAY_WAIT_TIME = 1f;                //Tiempo de espera hasta iniciar el reintento automatico en segundos.
        internal const float GAME_OVER_PAGE_WAIT_TIME = 1f;             //Tiempo de espera hasta iniciar la pantalla de Game Over en segundos.
        internal const short GRAVITY_CHUNK = 5;                         //Valor del conjunto de gravedad.
        internal const short MAX_GRAVITY_POWER = GRAVITY_CHUNK * 5;     //Valor maximo de la energia de gravedad.
        internal const short BASE_GRAVITY_POWER = GRAVITY_CHUNK * 2;    //Valor base de la energia de gravedad.
        internal const float GRAVITY_FORCE_Y = 9.8f;                    //Valor de la fuerza de la gravedad en el eje y.
        internal const float GRAVITY_BAR_MAX_WIDTH = 1f;                //Ancho maximo de la barra de energia de gravedad.
        internal const float GRAVITY_BAR_NULL_WIDTH = 0.029f;           //Ancho del valor null de la barra de energia de gravedad.
        internal const float GRAVITY_BAR_UNIT_WIDTH = 0.033f;           //Ancho de cada unidad de energia en la barra de energia de gravedad.
        internal const float GRAVITY_BAR_CHUNK_SPACING = GRAVITY_BAR_NULL_WIDTH;    //Ancho del espacio entre chunks de la barra de energia de gravedad.
        internal const float GRAVITY_BAR_CHUNK = 0.165f;                //Ancho de un chunk de la barra de energia de gravedad.
        internal const int LEVEL_FRAGMENT_OFFSET = 25;                  //Offset de separacion entre cada fragmento de nivel.
        internal const float PLAYER_FREEZE_AFTER_DEAD_WAIT_TIME = 3f;   //Tiempo de espera hasta congelar al jugador tras la muerte.
        internal const float DEATH_ANIMATION_LENGTH = 1f;               //Tiempo que dura la animacion de muerte del jugador en segundos.
        internal const float DEFAULT_GRAVITY_SCALE = 1f;                //Escala de la gravedad por defecto.
        internal const float DEATH_ANIMATION_GRAVITY_SCALE = 2f;        //Escala de la gravedad en la animacion de muerte.
        internal const float DEATH_ANIMATION_IMPULSE = 12f;             //Impulso al jugador en la animacion de muerte.
        internal const float DEFAULT_TIME_SCALE = 1f;                   //Escala del tiempo por defecto.
        internal const float FREEZE_TIME_SCALE = 0f;                    //Escala del tiempo congelada.
        internal const int DEFAULT_MULTIPLIER = 1;                      //Valor del multiplicador de puntos por defecto.
        internal const float DUMMY_BOUNCE_SPEED = 0.5f;                 //Velocidad a la que se balancea el avatar en la pantalla de titulo.
        internal const float DUMMY_BOUNCE_RIGHT_LIMIT = -7f;           //Limite derecho del balanceo del avatar en la pantalla de titulo.
        internal const float DUMMY_BOUNCE_LEFT_LIMIT = -7.5f;          //Limite izquierdo del balanceo del avatar en la pantalla de titulo.
        internal const float DUMMY_TAP_IMPULSE = 1.5f;                  //Impulso del avatar al volar en la pantalla de titulo.
        internal const float DUMMY_TAP_GREAT_IMPULSE = 5f;              //Impulso del avatar al volar MUY ALTO en la pantalla de titulo.
        internal const float DEFAULT_SONG_PITCH = 1f;                   //Pitch por defecto de las canciones.
        internal const bool DEFAULT_SONG_LOOP = true;                   //Valor por defecto para el loop de las canciones.
        internal const bool DEFAULT_SONG_FADE = true;                   //Valor por defecto para el efecto de desvanecimiento de las canciones.
        internal const int MULTIPLIER_MAX_VALUE = 99;                   //Valor maximo del multiplicador.
        internal const float MUSIC_FADE_SPEED = 0.03f;                  //Velocidad a la que se produce el fade de la musica. % de reduccion por frame.
        internal const float HIDDEN_PATH_FADE_OUT_SPEED = 0.02f;        //Velocidad a la que se produce el fade de los caminos ocultos. % de transparencia por frame.
        internal const float BUTTON_STATS_TIME_SPAWN = 0.4f;            //Tiempo de retraso hasta la aparicion de los botones en segundos.
        internal const float BUTTON_PLAY_TIME_SPAWN = 0f;               //
        internal const float BUTTON_INFO_TIME_SPANW = 0.3f;             //
        internal const float BUTTON_SHARE_TIME_SPAWN = 0.35f;           //
        internal const float BUTTON_CONFIG_TIME_SPAWN = 0.25f;          //
        internal const float BUTTON_BACK_TIME_SPAWN = 0.2f;             //
        internal const int WORLD_AMMOUNT = 6;                           //Cantidad de Mundos.
        internal const int LEVELS_PER_WORLD_AMMOUNT = 5;                //Cantidad de niveles por Mundo.
        internal const int BASE_UNLOCKED_LEVEL_WORLD_INDEX = 0;         //Index del Mundo desbloqueado por defecto.
        internal const int BASE_UNLOCKED_LEVEL_INDEX = 0;               //Index del nivel desbloqueado por defecto.
        internal const bool LEVEL_LOCKED = false;                       //Nivel bloqueado.
        internal const bool LEVEL_UNLOCKED = true;                      //Nivel desbloqueado.
        internal const bool LEVEL_INCOMPLETE = false;                   //Nivel Incompleto.
        internal const bool DIAMOND_NOT_OBTAINED = false;               //Diamante no obtenido.
        internal const float DEFAULT_MUSIC_VOLUME = 0.7f;               //Volumen de la musica por defecto en %.
        internal const float DEFAULT_SFX_VOLUME = 0.5f;                 //Volumen de los efectos de sonido por defecto en %.
        internal const float DEFAULT_BUTTON_OPACITY = 0.4f;             //Opacidad de los botones de control por defecto en %.
        internal const bool DEFAULT_AUTO_RETRY = false;                 //Valor por defecto de la opcion de auto reintentar.
        internal const bool DEFAULT_MUSIC_ENABLED = true;               //Valor por defecto de la opcion de habilitar la musica.
        internal const bool DEFAULT_SFX_ENABLED = true;                 //Valor por defecto de la opcion de habilitar los efectos de sonido.
        internal const bool DEFAULT_SHOW_LEVEL_PANEL = true;            //Valor por defecto de la opcion de mostrar los paneles de control.
        internal const bool DEFAULT_SWAP_LEVEL_PANEL = false;           //Valor por defecto de la opcion para intercambiar los controles del juego.
        internal const bool DEFAULT_ENABLE_TUTORIALS = true;            //Valor por defecto de la opcion para activar los tutoriales.
        internal const int MAX_FPS = 60;                                //Valor maximo de frames per second.
        internal const float SCORE_TEXT_ANIMATION_SPEED = 0.005f;       //Velocidad a la que se produce la animacion del texto de puntuacion al recoger un objeto. (Estilo 'Super Mario')
        internal const float SPEED_MODIFIER_INCREMENT = 0.02f;          //Incremento de la velocidad por fragmento de nivel en los niveles infinitos.

        //Paths
        internal const string TITLE_THEME = "title_theme";
        internal const string MENU_THEME = "menu_theme";
        internal const string WORLD_1_THEME = "world_1";
        internal const string MUSIC_RESOURCES_PATH = "music/";
        internal const string SFX_RESOURCES_PATH = "sfx/";
        internal const string TEXT_RESOURCES_PATH = "texts/";
        internal const string PREFAB_PATH = "prefabs/";
        internal const string TUTORIAL_RESOURCES_PATH = PREFAB_PATH + "tutorials/";
        internal const string PREFAB_LEVEL_RESOURCES_PATH = PREFAB_PATH + "level/";
        internal const string POP_UP_TEXT_PATH = PREFAB_PATH + "pop_up_text/";
        internal const string READY_POPUP_TEXT_PATH = POP_UP_TEXT_PATH + "ready_text";
        internal const string START_POPUP_TEXT_PATH = POP_UP_TEXT_PATH + "start_text";
        internal const string START_TRIGGER = "start_trigger";
        internal const string END_TRIGGER = "end_trigger";
        internal const string ITEMS_PATH = PREFAB_PATH + "items/";
        internal const string CHERRY_COIN_PATH = "cherry_coin";
        internal const string DIAMOND_COIN_PATH = "diamond_coin";
        internal const string GEM_GRAVITY_PATH = "gem_gravity";
        internal const string PHYSHIC_OBJECTS_PATH = PREFAB_PATH + "physhic_objects/";
        internal const string CRATE_PATH = "boxes/crate";
        internal const string BUTTON_BLOCK_CHANGER = PREFAB_PATH + "ui/buttons/btn_block_changer";
        internal const string TUTORIAL_CANVAS = "tutorial_canvas";
        internal const string TUTORIAL_TAP = "tutorial_tap";
        internal const string TUTORIAL_SWAP = "tutorial_swap";
        internal const string TUTORIAL_SWAP_CONSECUENCES = "tutorial_swap_consecuences";
        internal const string TUTORIAL_POINTS = "tutorial_points";
        internal const string TUTORIAL_MULTIPLIER = "tutorial_multiplier";
        internal const string DEATH_SFX = "death_sfx";
        internal const string FLAP_SFX = "flap_sfx";
        internal const string GRAVITY_SFX = "gravity_sfx";
        internal const string PICKUP_BIG_SFX = "pickup_1_sfx";
        internal const string PICKUP_SMALL_SFX = "pickup_2_sfx";
        internal const string UI_INTERACTED_SFX = "ui_interacted_sfx";
        internal const string TEXT_LEVEL_GO_TAG = "LevelText";
        internal const string TEXT_SCORE_GO_TAG = "ScoreText";
        internal const string TEXT_DIAMONDS_GO_TAG = "DiamondsText";
        internal const string PLAYER_GO_TAG = "Player";
        internal const string SAVE_FILE_NAME = "/gravity.sav";
        internal const string APP_LINK = "https://play.google.com/store/apps/details?id=com.xhyz.gravitybat";   //Enlace a la pagina de la app en la Play Store
    }

    public enum GameState { TITLE_SCREEN, LEVEL_SELECTION_MENU, LEVEL }                             //Posibles estados del juego
    public enum TitleScreenState { IDLE, OPTIONS, STATS, CREDITS, LANGUAGE }                        //Posibles estados de la pantalla de titulo
    public enum LevelState { INIT, RUNNING, GAME_PAUSED, TUTORIAL, DEAD, GAME_OVER, COMPLETED }     //Posibles estados de la pantalla de nivel
    public enum LSMState { IDLE, LEVEL_MENU, ERROR_INFO_MENU, LEVEL_STATS_MENU }                    //Posibles estados de la pantalla de seleccion de niveles
    public enum AvatarState { IDLE_FLYING, MOVING }                                                 //Posibles estados del avatar
    public enum Direction { RIGHT, LEFT }                                                           //Direcciones
    public enum GlobalStats_TAGS { TAPS, SWAPS, COMBO, DIAMONDS, LEVELS, SECRET_LEVELS, TOTAL_ATTEMPTS, RECORD }    //Tags de las estadisticas globales
    public enum LevelStats_TAGS { TAPS, SWAPS, COMBO, DIAMONDS, ATTEMPTS, RECORD }                  //Tags de las estadisticas de cada nivel
    public enum AttemptStats { TAPS, SWAPS, MUTIPLIER, DIAMONDS, SPEED, SCORE }                     //Tags de las estadisticas de cada intento
    public enum Abilities { BLOCK_CHANGER, GHOST }                                                  //Habilidades obtenibles
    public enum AudioState { IDLE, STARTED, PAUSED, STOPPED }                                       //Posibles estados del audio
    public enum TutorialTags { TAP, SWAP, SWAP_CONSECUENCES, POINTS, MULTIPLIER }                   //Tags de cada uno de los tutoriales
    public enum ConfigPageState { MAIN, LANGUAGE }                                                  //Posibles estados de la pagina de configuracion
    public enum SongTags { TITLE_THEME, MENU_THEME, WORLD_1 }                                       //Tags de las canciones del juego.
    public enum SfxTags { DEATH, FLAP, GRAVITY_SWAP, PICKUP_BIG, PICKUP_SMALL, UI_INTERACTED }      //Tags de los efectos sonoros del juego.
    public enum Biome { CAVE, OUTSIDE, WATER};                                                      //Tags de los biomas

    /// <summary>
    /// Tags utilizados para la identificacion de los diferentes strings del juego
    /// </summary>
    public enum TEXT_TAG
    {
        TXT_STATS, TXT_TAPS, TXT_SWAPS, TXT_MULTIPLIER, TXT_DIAMONDS, TXT_COMPLETED_LEVELS,
        TXT_SPECIAL_LEVELS, TXT_ATTEMPTS, TXT_RECORD, TXT_OPTIONS, TXT_LANGUAGE, TXT_LANGUAGES,
        TXT_MUSIC, TXT_SFX, TXT_AUTO_RETRY, TXT_ENABLE_MUSIC, TXT_ENABLE_SFX, TXT_SHOW_UI,
        TXT_SWAP_UI, TXT_CREDITS, TXT_LEVEL, TXT_WORLD, TXT_INFINITE, TXT_SPECIAL, TXT_BACK,
        TXT_RANKING, TXT_PLAY, TXT_LEVEL_BLOCKED, TXT_WORLD_BLOCKED, TXT_LEVEL_BLOCKED_INFO,
        TXT_SPECIAL_LEVEL_BLOCKED_INFO, TXT_WORLD_BLOCKED_INFO, TXT_GAME_PAUSED, TXT_GAME_OVER,
        TXT_LEVEL_COMPLETED, TXT_NEW_RECORD, TXT_MENU, TXT_RESUME, TXT_REPLAY, TXT_LANGUAGE_NAME,
        TXT_TOTAL_TAPS, TXT_TOTAL_SWAPS, TXT_TOTAL_MULTIPLIER, TXT_TOTAL_DIAMOND, TXT_TOTAL_ATTEMPTS,
        TXT_SCORE, TXT_CREDITS_GRAPHIC, TXT_CREDITS_MUSIC, TXT_CREDITS_FONTS, TXT_CREDITS_SPECIAL,
        TXT_SPEED, TXT_INFINITE_COMPLETED, TXT_SHARE_APP_MSG, TXT_SHARE_APP_INFO, TXT_SHARE_SCORE_MSG,
        TXT_SHARE_SCORE_INFO, TXT_TUTORIAL, TXT_WORKING_ON_WORLD_2, TXT_WELCOME
    }

    /// <summary>
    /// Tags utilizados para los strings obtenidos a partir de los datos guardados en el juego
    /// </summary>
    public enum DATA_TXT_TAG
    {
        GLOBAL_DATA_TAPS, GLOBAL_DATA_SWAPS, GLOBAL_DATA_MULTIPLIER, GLOBAL_DATA_DIAMONDS,
        GLOBAL_DATA_LEVELS, GLOBAL_DATA_SECRET_LEVELS, GLOBAL_DATA_ATTEMPTS, GLOBAL_DATA_RECORD,
        DATA_TAPS, DATA_SWAPS, DATA_MULTIPLIER, DATA_DIAMONDS, DATA_ATTEMPTS, DATA_RECORD
    }

    /// <summary>
    /// Tags utilizados para los strings de cada uno de los tutoriales del juego
    /// </summary>
    public enum TUTORIAL_TXT_TAG
    {
        TXT_TUTORIAL_TAP, TXT_TUTORIAL_SWAP, TXT_TUTORIAL_SCORE, TXT_TUTORIAL_SWAP_CONSECUENCES,
        TXT_TUTORIAL_MULTIPLIER_CHAIN, TXT_TUTORIAL_SWAP_CONSECUENCES_2, TXT_ENERGY, TXT_MULTIPLIER
    }

    /// <summary>
    /// Tags de cada uno de los idiomas del juego
    /// </summary>
    [System.Serializable]
    public enum Languages { ENGLISH, SPANISH, FRENCH, GERMAN }
}