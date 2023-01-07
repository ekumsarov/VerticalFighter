using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using SimpleJSON;

public class PlayerDataReader : MonoBehaviour
{
    private int _shipType;
    public int ShipType => _shipType;

    private int _speed;
    public int Speed => _speed;

    private int _shield;
    public int Shield => _shield;

    private int _damage;
    public int Damage => _damage;

    private int _fuel;
    public int Fuel => _fuel;

    private int _recharge;
    public int Recharge => _recharge;

    private int _bestDistance = 0;
    public int BestDistance => _bestDistance;

    private int _coins = 0;
    public int Coins => _coins;

    private JSONNode _mainNode;

    private void Awake()
    {
        string pathCrossString = File.ReadAllText(Application.dataPath + "/Resources/Data/PlayerData.json");
        _mainNode = JSON.Parse(pathCrossString);

        _shipType = _mainNode["SelectedShip"].AsInt;

        _speed = _mainNode["Ships"][_shipType]["Speed"].AsInt;
        _shield = _mainNode["Ships"][_shipType]["Shield"].AsInt;
        _damage = _mainNode["Ships"][_shipType]["Damage"].AsInt;
        _fuel = _mainNode["Ships"][_shipType]["Fuel"].AsInt;
        _recharge = _mainNode["Ships"][_shipType]["Recharge"].AsInt;
        _bestDistance = _mainNode["BestDistance"].AsInt;
        _coins = _mainNode["Coins"].AsInt;
    }

    public void SaveData(int bestDistance, int Coins)
    {
        if(bestDistance > _bestDistance)
        {
            _bestDistance = bestDistance;
        }

        _coins += Coins;

        _mainNode["BestDistance"].AsInt = _bestDistance;
        _mainNode["Coins"].AsInt = _coins;

        FileSystemWatcher temp = new FileSystemWatcher(Application.dataPath + "/Resources/Data/");

        temp.NotifyFilter = NotifyFilters.Attributes
                             | NotifyFilters.CreationTime
                             | NotifyFilters.DirectoryName
                             | NotifyFilters.FileName
                             | NotifyFilters.LastAccess
                             | NotifyFilters.LastWrite
                             | NotifyFilters.Security
                             | NotifyFilters.Size;

        temp.Filter = "*.json";
        temp.IncludeSubdirectories = true;
        temp.EnableRaisingEvents = true;

        File.WriteAllText(Application.dataPath + "/Resources/Data/PlayerData.json", _mainNode.ToString());
    }
}
