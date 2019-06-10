using GravityBat_Constants;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

namespace GravityBat_Data
{
    /// <summary>
    /// Clase static que se encarga de guardar los datos en un fichero de objetos.
    /// </summary>
    public static class SaveSystem
    {
        /// <summary>
        /// Ruta en la que el juego guarda el fichero con los datos de la partida.
        /// </summary>
        public static string PATH = Application.persistentDataPath + Constants.SAVE_FILE_NAME;

        /// <summary>
        /// Guarda la partida en un fichero de objetos.
        /// </summary>
        public static void Save()
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream fs = new FileStream(PATH, FileMode.Create);  //Crea stream con el fichero en modo crear
            SaveData data = new SaveData(GameManager.Instance);     //Crea una instancia de los datos a guardar

            try
            {
                bf.Serialize(fs, data); //Serializa los datos y los pasa al stream
            }
            catch (SerializationException se)
            {
                Debug.LogWarning("Se ha producido un error al guardar la partida.\n" + 
                    se.ToString() + "\n" + se.HelpLink);
            }
            finally
            {
                fs.Close();             //Cierra el stream
            }
        }

        /// <summary>
        /// Obtiene un objeto de tipo SaveData de la lectura de un fichero binario para cargar asi los datos de la partida.
        /// Si el fichero de guardado no existe, el programa crea unos datos por defecto.
        /// </summary>
        /// <returns>SaveData con los datos de la partida</returns>
        public static SaveData Load()   
        {
            if (File.Exists(PATH))
            {
                SaveData data;
                BinaryFormatter bf = new BinaryFormatter();
                FileStream fs = new FileStream(PATH, FileMode.Open);

                try
                {
                    data = bf.Deserialize(fs) as SaveData;
                }
                catch (SerializationException se)
                {
                    data = new SaveData();
                    Debug.LogWarning("Ha habido un problema al leer la partida. Se ha creado una nueva partida.\n" +
                        se.ToString() + "\n" + se.HelpLink);
                }
                finally
                {
                    fs.Close();
                }
                return data;
            }
            else
            {
                Debug.Log("Sin partida guardada en " + PATH + "\nSe ha creado una nueva partida.");
                return new SaveData();
            }
        }
    }
}