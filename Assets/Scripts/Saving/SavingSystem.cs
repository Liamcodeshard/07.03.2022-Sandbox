using System;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;

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
                // byte[] bytes = Encoding.UTF8.GetBytes("¡Hola Mundo!");


                Transform playerTransform = GetPlayerTransform();
             //   byte[] buffer = SerializeVector(playerTransform.position);

                BinaryFormatter binaryFormatter = new BinaryFormatter();

                //needs s stream to write to and an object to copy data from
                binaryFormatter.Serialize(stream, playerTransform.position);

                // then overwrite current file, starting at index 0- and finishing at length end
              //  stream.Write(buffer, 0, buffer.Length);
            }



            // must close the stream otherwise it will not be cleaned up - the filehandlers* will get built up and memory error will be thrown
            //stream.Close(); by putting this in a using brackets, we do not need to close the stream


        }
        public void Load(string saveFile)
        {
            string path = GetPathFromSaveFile(saveFile);
            print("Loading from " + saveFile);

            // we access the file then open it  // we can use 'using' in this way to ensure we do not forget to close the stream
            using (FileStream stream = File.Open(path, FileMode.Open))
            {

                byte[] buffer = new byte[stream.Length];
                stream.Read(buffer, 0, buffer.Length); // then reading it, starting at index 0- and finishing at length end // this buffer will contain a vector3

                Transform playerTransform = GetPlayerTransform();
                playerTransform.position = DeSerializeVector(buffer); // we auto set the players transform
            }

        }
        private Transform GetPlayerTransform()
        {
            return GameObject.FindGameObjectWithTag("Player").transform;
        }



        private byte[] SerializeVector(Vector3 vector)
        {
            byte[] vectorBytes = new byte[12];
            BitConverter.GetBytes(vector.x).CopyTo(vectorBytes, 0);
            BitConverter.GetBytes(vector.y).CopyTo(vectorBytes, 4);
            BitConverter.GetBytes(vector.z).CopyTo(vectorBytes, 8);
            return vectorBytes;
        }

        private Vector3 DeSerializeVector(byte[] buffer)
        {
            Vector3 result = new Vector3();
            result.x = BitConverter.ToSingle(buffer, 0);
            result.y = BitConverter.ToSingle(buffer, 4);
            result.z = BitConverter.ToSingle(buffer, 8);
            return result;
        }


        private string GetPathFromSaveFile(string saveFile)
        {
            // using path.Combine in order to create an appropriate pathway for seperate platforms
            return Path.Combine(Application.persistentDataPath, saveFile + ".sav");
        }
    }       
}   
