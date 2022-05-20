using UnityEngine;
using TMPro;
public class SetGameProperties : MonoBehaviour
{

    string _character;
    DBManager dbmanager;


 
    public void Start()
    {

        dbmanager = GetComponent<DBManager>();

    }

    public void SetDifficulty(int difficulty)
    {
        dbmanager.StartGetDifficulty(difficulty.ToString());
       
    }
    public void SetCharacter(int character)
    {
        switch (character)
        {
            case 0:
                _character = "Barbarian";
                break;
            case 1:
                _character = "Assassin";
                break;
            case 2:
                _character = "Ronin";
                break;
                
        }
        dbmanager.StartGetCharacter(_character);
    }
}