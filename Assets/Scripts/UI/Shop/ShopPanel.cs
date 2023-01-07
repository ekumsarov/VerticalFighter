using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;
using System.IO;
using TMPro;
using UnityEngine.UI;
using System.Linq;

namespace VecrticalFighter.UI
{
    public class ShopPanel : MonoBehaviour
    {
        [SerializeField] private List<ShipData> _shipData;

        [SerializeField] private TextMeshProUGUI _coinsText;
        [SerializeField] private List<Image> _ships;

        [SerializeField] private CharacteristicPanel _speedPanel;
        [SerializeField] private CharacteristicPanel _shieldPanel;
        [SerializeField] private CharacteristicPanel _damagePanel;
        [SerializeField] private CharacteristicPanel _fuelPanel;
        [SerializeField] private CharacteristicPanel _rechargePanel;

        [SerializeField] private Image _closeButton;
        [SerializeField] private Button _buyButton;

        private int _currentShip = 0;
        private List<int> _openedShips;
        private int Coins = 0;

        private JSONNode _playerData;

        private void Awake()
        {
            if (File.Exists(Application.dataPath + "/Resources/Data/PlayerData.json") == false)
            {
                Directory.CreateDirectory(Application.dataPath + "/Resources/Data/");

                TextAsset asetCrossText = Resources.Load("ShipData/PlayerDataBase") as TextAsset;
                FileSystemWatcher temp = new FileSystemWatcher(Application.dataPath + "/Resources/Data/");
                temp.Created += CompleteSave;
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

                File.WriteAllText(Application.dataPath + "/Resources/Data/PlayerData.json", asetCrossText.text);
            }
        }

        private void Start()
        {
            string pathCrossString = File.ReadAllText(Application.dataPath + "/Resources/Data/PlayerData.json");
            _playerData = JSON.Parse(pathCrossString);
            SetupShop();
        }

        private void CompleteSave(object sender, FileSystemEventArgs e)
        {
            Start();
        }

        private void SetupShop()
        {
            _buyButton.gameObject.SetActive(false);
            _closeButton.gameObject.SetActive(true);
            _currentShip = _playerData["SelectedShip"].AsInt;
            Coins = _playerData["Coins"].AsInt;

            _coinsText.text = Coins.ToString();

            JSONArray openedShips = _playerData["OpenedShip"].AsArray;
            _openedShips = new List<int>();
            for(int i = 0; i < openedShips.Count; i++)
            {
                _openedShips.Add(openedShips[i].AsInt);
            }

            for(int i = 0; i < _ships.Count; i++)
            {
                if(i == _currentShip)
                    _ships[i].gameObject.SetActive(true);
                else
                    _ships[i].gameObject.SetActive(false);

            }

            _speedPanel.SetupPanel(_playerData["Ships"][_currentShip]["Speed"].AsInt, _shipData[_currentShip].Speed, costConstant * (_playerData["Ships"][_currentShip]["Speed"].AsInt + 1));
            _shieldPanel.SetupPanel(_playerData["Ships"][_currentShip]["Shield"].AsInt, _shipData[_currentShip].Shield, costConstant * (_playerData["Ships"][_currentShip]["Shield"].AsInt + 1));
            _damagePanel.SetupPanel(_playerData["Ships"][_currentShip]["Damage"].AsInt, _shipData[_currentShip].Damage, costConstant * (_playerData["Ships"][_currentShip]["Damage"].AsInt + 1));
            _fuelPanel.SetupPanel(_playerData["Ships"][_currentShip]["Fuel"].AsInt, _shipData[_currentShip].Fuel, costConstant * (_playerData["Ships"][_currentShip]["Fuel"].AsInt + 1));
            _rechargePanel.SetupPanel(_playerData["Ships"][_currentShip]["Recharge"].AsInt, _shipData[_currentShip].Recharge, costConstant * (_playerData["Ships"][_currentShip]["Recharge"].AsInt + 1));
        }

        public void BuyPoint(string pointName)
        {
            if (CanBuyPoint(pointName))
            {
                SaveData();
                SetupShop();
            }
        }

        public void BuyShip()
        {
            if(TryBuyShip())
            {
                SaveData();
                SetupShop();
            }
        }

        private bool TryBuyShip()
        {
            JSONArray costArray = _playerData["ShipOpenCost"].AsArray;
            for(int i = 0; i < 4; i++)
            {
                if(costArray[i]["Type"].AsInt == _currentShip)
                {
                    if(costArray[i]["Cost"].AsInt <= Coins)
                    {
                        Coins -= costArray[i]["Cost"].AsInt;
                        _playerData["Coins"] = Coins;
                        JSONArray temp = _playerData["OpenedShip"].AsArray;
                        temp.Add(_currentShip);
                        _playerData["OpenedShip"] = temp;

                        return true;
                    }
                }
            }


            return false;
        }

        public void NextShip(int next)
        {
            _currentShip += next;
            if (_currentShip < 0)
                _currentShip = 4;

            if (_currentShip > 4)
                _currentShip = 0;

            for (int i = 0; i < _ships.Count; i++)
            {
                if (i == _currentShip)
                    _ships[i].gameObject.SetActive(true);
                else
                    _ships[i].gameObject.SetActive(false);

            }

            if (_openedShips.Any(ship => ship == _currentShip))
            {
                _playerData["SelectedShip"] = _currentShip;
                SaveData();
                SetupShop();
                return;
            }

            SetupClosedShip();
        }

        private void SetupClosedShip()
        {
            _buyButton.gameObject.SetActive(true);
            _closeButton.gameObject.SetActive(false);

            Coins = _playerData["Coins"].AsInt;
            _coinsText.text = Coins.ToString();

            JSONArray openedShips = _playerData["OpenedShip"].AsArray;
            _openedShips = new List<int>();
            for (int i = 0; i < openedShips.Count; i++)
            {
                _openedShips.Add(openedShips[i].AsInt);
            }

            _speedPanel.SetupCloseShipPanel(_playerData["Ships"][_currentShip]["Speed"].AsInt, _shipData[_currentShip].Speed);
            _shieldPanel.SetupCloseShipPanel(_playerData["Ships"][_currentShip]["Shield"].AsInt, _shipData[_currentShip].Shield);
            _damagePanel.SetupCloseShipPanel(_playerData["Ships"][_currentShip]["Damage"].AsInt, _shipData[_currentShip].Damage);
            _fuelPanel.SetupCloseShipPanel(_playerData["Ships"][_currentShip]["Fuel"].AsInt, _shipData[_currentShip].Fuel);
            _rechargePanel.SetupCloseShipPanel(_playerData["Ships"][_currentShip]["Recharge"].AsInt, _shipData[_currentShip].Recharge);
        }

        private void SaveData()
        {
            FileSystemWatcher temp = new FileSystemWatcher(Application.dataPath + "/Resources/Data/");
            temp.Changed += CompleteSave;

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

            File.WriteAllText(Application.dataPath + "/Resources/Data/PlayerData.json", _playerData.ToString());
        }

        public void CloseShop()
        {
            SaveData();
        }

        private bool CanBuyPoint(string pointName)
        {
            if(costConstant * (_playerData["Ships"][_currentShip][pointName].AsInt + 1) <= Coins)
            {
                Coins -= costConstant * (_playerData["Ships"][_currentShip][pointName].AsInt + 1);
                _playerData["Coins"] = Coins;
                _playerData["Ships"][_currentShip][pointName] = _playerData["Ships"][_currentShip][pointName].AsInt + 1;

                return true;
            }

            return false;
        }

        private int costConstant = 100;
    }
}