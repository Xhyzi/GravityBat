
using GravityBat_Constants;
using UnityEngine;
using System.Xml;
using System.Collections.Generic;
using System;


namespace GravityBat_Localization
{
    /// <summary>
    /// Contiene diccionarios con los strings del juego. 
    /// Estos strings son cargados en memoria al cargar una escena, teniendo en cuenta 
    /// los textos necesitados en la misma y el idioma seleccionado en la configuracion del juego.
    /// Los textos son cargados desde documentos xml en 'Assets/Resources/texts', habiendo un xml por idioma.
    /// La carga en memoria evita el acceso constante a los documentos.
    /// </summary>
    public static class Strings
    {
        #region Strings Dictionaries
        /// <summary>
        /// Diccionario con los strings del juego.
        /// </summary>
        private static Dictionary<TEXT_TAG, string> stringsDictionary;

        /// <summary>
        /// Diccionario con los strings de datos del juego (contienen estadisticas y no se encuentran en el xml ni dependen del idioma).
        /// </summary>
        private static Dictionary<DATA_TXT_TAG, string> dataDictionary;

        /// <summary>
        /// Diccionario con los strings mostrados en los tutoriales.
        /// </summary>
        private static Dictionary<TUTORIAL_TXT_TAG, string> tutorialsDictionary;
        #endregion

        #region Text Load Methods
        /// <summary>
        /// Carga en memoria los textos de la pantalla de inicio
        /// </summary>
        public static void LoadTitleScreenTexts()
        {
            stringsDictionary = new Dictionary<TEXT_TAG, string>();
            stringsDictionary.Add(TEXT_TAG.TXT_STATS, LoadString(TEXT_TAG.TXT_STATS));                          //Estadisticas
            stringsDictionary.Add(TEXT_TAG.TXT_TAPS, LoadString(TEXT_TAG.TXT_TAPS));                            //Toques
            stringsDictionary.Add(TEXT_TAG.TXT_SWAPS, LoadString(TEXT_TAG.TXT_SWAPS));                          //Cambios
            stringsDictionary.Add(TEXT_TAG.TXT_MULTIPLIER, LoadString(TEXT_TAG.TXT_MULTIPLIER));                //Multiplicador
            stringsDictionary.Add(TEXT_TAG.TXT_DIAMONDS, LoadString(TEXT_TAG.TXT_DIAMONDS));                    //Diamantes
            stringsDictionary.Add(TEXT_TAG.TXT_COMPLETED_LEVELS, LoadString(TEXT_TAG.TXT_COMPLETED_LEVELS));    //Niveles completados
            stringsDictionary.Add(TEXT_TAG.TXT_SPECIAL_LEVELS, LoadString(TEXT_TAG.TXT_SPECIAL_LEVELS));        //Niveles especiales
            stringsDictionary.Add(TEXT_TAG.TXT_ATTEMPTS, LoadString(TEXT_TAG.TXT_ATTEMPTS));                    //Intentos
            stringsDictionary.Add(TEXT_TAG.TXT_RECORD, LoadString(TEXT_TAG.TXT_RECORD));                        //Record
            stringsDictionary.Add(TEXT_TAG.TXT_OPTIONS, LoadString(TEXT_TAG.TXT_OPTIONS));                      //Opciones
            stringsDictionary.Add(TEXT_TAG.TXT_LANGUAGE, LoadString(TEXT_TAG.TXT_LANGUAGE));                    //Idioma
            stringsDictionary.Add(TEXT_TAG.TXT_LANGUAGES, LoadString(TEXT_TAG.TXT_LANGUAGES));                  //Idiomas
            stringsDictionary.Add(TEXT_TAG.TXT_MUSIC, LoadString(TEXT_TAG.TXT_MUSIC));                          //Musica
            stringsDictionary.Add(TEXT_TAG.TXT_SFX, LoadString(TEXT_TAG.TXT_SFX));                              //SFX
            stringsDictionary.Add(TEXT_TAG.TXT_AUTO_RETRY, LoadString(TEXT_TAG.TXT_AUTO_RETRY));                //Reintentar
            stringsDictionary.Add(TEXT_TAG.TXT_ENABLE_MUSIC, LoadString(TEXT_TAG.TXT_ENABLE_MUSIC));            //Activar Musica
            stringsDictionary.Add(TEXT_TAG.TXT_ENABLE_SFX, LoadString(TEXT_TAG.TXT_ENABLE_SFX));                //Activar SFX
            stringsDictionary.Add(TEXT_TAG.TXT_SHOW_UI, LoadString(TEXT_TAG.TXT_SHOW_UI));                      //Mostrar botones nivel
            stringsDictionary.Add(TEXT_TAG.TXT_SWAP_UI, LoadString(TEXT_TAG.TXT_SWAP_UI));                      //Cambiar botones nivel
            stringsDictionary.Add(TEXT_TAG.TXT_CREDITS, LoadString(TEXT_TAG.TXT_CREDITS));                      //Creditos
            stringsDictionary.Add(TEXT_TAG.TXT_LANGUAGE_NAME, GetCurrentLanguageName());                               //Nombre del idioma
            stringsDictionary.Add(TEXT_TAG.TXT_TOTAL_TAPS, LoadString(TEXT_TAG.TXT_TOTAL_TAPS));                //Toques totales
            stringsDictionary.Add(TEXT_TAG.TXT_TOTAL_SWAPS, LoadString(TEXT_TAG.TXT_TOTAL_SWAPS));              //Cambios totales
            stringsDictionary.Add(TEXT_TAG.TXT_TOTAL_MULTIPLIER, LoadString(TEXT_TAG.TXT_TOTAL_MULTIPLIER));    //Mayor multiplicador
            stringsDictionary.Add(TEXT_TAG.TXT_TOTAL_DIAMOND, LoadString(TEXT_TAG.TXT_TOTAL_DIAMOND));          //Diamantes totales
            stringsDictionary.Add(TEXT_TAG.TXT_TOTAL_ATTEMPTS, LoadString(TEXT_TAG.TXT_TOTAL_ATTEMPTS));        //Intentos totales
            stringsDictionary.Add(TEXT_TAG.TXT_CREDITS_FONTS, LoadString(TEXT_TAG.TXT_CREDITS_FONTS));          //'Fuentes de texto'
            stringsDictionary.Add(TEXT_TAG.TXT_CREDITS_GRAPHIC, LoadString(TEXT_TAG.TXT_CREDITS_GRAPHIC));      //'Graficos'
            stringsDictionary.Add(TEXT_TAG.TXT_CREDITS_MUSIC, LoadString(TEXT_TAG.TXT_CREDITS_MUSIC));          //'Musica'
            stringsDictionary.Add(TEXT_TAG.TXT_CREDITS_SPECIAL, LoadString(TEXT_TAG.TXT_CREDITS_SPECIAL));      //'Agradecimientos especiales'
            stringsDictionary.Add(TEXT_TAG.TXT_REPLAY, LoadString(TEXT_TAG.TXT_REPLAY));
            stringsDictionary.Add(TEXT_TAG.TXT_SHARE_APP_MSG, LoadString(TEXT_TAG.TXT_SHARE_APP_MSG));
            stringsDictionary.Add(TEXT_TAG.TXT_SHARE_APP_INFO, LoadString(TEXT_TAG.TXT_SHARE_APP_INFO));
            stringsDictionary.Add(TEXT_TAG.TXT_TUTORIAL, LoadString(TEXT_TAG.TXT_TUTORIAL));
            stringsDictionary.Add(TEXT_TAG.TXT_WELCOME, LoadString(TEXT_TAG.TXT_WELCOME));

            GameManager.Instance.Raise_TextLoadedToMemory();
        }

        /// <summary>
        /// Carga en memoria los texto de la pantalla de seleccion de nivel.
        /// </summary>
        public static void LoadLevelSelectionTexts()
        {
            stringsDictionary = new Dictionary<TEXT_TAG, string>();
            stringsDictionary.Add(TEXT_TAG.TXT_LEVEL, LoadString(TEXT_TAG.TXT_LEVEL));                          //Nivel
            stringsDictionary.Add(TEXT_TAG.TXT_WORLD, LoadString(TEXT_TAG.TXT_WORLD));                          //Mundo
            stringsDictionary.Add(TEXT_TAG.TXT_INFINITE, LoadString(TEXT_TAG.TXT_INFINITE));                    //Infinito
            stringsDictionary.Add(TEXT_TAG.TXT_SPECIAL, LoadString(TEXT_TAG.TXT_SPECIAL));                      //Especial
            stringsDictionary.Add(TEXT_TAG.TXT_BACK, LoadString(TEXT_TAG.TXT_BACK));                            //Volver
            stringsDictionary.Add(TEXT_TAG.TXT_RANKING, LoadString(TEXT_TAG.TXT_RANKING));                      //Ranking
            stringsDictionary.Add(TEXT_TAG.TXT_STATS, LoadString(TEXT_TAG.TXT_STATS));                          //Estadisticas
            stringsDictionary.Add(TEXT_TAG.TXT_PLAY, LoadString(TEXT_TAG.TXT_PLAY));                            //Jugar
            stringsDictionary.Add(TEXT_TAG.TXT_TAPS, LoadString(TEXT_TAG.TXT_TAPS));                            //Toques
            stringsDictionary.Add(TEXT_TAG.TXT_SWAPS, LoadString(TEXT_TAG.TXT_SWAPS));                          //Cambios
            stringsDictionary.Add(TEXT_TAG.TXT_MULTIPLIER, LoadString(TEXT_TAG.TXT_MULTIPLIER));                //Multiplicador
            stringsDictionary.Add(TEXT_TAG.TXT_DIAMONDS, LoadString(TEXT_TAG.TXT_DIAMONDS));                    //Diamantes
            stringsDictionary.Add(TEXT_TAG.TXT_TOTAL_TAPS, LoadString(TEXT_TAG.TXT_TOTAL_TAPS));                //Toques totales
            stringsDictionary.Add(TEXT_TAG.TXT_TOTAL_SWAPS, LoadString(TEXT_TAG.TXT_TOTAL_SWAPS));              //Cambios totales
            stringsDictionary.Add(TEXT_TAG.TXT_TOTAL_MULTIPLIER, LoadString(TEXT_TAG.TXT_TOTAL_MULTIPLIER));    //Mayor multiplicador
            stringsDictionary.Add(TEXT_TAG.TXT_TOTAL_DIAMOND, LoadString(TEXT_TAG.TXT_TOTAL_DIAMOND));          //Diamantes totales
            stringsDictionary.Add(TEXT_TAG.TXT_TOTAL_ATTEMPTS, LoadString(TEXT_TAG.TXT_TOTAL_ATTEMPTS));        //Intentos totales
            stringsDictionary.Add(TEXT_TAG.TXT_ATTEMPTS, LoadString(TEXT_TAG.TXT_ATTEMPTS));                    //Intentos
            stringsDictionary.Add(TEXT_TAG.TXT_RECORD, LoadString(TEXT_TAG.TXT_RECORD));                        //Record
            stringsDictionary.Add(TEXT_TAG.TXT_LEVEL_BLOCKED, LoadString(TEXT_TAG.TXT_LEVEL_BLOCKED));          //Nivel Bloqueado
            stringsDictionary.Add(TEXT_TAG.TXT_LEVEL_BLOCKED_INFO, LoadString(TEXT_TAG.TXT_LEVEL_BLOCKED_INFO));//Info sobre nivel bloqueado
            stringsDictionary.Add(TEXT_TAG.TXT_SPECIAL_LEVEL_BLOCKED_INFO, LoadString(TEXT_TAG.TXT_SPECIAL_LEVEL_BLOCKED_INFO));    //Info sobre nivel especial bloqueado
            stringsDictionary.Add(TEXT_TAG.TXT_WORLD_BLOCKED, LoadString(TEXT_TAG.TXT_WORLD_BLOCKED));          //Mundo bloqueado
            stringsDictionary.Add(TEXT_TAG.TXT_WORLD_BLOCKED_INFO, LoadString(TEXT_TAG.TXT_WORLD_BLOCKED_INFO));//Info sobre mundo bloqueado
            stringsDictionary.Add(TEXT_TAG.TXT_WORKING_ON_WORLD_2, LoadString(TEXT_TAG.TXT_WORKING_ON_WORLD_2));

            GameManager.Instance.Raise_TextLoadedToMemory();
        }

        /// <summary>
        /// Carga en memoria los textos utilizados en el nivel
        /// </summary>
        public static void LoadLevelTexts()
        {
            stringsDictionary = new Dictionary<TEXT_TAG, string>();
            stringsDictionary.Add(TEXT_TAG.TXT_GAME_PAUSED, LoadString(TEXT_TAG.TXT_GAME_PAUSED));          //Partida pausada
            stringsDictionary.Add(TEXT_TAG.TXT_GAME_OVER, LoadString(TEXT_TAG.TXT_GAME_OVER));              //Partida finalizada
            stringsDictionary.Add(TEXT_TAG.TXT_LEVEL_COMPLETED, LoadString(TEXT_TAG.TXT_LEVEL_COMPLETED));  //Nivel completado
            stringsDictionary.Add(TEXT_TAG.TXT_NEW_RECORD, LoadString(TEXT_TAG.TXT_NEW_RECORD));            //Nuevo record
            stringsDictionary.Add(TEXT_TAG.TXT_MENU, LoadString(TEXT_TAG.TXT_MENU));                        //Menu
            stringsDictionary.Add(TEXT_TAG.TXT_RESUME, LoadString(TEXT_TAG.TXT_RESUME));                    //Continuar
            stringsDictionary.Add(TEXT_TAG.TXT_REPLAY, LoadString(TEXT_TAG.TXT_REPLAY));                    //Volver a jugar
            stringsDictionary.Add(TEXT_TAG.TXT_TAPS, LoadString(TEXT_TAG.TXT_TAPS));                        //Toques
            stringsDictionary.Add(TEXT_TAG.TXT_SWAPS, LoadString(TEXT_TAG.TXT_SWAPS));                      //Cambios
            stringsDictionary.Add(TEXT_TAG.TXT_MULTIPLIER, LoadString(TEXT_TAG.TXT_MULTIPLIER));            //Multiplicador
            stringsDictionary.Add(TEXT_TAG.TXT_DIAMONDS, LoadString(TEXT_TAG.TXT_DIAMONDS));                //Diamantes
            stringsDictionary.Add(TEXT_TAG.TXT_SCORE, LoadString(TEXT_TAG.TXT_SCORE));                      //Puntuacion
            stringsDictionary.Add(TEXT_TAG.TXT_LEVEL, LoadString(TEXT_TAG.TXT_LEVEL));                      //Nivel
            stringsDictionary.Add(TEXT_TAG.TXT_SPEED, LoadString(TEXT_TAG.TXT_SPEED));                      //Velocidad
            stringsDictionary.Add(TEXT_TAG.TXT_INFINITE_COMPLETED, LoadString(TEXT_TAG.TXT_INFINITE_COMPLETED));
            stringsDictionary.Add(TEXT_TAG.TXT_SHARE_SCORE_MSG, LoadString(TEXT_TAG.TXT_SHARE_SCORE_MSG));
            stringsDictionary.Add(TEXT_TAG.TXT_SHARE_SCORE_INFO, LoadString(TEXT_TAG.TXT_SHARE_SCORE_INFO));

            GameManager.Instance.Raise_TextLoadedToMemory();
        }

        /// <summary>
        /// Carga en memoria los textos de las estadisticas globales del juego
        /// </summary>
        public static void LoadGlobalDataTexts()
        {
            dataDictionary = new Dictionary<DATA_TXT_TAG, string>();
            dataDictionary.Add(DATA_TXT_TAG.GLOBAL_DATA_TAPS, GameManager.Instance.Data.TotalTaps().ToString());            //Taps gloables
            dataDictionary.Add(DATA_TXT_TAG.GLOBAL_DATA_SWAPS, GameManager.Instance.Data.TotalSwaps().ToString());          //Cambios globales
            dataDictionary.Add(DATA_TXT_TAG.GLOBAL_DATA_MULTIPLIER, GameManager.Instance.Data.LongestCombo().ToString());   //Combo mas largo
            dataDictionary.Add(DATA_TXT_TAG.GLOBAL_DATA_DIAMONDS, GameManager.Instance.Data.TotalDiamonds().ToString());    //Diamantes totales
            dataDictionary.Add(DATA_TXT_TAG.GLOBAL_DATA_LEVELS, GameManager.Instance.Data.LevelsCompleted().ToString());    //Niveles completados
            dataDictionary.Add(DATA_TXT_TAG.GLOBAL_DATA_SECRET_LEVELS, GameManager.Instance.Data.SecretLevelsCompleted().ToString());   //Niveles secretos completados
            dataDictionary.Add(DATA_TXT_TAG.GLOBAL_DATA_ATTEMPTS, GameManager.Instance.Data.TotalAttempts().ToString());    //Intentos totales
            dataDictionary.Add(DATA_TXT_TAG.GLOBAL_DATA_RECORD, GameManager.Instance.Data.RecordScore().ToString());        //Puntuacion record

            GameManager.Instance.Raise_DataTextLoadedToMemory();
        }

        /// <summary>
        /// Carga en memoria los textos de las estadisticas de un nivel del juego
        /// </summary>
        public static void LoadLevelDataTexts(int worldIndex, int levelIndex)
        {
            dataDictionary = new Dictionary<DATA_TXT_TAG, string>();
            dataDictionary.Add(DATA_TXT_TAG.DATA_TAPS, GameManager.Instance.Data.LevelArray[worldIndex, levelIndex].TotalTaps.ToString());      //Taps del nivel
            dataDictionary.Add(DATA_TXT_TAG.DATA_SWAPS, GameManager.Instance.Data.LevelArray[worldIndex, levelIndex].TotalSwaps.ToString());    //Swaps del nivel
            dataDictionary.Add(DATA_TXT_TAG.DATA_MULTIPLIER, GameManager.Instance.Data.LevelArray[worldIndex, levelIndex].HighestMultiplier.ToString());    //Maximo multiplicador del nivel
            dataDictionary.Add(DATA_TXT_TAG.DATA_DIAMONDS, GameManager.Instance.Data.LevelArray[worldIndex, levelIndex].DiamondsCount().ToString());    //Diamantes del nivel
            dataDictionary.Add(DATA_TXT_TAG.DATA_ATTEMPTS, GameManager.Instance.Data.LevelArray[worldIndex, levelIndex].Attempts.ToString());   //Intentos del nivel
            dataDictionary.Add(DATA_TXT_TAG.DATA_RECORD, GameManager.Instance.Data.LevelArray[worldIndex, levelIndex].BestScore.ToString());    //Mejor puntuacion del nivel

            GameManager.Instance.Raise_DataTextLoadedToMemory();
        }

        /// <summary>
        /// Carga en memoria los textos de los tutoriales
        /// </summary>
        public static void LoadTutorialText()
        {
            tutorialsDictionary = new Dictionary<TUTORIAL_TXT_TAG, string>();
            tutorialsDictionary.Add(TUTORIAL_TXT_TAG.TXT_TUTORIAL_TAP, LoadString(TUTORIAL_TXT_TAG.TXT_TUTORIAL_TAP));      //Tutorial de vuelo
            tutorialsDictionary.Add(TUTORIAL_TXT_TAG.TXT_TUTORIAL_SWAP, LoadString(TUTORIAL_TXT_TAG.TXT_TUTORIAL_SWAP));    //Tutorial de cambio de gravedad
            tutorialsDictionary.Add(TUTORIAL_TXT_TAG.TXT_TUTORIAL_SWAP_CONSECUENCES, LoadString(TUTORIAL_TXT_TAG.TXT_TUTORIAL_SWAP_CONSECUENCES));  //Tutorial de consecuencias de Swap
            tutorialsDictionary.Add(TUTORIAL_TXT_TAG.TXT_TUTORIAL_SCORE, LoadString(TUTORIAL_TXT_TAG.TXT_TUTORIAL_SCORE));  //Tutorial de items y puntos
            tutorialsDictionary.Add(TUTORIAL_TXT_TAG.TXT_TUTORIAL_MULTIPLIER_CHAIN, LoadString(TUTORIAL_TXT_TAG.TXT_TUTORIAL_MULTIPLIER_CHAIN));      //Tutorial de gemas
            tutorialsDictionary.Add(TUTORIAL_TXT_TAG.TXT_TUTORIAL_SWAP_CONSECUENCES_2, LoadString(TUTORIAL_TXT_TAG.TXT_TUTORIAL_SWAP_CONSECUENCES_2));
            tutorialsDictionary.Add(TUTORIAL_TXT_TAG.TXT_ENERGY, LoadString(TUTORIAL_TXT_TAG.TXT_ENERGY));
            tutorialsDictionary.Add(TUTORIAL_TXT_TAG.TXT_MULTIPLIER, LoadString(TUTORIAL_TXT_TAG.TXT_MULTIPLIER));

            GameManager.Instance.Raise_TutorialTextLoadedToMemory();
        }
        #endregion

        #region Methods
        /// <summary>
        /// Devuelve el valor de un string, dependiendo del idioma seleccionado en las opciones del juego
        /// </summary>
        /// <param name="tag">Identificador del texto deseado</param>
        /// <returns>string deseado</returns>
        public static string GetString(TEXT_TAG tag)
        {
            try
            {
                return stringsDictionary[tag];
            }
            catch (KeyNotFoundException knfe)
            {
                Debug.LogWarning("No se pudo encontrar el string en el diccionario" +
                   "\nSe ha producido una excepcion: " + knfe.ToString() +
                   "\n" + knfe.HelpLink + " KEY: " + tag);
                return LoadString(tag);
            }

        }

        /// <summary>
        /// Devuelve un string con el valor de la estadistica seleccionada
        /// </summary>
        /// <param name="tag">Etiqueta del dato deseado</param>
        /// <returns>Dato deseado en formato de cadena</returns>
        public static string GetDataString(DATA_TXT_TAG tag)
        {
            try
            {
                return dataDictionary[tag];
            }
            catch (KeyNotFoundException knfe)
            {
                LoadGlobalDataTexts();
                Debug.LogWarning("No se pudo encontrar el string en el diccionario" +
                    "\nSe ha producido una excepcion: " + knfe.ToString() +
                    "\n" + knfe.HelpLink + " KEY: " + tag +
                    "\nSe han cargado los textos de nuevo.");
                return dataDictionary[tag];
            }
            catch (Exception e)
            {
                Debug.LogError("Se ha intentado acceder a los datos de un nivel sin inicializarlo antes. ~.~\"\n"
                    + e.ToString() + " " + e.HelpLink);
                return "0";
            }
        }

        /// <summary>
        /// Devuelve los textos de los tutoriales
        /// </summary>
        /// <param name="tag"></param>
        /// <returns></returns>
        public static string GetTutorialString(TUTORIAL_TXT_TAG tag)
        {
            try
            {
                return tutorialsDictionary[tag];
            }
            catch (KeyNotFoundException knfe)
            {
                LoadGlobalDataTexts();
                Debug.LogWarning("No se pudo encontrar el string en el diccionario" +
                    "\nSe ha producido una excepcion: " + knfe.ToString() +
                    "\n" + knfe.HelpLink + " KEY: " + tag +
                    "\nSe han cargado los textos de nuevo.");
                return tutorialsDictionary[tag];
            }
            catch (Exception e)
            {
                Debug.LogError("Se ha intentado acceder a los datos de un nivel sin inicializarlo antes. ~.~\"\n"
                    + e.ToString() + " " + e.HelpLink);
                return "0";
            }
        }

        /// <summary>
        /// Carga un string desde el fichero xml teniendo en cuenta el idioma seleccionado en la configuracion.
        /// </summary>
        /// <param name="tag">Identificador del texto a cargar</param>
        /// <returns>string deseado</returns>
        private static string LoadString(TEXT_TAG tag)
        {
            TextAsset txtXml;
            XmlDocument doc = new XmlDocument();
            XmlNode node;

            try
            {
                txtXml = Resources.Load<TextAsset>("texts/" + GetStringsFileNameByLanguage());   //Obtiene el fichero xml en funcion del idioma
                doc.LoadXml(txtXml.text);   //Carga el documento xml

                node = doc.DocumentElement.SelectSingleNode("//string[@id='" + tag + "']"); //Obtiene el nodo con Xpath

                return node.InnerText;
            }
            catch (NullReferenceException nre)
            {
                Debug.LogError("No se ha encontrado el fichero xml del idioma: " + GameManager.Instance.Data.Language +
                    "\n" + nre.ToString() + ", " + nre.HelpLink);
                return "";
            }
        }

        /// <summary>
        /// Carga un string desde el fichero xml teniendo en cuenta el idioma seleccionado en la configuracion.
        /// </summary>
        /// <param name="tag">Identificador del texto tutorial a cargar</param>
        /// <returns>string deseado</returns>
        private static string LoadString(TUTORIAL_TXT_TAG tutorial)
        {
            TextAsset txtXml;
            XmlDocument doc = new XmlDocument();
            XmlNode node;

            try
            {
                txtXml = Resources.Load<TextAsset>("texts/" + GetStringsFileNameByLanguage());   //Obtiene el fichero xml en funcion del idioma
                doc.LoadXml(txtXml.text);   //Carga el documento xml

                node = doc.DocumentElement.SelectSingleNode("//string[@id='" + tutorial + "']"); //Obtiene el nodo con Xpath

                return node.InnerText;
            }
            catch (NullReferenceException nre)
            {
                Debug.LogError("No se ha encontrado el fichero xml del idioma: " + GameManager.Instance.Data.Language +
                    "\n" + nre.ToString() + ", " + nre.HelpLink);
                return "";
            }
        }


        /// <summary>
        /// Devuelve el nombre del fichero xml con los strings del juego en funcion del idioma
        /// </summary>
        /// <returns>nombre del fichero xml con los strings</returns>
        private static string GetStringsFileNameByLanguage()
        {
            string fileName = "";

            switch (GameManager.Instance.Data.Language)
            {
                case Languages.ENGLISH:
                    fileName = "strings_en";
                    break;

                case Languages.SPANISH:
                    fileName = "strings_es";
                    break;

                case Languages.FRENCH:
                    fileName = "strings_fr";
                    break;

                case Languages.GERMAN:
                    fileName = "strings_de";
                    break;

                default:
                    fileName = "strings_en";
                    break;
            }

            return fileName;
        }

        /// <summary>
        /// Devuelve el nombre del idioma seleccionado para el juego.
        /// </summary>
        /// <returns></returns>
        private static string GetCurrentLanguageName()
        {
            string lang = "";

            switch (GameManager.Instance.Data.Language)
            {
                case Languages.ENGLISH:
                    lang = "English";
                    break;

                case Languages.SPANISH:
                    lang = "Español";
                    break;

                case Languages.FRENCH:
                    lang = "Français";
                    break;

                case Languages.GERMAN:
                    lang = "Deutsche";
                    break;
            }

            return lang;
        }

        /// <summary>
        /// Devuelve el idioma utilizado en el sistema que ejecuta el juego.
        /// Permite detectar el idioma por defecto.
        /// </summary>
        /// <returns>Idioma del sistema.</returns>
        public static Languages GetSystemLanguages()
        {
            Languages lang;

            switch (Application.systemLanguage)
            {
                case SystemLanguage.English:
                    lang = Languages.ENGLISH;
                    break;

                case SystemLanguage.Spanish:
                    lang = Languages.SPANISH;
                    break;

                case SystemLanguage.French:
                    lang = Languages.FRENCH;
                    break;

                case SystemLanguage.German:
                    lang = Languages.GERMAN;
                    break;

                default:
                    lang = Languages.ENGLISH;
                    break;
            }

            return lang;
        }
    }
    #endregion
}