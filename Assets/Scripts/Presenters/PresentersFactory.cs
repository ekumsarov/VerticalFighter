using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using VecrticalFighter.Model;
using VecrticalFighter;

public class PresentersFactory : MonoBehaviour
{
    [Header("Base")]
    [SerializeField] private Camera _camera;
    [SerializeField] private Level _level;
    private PlayerShip _ship;

    public void Init(PlayerShip ship)
    {
        _ship = ship;

        _pinSprite = _redPin.GetComponent<SpriteRenderer>();
        _eraserSprite = _eraser.GetComponent<SpriteRenderer>();

        _ruler1PinSprite = _ruler1.GetComponent<SpriteRenderer>();
        _rullerWidth = _ruler1PinSprite.bounds.size.x;
        _startRulerLeft = Config.LeftBoardX + _rullerWidth / 2;
        _startRulerRight = Config.RightBoardX - _rullerWidth / 2;

        _enemies = new List<Presenter>();
        _obstacles = new List<Presenter>();
        _bonuses = new List<Presenter>();
        _playerBullets = new List<Presenter>();
        _enemyBullets = new List<Presenter>();
    }

    private void Awake()
    {
        if (_boardPartPresenters == null || _boardPartPresenters.Count == 0)
            throw new System.Exception("No board part presenters");

        /*_playerBulletPresenters = new List<Presenter>();
        for(int i = 0; i < 30; i++)
        {
            _playerBulletPresenters.Add(Instantiate(_playerBulletPresenter));
        }*/
    }

    #region BoardParts 
    [Header("Board Part")]
    [SerializeField] private GameObject _boardLevel;
    [SerializeField] private Vector2 _boardStartPosition;
    [SerializeField] private List<BoardPresenter> _boardPartPresenters;

    public void CreateBoard()
    {
        for(int i = 0; i < 7; i++)
        {
            SceneObject boardPart = new BoardPart(new Vector2(_boardStartPosition.x, _boardStartPosition.y - 5.3f*i));
            boardPart.Destroying += CreateBoardPart;
            List<BoardPresenter> tempList = _boardPartPresenters.Where(part => part.gameObject.activeSelf == false).ToList();
            BoardPresenter temp = tempList[Random.Range(0, tempList.Count)];
            temp.Init(boardPart);
        }
    }

    public void CreateBoardPart()
    {
        SceneObject boardPart = new BoardPart(_boardStartPosition);
        boardPart.Destroying += CreateBoardPart;
        List<BoardPresenter> tempList = _boardPartPresenters.Where(part => part.gameObject.activeSelf == false).ToList();
        BoardPresenter temp = tempList[Random.Range(0, tempList.Count)];
        temp.transform.SetParent(_boardLevel.transform);
        temp.transform.SetAsFirstSibling();
        temp.Init(boardPart);
    }


    #endregion

    #region Enemies
    [Header("Enemy")]
    [SerializeField] private Presenter _presenterPlane1;
    [SerializeField] private Presenter _presenterPlane2;
    [SerializeField] private Presenter _presenterZinit;
    [SerializeField] private Presenter _blackTank;

    private List<Presenter> _enemies;

    public void CreateEnemy(PlaneData data)
    {
        if(data.EnemyType == VecrticalFighter.EnemyType.Plane1)
        {
            VecrticalFighter.Model.Plane temp = new VecrticalFighter.Model.Plane(data, _ship);
            PlanePresenter presenter = GameObject.Instantiate<Presenter>(_presenterPlane1) as PlanePresenter;
            presenter.Init(temp, _level, this);
            presenter.PresenterDestroyed += EnemyDestroy;
            _enemies.Add(presenter);
        }
        else if(data.EnemyType == VecrticalFighter.EnemyType.Plane2)
        {
            VecrticalFighter.Model.Plane temp = new VecrticalFighter.Model.Plane(data, _ship);
            PlanePresenter presenter = GameObject.Instantiate<Presenter>(_presenterPlane2) as PlanePresenter;
            presenter.Init(temp, _level, this);
            presenter.PresenterDestroyed += EnemyDestroy;
            _enemies.Add(presenter);
        }
        else if(data.EnemyType == VecrticalFighter.EnemyType.Zinit)
        {
            VecrticalFighter.Model.Plane temp = new VecrticalFighter.Model.Plane(data, _ship);
            TurretUnitPresenter presenter = GameObject.Instantiate<Presenter>(_presenterZinit) as TurretUnitPresenter;
            presenter.Init(temp.Turret);
            presenter.Init(temp, _level, this);
            presenter.PresenterDestroyed += EnemyDestroy;
            _enemies.Add(presenter);
        }
        else if (data.EnemyType == VecrticalFighter.EnemyType.BlackTank)
        {
            VecrticalFighter.Model.Plane temp = new VecrticalFighter.Model.Plane(data, _ship);
            TurretUnitPresenter presenter = GameObject.Instantiate<Presenter>(_blackTank) as TurretUnitPresenter;
            presenter.Init(temp.Turret);
            presenter.Init(temp, _level, this);
            presenter.PresenterDestroyed += EnemyDestroy;
            _enemies.Add(presenter);
        }

    }

    private void EnemyDestroy(Presenter presenter)
    {
        _enemies.Remove(presenter);
    }

    #endregion

    #region Obstacles

    [Header("Obstacles")]
    [SerializeField] private Presenter _redPin;
    [SerializeField] private Presenter _yellowPin;
    [SerializeField] private Presenter _eraser;
    [SerializeField] private Presenter _ruler1;
    [SerializeField] private Presenter _ruler2;
    private float _rullerWidth = 0f;
    private float _startRulerLeft = 0f;
    private float _startRulerRight = 0f;

    private SpriteRenderer _pinSprite;
    private SpriteRenderer _eraserSprite;
    private SpriteRenderer _ruler1PinSprite;

    private List<Presenter> _obstacles;

    public void CreateObstacle()
    {
        int randomResult = Random.Range(0, 100);
        if (randomResult <= 33)
        {
            if (Random.Range(0, 101) <= 50)
            {
                Obstacle temp = new Obstacle(VecrticalFighter.ObstacleType.Pin, new Vector2(Random.Range(Config.LeftBoardX + _pinSprite.bounds.size.x/2, Config.Width / 2), Config.Height / 2 + 0.3f));
                Presenter obstacle = CreatePresenter(_redPin, temp);
                obstacle.PresenterDestroyed += ObstacleDestouy;
                _obstacles.Add(obstacle);
            }
            else
            {
                Obstacle temp = new Obstacle(VecrticalFighter.ObstacleType.Pin, new Vector2(Random.Range(Config.LeftBoardX + _pinSprite.bounds.size.x / 2, Config.Width / 2), Config.Height / 2 + 0.3f));
                Presenter obstacle = CreatePresenter(_yellowPin, temp);
                obstacle.PresenterDestroyed += ObstacleDestouy;
                _obstacles.Add(obstacle);
            }
        }
        else if (randomResult > 33 && randomResult <= 66)
        {
            Obstacle temp = new Obstacle(VecrticalFighter.ObstacleType.Eraser, new Vector2(Random.Range(Config.LeftBoardX + _eraserSprite.bounds.size.x / 2, Config.Width / 2), Config.Height / 2 + 0.3f));
            Presenter obstacle = CreatePresenter(_eraser, temp);
            obstacle.PresenterDestroyed += ObstacleDestouy;
            _obstacles.Add(obstacle);
        }
        else
        {
            int howMuch = Random.Range(2, 6);

            float offset;
            Vector2 startPosition;

            if (Random.Range(0, 101) <= 50)
            {
                startPosition = new Vector2(_startRulerLeft, Config.Height/2 + 0.3f);
                offset = _rullerWidth;
            } 
            else
            {
                startPosition = new Vector2(_startRulerRight, Config.Height / 2 + 0.3f);
                offset = -_rullerWidth;
            }
                

            for(int i = 0; i < howMuch; i++)
            {
                if(i == 0)
                {
                    Obstacle temp = new Obstacle(VecrticalFighter.ObstacleType.Ruler, new Vector2(startPosition.x, Config.Height / 2 + 0.3f));
                    Presenter obstacle = CreatePresenter(_ruler1, temp);
                    obstacle.PresenterDestroyed += ObstacleDestouy;
                    _obstacles.Add(obstacle);
                }
                else
                {
                    Obstacle temp = new Obstacle(VecrticalFighter.ObstacleType.Ruler, new Vector2(startPosition.x + offset * i, Config.Height / 2 + 0.3f));
                    Presenter obstacle = CreatePresenter(_ruler2, temp);
                    obstacle.PresenterDestroyed += ObstacleDestouy;
                    _obstacles.Add(obstacle);
                }
            }
        }
    }

    public void ObstacleDestouy(Presenter presenter)
    {
        _obstacles.Remove(presenter);
    }

    #endregion

    #region bullets

    [Header("Bullet")]
    [SerializeField] private Presenter _playerBullet;
    [SerializeField] private Presenter _enemyBullet;
    [SerializeField] private Presenter _bulletBlow;

    private List<Presenter> _playerBullets;
    private List<Presenter> _enemyBullets;

    public void CreatePlayerBullet(float speed, Vector2 position)
    {
        Bullet temp = new Bullet(90f, speed, position);
        PlayerBulletPresenter presenter = Instantiate(_playerBullet) as PlayerBulletPresenter;
        presenter.Init(temp, this);
        presenter.PresenterDestroyed += PlayerBulletDestroy;
        _playerBullets.Add(presenter);
    }

    public void CreateEnemyBullet(float angel, float speed, Vector2 position)
    {
        Bullet temp = new Bullet(angel, speed, position);
        EnemyBulletPresenter presenter = Instantiate(_enemyBullet) as EnemyBulletPresenter;
        presenter.Init(temp, this);
        presenter.PresenterDestroyed += EnemyBulletDestroy;
        _enemyBullets.Add(presenter);
    }
    public void CreateFollowEnemyBullet(float speed, Vector2 position)
    {
        Bullet temp = new Bullet(_ship, speed, position);
        EnemyBulletPresenter presenter = Instantiate(_enemyBullet) as EnemyBulletPresenter;
        presenter.Init(temp, this);
        presenter.PresenterDestroyed += EnemyBulletDestroy;
        _enemyBullets.Add(presenter);
    }

    public void CreateBulletBlow(Vector2 position)
    {
        BulletBlowPresenter blow = Instantiate(_bulletBlow) as BulletBlowPresenter;
        blow.Init(position);
    }

    private void PlayerBulletDestroy(Presenter presenter)
    {
        _playerBullets.Remove(presenter);
    }

    private void EnemyBulletDestroy(Presenter presenter)
    {
        _enemyBullets.Remove(presenter);
    }

    #endregion

    #region bonus

    [Header("Bonus")]
    [SerializeField] private Presenter _gun1Presenter;
    [SerializeField] private Presenter _gun2Presenter;
    [SerializeField] private Presenter _gun3Presenter;
    [SerializeField] private Presenter _heartPresenter;
    [SerializeField] private Presenter _coinPresenter;
    [SerializeField] private Presenter _fuelPresenter;
    [SerializeField] private Presenter _starPresenter;

    private List<Presenter> _bonuses;

    public void CreateBonus(Vector2 position)
    {
        int chance = Random.Range(0, 70);

        if (chance < 17)
        {
            Bonus temp = new Bonus(position);
            CreateBonusPresenter(_fuelPresenter, temp);
        }
        else if (chance >= 18 && chance < 30)
        {
            Bonus temp = new Bonus(position);
            CreateBonusPresenter(_gun1Presenter, temp);
        }
        else if (chance >= 30 && chance < 40)
        {
            Bonus temp = new Bonus(position);
            CreateBonusPresenter(_gun2Presenter, temp);
        }
        else if (chance >= 40 && chance < 50)
        {
            Bonus temp = new Bonus(position);
            CreateBonusPresenter(_gun3Presenter, temp);
        }
        else if (chance >= 50 && chance < 53)
        {
            Bonus temp = new Bonus(position);
            CreateBonusPresenter(_heartPresenter, temp);
        }
        else
        {
            Bonus temp = new Bonus(position);
            CreateBonusPresenter(_starPresenter, temp);
        }
    }

    public void CreateCoin(Vector2 position)
    {
        Bonus temp = new Bonus(position);
        CreateBonusPresenter(_coinPresenter, temp);
    }

    private void CreateBonusPresenter(Presenter template, Bonus model)
    {
        BonusPresenter presenter = Instantiate(template) as BonusPresenter;
        presenter.Init(model, _ship, _level);
        presenter.PresenterDestroyed += BonusDestroy;
        _bonuses.Add(presenter);
    }

    private void BonusDestroy(Presenter presenter)
    {
        _bonuses.Remove(presenter);
    }

    #endregion

    #region Other
    [Header("Other")]

    [SerializeField] private Presenter _explodePresenter;

    public void CreateExplode(Vector2 position)
    {
        ExplodePresenter blow = Instantiate(_explodePresenter) as ExplodePresenter;
        blow.Init(position);
    }

    public void RemoveAllPresenters()
    {
        foreach (var presenter in _enemies)
        {
            presenter.DestroyByBonus();
        }
        _enemies.Clear();

        foreach (var presenter in _obstacles)
        {
            presenter.DestroyByBonus();
        }
        _obstacles.Clear();

        foreach (var presenter in _enemyBullets)
        {
            presenter.DestroyByBonus();
        }
        _enemyBullets.Clear();

        foreach (var presenter in _playerBullets)
        {
            presenter.DestroyByBonus();
        }
        _playerBullets.Clear();

        foreach (var presenter in _bonuses)
        {
            presenter.DestroyByBonus();
        }
        _bonuses.Clear();
    }

    public void StarBonusActivate()
    {
        foreach (var presenter in _enemies)
        {
            presenter.DestroyByBonus();
        }
        _enemies.Clear();

        foreach (var presenter in _obstacles)
        {
            presenter.DestroyByBonus();
        }
        _obstacles.Clear();

        foreach (var presenter in _enemyBullets)
        {
            presenter.DestroyByBonus();
        }
        _enemyBullets.Clear();
    }

    #endregion

    private Presenter CreatePresenter(Presenter template, SceneObject model)
    {
        Presenter presenter = Instantiate(template);
        presenter.Init(model);

        return presenter;
    }
}
