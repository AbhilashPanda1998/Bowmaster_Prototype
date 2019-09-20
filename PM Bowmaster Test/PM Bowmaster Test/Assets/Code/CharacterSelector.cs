using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameData;

public class CharacterSelector : MonoBehaviour
{
    #region Variables
    [SerializeField]
    private CharacterData[] m_CharacterDatas;
    private Vector3 m_PlayerSpawnPosition = new Vector3(0, -2.42f, 0);
    #endregion

    #region ClassFunctions
    public void LoadCharacter(int selectedCharacter)
    {
        this.gameObject.SetActive(false);
        CharacterData characterSelected = m_CharacterDatas[selectedCharacter];
        GameObject player = Instantiate(characterSelected._characterPrefab, m_PlayerSpawnPosition, Quaternion.identity) as GameObject;
        player.GetComponent<PlayerController>().WeaponPrefab = characterSelected._weapon;
        player.GetComponent<PlayerController>().CharacterName = m_CharacterDatas[selectedCharacter].name;
    }
    #endregion
}
