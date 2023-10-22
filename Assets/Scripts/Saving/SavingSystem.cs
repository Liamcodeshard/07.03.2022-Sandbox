using System;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Specialized;

namespace RPG.Saving
{
    public class SavingSystem : MonoBehaviour
    {
        private string path = "";

        public void Save(string saveFile)
        {
            Dictionary<string, object> state = LoadFile(saveFile);
            CaptureState(state);
            SaveFile(saveFile, state);
        }


        public void Load(string saveFile)
        {


            RestoreState(LoadFile(saveFile));
        }



        private void SaveFile(string saveFile, object state)
        {
            // get the path name
            string path = GetPathFromSaveFile(saveFile);

            print("Saving to " + path);

            using (FileStream stream = File.Open(path, FileMode.Create))
            {
                // get the data we want to serialioze

                Transform playerTransform = GetPlayerTransform();

                // create a bnary formatter
                BinaryFormatter formatter = new BinaryFormatter();

                // ensure the data is serializable
                //  SerializableVector3 position = new SerializableVector3(playerTransform.position);

                //format the data (save the file)
                formatter.Serialize(stream, state);

            }
        }

        private Dictionary<string, object> LoadFile(string saveFile)
        {
            string path = GetPathFromSaveFile(saveFile);
            print("Loading from " + path);
            if (!File.Exists(path))
            {
                return new Dictionary<string, object>();
            }


            // we access the file then open it  // we can use 'using' in this way to ensure we do not forget to close the stream
            using (FileStream stream = File.Open(path, FileMode.Open))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                return (Dictionary<string, object>)formatter.Deserialize(stream);
            }
        }

        private void CaptureState(Dictionary<string, object> state)
        {


            foreach (SaveableEntity saveable in FindObjectsOfType<SaveableEntity>())
            {
                state[saveable.GetUniqueIdentifier()] = saveable.CaptureState();
            }


        }


        private void RestoreState(Dictionary<string, object> state)
        {

            foreach (SaveableEntity saveable in FindObjectsOfType<SaveableEntity>())
            {
                string id = saveable.GetUniqueIdentifier();
                if (state.ContainsKey(id))
                {
                    saveable.RestoreState(state[id]);
                }
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
