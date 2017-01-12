// ---------- // 
// Librairies //
// ---------- //

using System.IO;
using Newtonsoft.Json;

namespace VoidKeysInfo
{
    class Json
    {
        // ----------------- // 
        // Global Variables  //
        // ----------------- //
        
        private const string PATH = @"..\..\..\..\..\JSON\MissionDecks.json"; //relative path to Json file
        private const string TOKEN = "VoidKeyMissionRewards";
        private const string KEY_CHOICE = "Locations";
        private const string DROP_TABLE = "Rotation A";
        private static string jSonFile = Path.Combine(Directory.GetCurrentDirectory(), PATH);

        // ---------- // 
        // Functions  //
        // ---------- //

        /**  
         * Loads json data in an array for parsing  
         *   
         * @return a dynamic array containing all the data in the json file
         */
        public static dynamic LoadJson()
        {
            using (StreamReader r = new StreamReader(jSonFile))
            {
                string file = r.ReadToEnd();
                dynamic  jSon = JsonConvert.DeserializeObject(file);
                return jSon;
            }

        }

        /**  
         * Parses the json file to find the key chosen by the user, and gets all the informations
         * about the key, like drops, drop rates and ducats values
         *
         * @param key, the name of the void key which the user wants to find the informations
         * @param jSon, the array containing all the data about drops in the game
         * @return the data about the key chosen
         */
        public static string[] outputKeyInfo(string key, dynamic jSon)
        {
            key = key.ToUpper();
            string[] drops = null;
            foreach (dynamic category in jSon)
            {
                string catName = category.Name;
                if (catName.Contains(TOKEN))
                {
                    foreach (dynamic info in category.Children())
                    {
                        string tempKey = info[KEY_CHOICE][0];
                        if (tempKey.Contains(key))
                        {
                            return drops = info[DROP_TABLE].ToObject<string[]>();  
                        }
                    }
                }
            }
            return drops;
        }
    }
}
