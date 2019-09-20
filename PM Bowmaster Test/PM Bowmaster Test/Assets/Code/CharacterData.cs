using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameData
{
    [CreateAssetMenu(menuName = "CharacterData")]
    public class CharacterData : ScriptableObject
    {
        public string _characterName = "Default";
        public int _hp = 100;
        public GameObject _characterPrefab;
        public GameObject _weapon;

        public class CharacterDataLoader
        {
            private static List<CharacterData> m_CharactersList;
            private const string DATA_ASSETS_PATH = "ScriptableObjects/";

            public static void Init()
            {
                m_CharactersList = new List<CharacterData>();

                CharacterData[] loadedObjects = Resources.LoadAll<CharacterData>(DATA_ASSETS_PATH);
                foreach (Object obj in loadedObjects)
                    m_CharactersList.Add((CharacterData)obj);
            }

            public static CharacterData GetData(string characterName)
            {
                return m_CharactersList.Find(x => x._characterName == characterName);
            }
        }
    }
}
