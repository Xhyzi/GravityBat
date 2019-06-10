using GooglePlayGames;
using GooglePlayGames.BasicApi;
//using UnityEngine.SocialPlatforms;
using UnityEngine;
//using GravityBatConstants;

namespace GPGServices
{
    /// <summary>
    /// Controla los servicios de Google Play Games
    /// </summary>
    public static class PGServices
    {

        /// <summary>
        /// Autentica al usuario en Google Play Games.
        /// </summary>
        /// <returns>bool, indica si la operacion se realizo con exito o no.</returns>
        public static bool AuthenticateUser()
        {
            bool authenticated = false;

            PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().Build();   //.EnableSavedGames().Build();
            PlayGamesPlatform.InitializeInstance(config);
            PlayGamesPlatform.Activate();

            Social.localUser.Authenticate((bool success) =>
            {
                authenticated = success;
                if (success)
                    Debug.Log("Se ha logueado en Google Play Games");
                else
                    Debug.Log("No Se ha logueado en Google Play Games");
            });

            return authenticated;
        }


        /// <summary>
        /// Publica una puntuacion en un ladderboard
        /// </summary>
        /// <param name="score"></param>
        /// <param name="ladderboardIndex"></param>
        /// <returns></returns>
        public static bool PostScoreToLadderboard(long score, int worldIndex, int levelIndex)
        {
            bool posted = false;
            Social.ReportScore(score, GetLadderboardIndex(worldIndex, levelIndex), (bool success) => { posted = success; });
            return posted;
        }

        /// <summary>
        /// Muestra el ladderboard del nivel seleccionado en Google Play Games
        /// </summary>
        /// <param name="worldIndex">mundo del nivel cuyo ranking se mostrara</param>
        /// <param name="levelIndex">indice del nivel cuyo ranking se mostrara</param>
        public static void ShowLadderboard(int worldIndex, int levelIndex)
        {
            PlayGamesPlatform.Instance.ShowLeaderboardUI(GetLadderboardIndex(worldIndex, levelIndex));
        }

        /// <summary>
        /// Devuelve el indice de GPG para el ranking del nivel seleccionado.
        /// </summary>
        /// <param name="worldIndex">Indice del mundo</param>
        /// <param name="levelIndex">Indice del nivel a mostrar</param>
        /// <returns></returns>
        public static string GetLadderboardIndex(int worldIndex, int levelIndex)    
        {
            string ladderboardIndex = "";

            switch (worldIndex)
            {
                case 0:

                    switch (levelIndex)
                    {
                        case 0:
                            ladderboardIndex = GPGSIds.leaderboard_level_11;
                            break;

                        case 1:
                            ladderboardIndex = GPGSIds.leaderboard_level_12;
                            break;

                        case 2:
                            ladderboardIndex = GPGSIds.leaderboard_level_13;
                            break;

                        case 3:
                            ladderboardIndex = GPGSIds.leaderboard_level_14_infinite;
                            break;

                        case 4:
                            ladderboardIndex = GPGSIds.leaderboard_level_15_special;
                            break;
                    }

                    break;
            }

            return ladderboardIndex;
        }
    }
}
