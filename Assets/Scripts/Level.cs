using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VecrticalFighter.Model;
using VecrticalFighter;

public class Level : MonoBehaviour
{
    [SerializeField] private Camera _camera;

    [Header("Player")]
    [SerializeField] private PlayerHUD _playerHud;
    private PlayerShipPresenter _playerPresenter;
    [SerializeField] private PlayerShipPresenter _playerPrefab;
    [SerializeField] private PlayerInputHandler _playerInputHandler;
    [SerializeField] private Vector2 _startPlayerShipPoint;
    [SerializeField] private PlayerDataReader _dataReader;
    private PlayerInputRouter _playerInputRouter;
    private PlayerShip _playerShip;
    private PlayerFuel _playerFuel;

    [Header("Presenters")]
    [SerializeField] private PresentersFactory _factory;

    [Header("Data")]
    [SerializeField] private List<PlaneData> _planeDatas;
    [SerializeField] private List<LevelData> _levelData;

    [Header("Options")]
    private CameraViewportHandler _cameraHandler;
    [SerializeField] private SpriteRenderer _boardpart;
    [SerializeField] private SpriteRenderer _boardpartRight;
    [SerializeField] private GameObject _endPanel;

    private bool _pause = false;

    private void Awake()
    {
        _cameraHandler = _camera.GetComponent<CameraViewportHandler>();

        _factory.CreateBoard();

        InitLevel(_levelData[0]);

    }

    private void Start()
    {
        Config.InitData(this, _cameraHandler, _boardpart, _boardpartRight);
        Config.InitPlayerData(_dataReader);

        _playerFuel = new PlayerFuel();
        _playerFuel.Init(_dataReader.Fuel);

        PlayerShip ship = new PlayerShip(_dataReader, _startPlayerShipPoint);
        ship.Destroying += PlayerDead;
        _playerInputRouter = new PlayerInputRouter();
        _playerInputRouter.Init(ship, _playerInputHandler);
        _playerPresenter = Instantiate(_playerPrefab);
        _playerPresenter.Init(ship, _factory);
        _playerShip = ship;
        _playerHud.Init(this, ship, _playerFuel);

        _factory.Init(_playerShip);
    }

    private void Update()
    {
        if (_pause)
            return;

        float deltaTime = Time.deltaTime;
        //_playerInputRouter.Update(deltaTime);

        _currentMeterDecect += _tickSpeed * deltaTime;

        if(_currentMeterDecect >= _meterDetect)
        {
            _currentMeterDecect = 0f;
            _currentMeter += 1;
            _currentMeterEnemy += 1;
            _currentMeterObstacle += 1;
            _playerFuel.MeterDetect();
        }

        if(_currentMeterEnemy >= _meterEnemy)
        {
            _currentMeterEnemy = 0;
            if(Random.Range(0, 101) <= _chanceEnemy)
                _factory.CreateEnemy(_planeDatas[Random.Range(0, _enemyTypesToCall)]);
        }

        if(_currentMeterObstacle >= _meterObstacle)
        {
            _currentMeterObstacle = 0;
            if(Random.Range(0, 101) <= _chanceObstacle)
                _factory.CreateObstacle();
        }
    }

    public void TryAddBonus(Vector2 position)
    {
        if (Random.Range(0, 101) <= _chanceBonus)
            _factory.CreateBonus(position);
        else
            _factory.CreateCoin(position);
    }

    public void PlayerDead()
    {
        _pause = true;
        _factory.RemoveAllPresenters();
        _endPanel.gameObject.SetActive(true);
        _dataReader.SaveData(_currentMeter, Config.Coins);
    }

    public void ToMenu()
    {
        LoaderView.Instance.LoadScene("MainMenu");
    }

    public void Restart()
    {
        _playerFuel = new PlayerFuel();
        _playerFuel.Init(_dataReader.Fuel);

        PlayerShip ship = new PlayerShip(_dataReader, _startPlayerShipPoint);
        ship.Destroying += PlayerDead;
        _playerInputRouter = new PlayerInputRouter();
        _playerInputRouter.Init(ship, _playerInputHandler);
        _playerPresenter = Instantiate(_playerPrefab);
        _playerPresenter.Init(ship, _factory);
        _playerShip = ship;
        _playerHud.Init(this, ship, _playerFuel);

        _factory.Init(_playerShip);

        _currentMeterDecect = 0;
        _currentMeter = 0;
        _currentMeterEnemy = 0;
        _currentMeterObstacle = 0;
        InitLevel(_levelData[0]);

        _endPanel.gameObject.SetActive(false);
        _pause = false;
    }

    public void RecievedGlobalBonus(BonusType bonusType)
    {
        if(bonusType == BonusType.Fuel)
        {
            _playerFuel.RecivedFuel();
            return;
        }

        if(bonusType == BonusType.Coin)
        {
            Config.AddCoin();
            _playerHud.OnCoinChanged(Config.Coins);
            return;
        }

        if(bonusType == BonusType.Star)
        {
            _factory.StarBonusActivate();
        }
    }

    #region Level data work

    private float _tickSpeed;
    public float TickSpeed => _tickSpeed;

    private int _meterEnemy;
    private int _currentMeterEnemy;

    private int _meterObstacle;
    private int _currentMeterObstacle;

    private int _chanceEnemy;
    private int _chanceObstacle;
    private int _chanceBonus;

    private int _currentMeter = 0;

    private int _enemyTypesToCall;

    private int _nextLevel;

    private float _meterDetect = 2f;
    private float _currentMeterDecect = 0f;

    private void InitLevel(LevelData data)
    {
        _tickSpeed = data.TickSpeed;

        _meterEnemy = data.MeterEnemy;
        _currentMeterEnemy = 0;

        _meterObstacle = data.MeterObstacle;
        _currentMeterObstacle = 0;

        _currentMeter = 0;

        _chanceEnemy = data.ChanceEnemy;
        _chanceObstacle = data.ChanceObstacle;
        _chanceBonus = data.ChanceBonus;

        _enemyTypesToCall = data.EnemyTypes;

        _nextLevel = data.NextLevelMeter;
    }

    #endregion
}
