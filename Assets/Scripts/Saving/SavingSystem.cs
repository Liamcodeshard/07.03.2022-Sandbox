using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using System.Text;


namespace RPG.Saving
{
    public class SavingSystem : MonoBehaviour
    {
        private string path = "";

        public void Save(string saveFile)
        {
            // get the path name
            string path = GetPathFromSaveFile(saveFile);

            print("Saving to " + path);

            // we access the file then create (this will overwrite previous data)
            // we can use 'using' in this way to ensure we do not forget to close the stream
            using (FileStream stream = File.Open(path, FileMode.Create))
            {

                // in C# in order to write in hexidecimal UTF8 we use 0x then write the hexidecimal code 
               // stream.WriteByte(0xc2);
               // stream.WriteByte(0xa1);
            
                // or we can get the UTF8 writing byt first getting a byte array
                byte[] bytes = Encoding.UTF8.GetBytes("¡Hola Mundo!");

                // then reading it, starting at index 0- and finishing at length end
                stream.Write(bytes, 0, bytes.Length);

                // must close the stream otherwise it will not be cleaned up - the filehandlers* will get built up and memory error will be thrown
                //stream.Close(); by putting this in a using brackets, we do not need to close the stream
            }
        }

        public void Load(string saveFile)
        {
            string path = GetPathFromSaveFile(saveFile);
            print("Loading from " + saveFile);

            // we access the file then open it
            // we can use 'using' in this way to ensure we do not forget to close the stream
            using (FileStream stream = File.Open(path, FileMode.Open))
            {

                byte[] buffer = new byte[stream.Length];

                // then reading it, starting at index 0- and finishing at length end
                stream.Read(buffer, 0, buffer.Length);

                print(Encoding.UTF8.GetString(buffer));
            }
        
        }


        private string GetPathFromSaveFile(string saveFile)
        {
            // using path.Combine in order to create an appropriate pathway for seperate platforms
            return Path.Combine(Application.persistentDataPath, saveFile+".sav");
        }
    }
}