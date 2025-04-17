using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

using UnityEngine;


public class GameManager : Singleton<GameManager>
{
    // -------------------------------------------------------------------------
    // Public Enumerations:
    // --------------------
    //   State
    // -------------------------------------------------------------------------

    #region .  Public Enumerations  .

    public enum State
    {
        MENU,
        INIT,
        PLAY,
        LEVELCOMPLETED,
        LOADLEVEL,
        GAMEOVER
    }

    #endregion


    // -------------------------------------------------------------------------
    // Public Properties:
    // ------------------
    //   AvailableBalls
    //   AvailableBalls
    //   IsBallLaunched
    //   IsDebugOn
    //   IsGameStarted
    //   RandomPop
    //   BallPrefab
    //   Paddle
    //   Levels
    //   PanelGameOver
    //   PanelLevelCompleted
    //   PanelMenu
    //   PanelQuit
    //   PanelStats
    //   BallCount
    //   HighScore
    //   Level
    //   Score
    //   Speed
    // -------------------------------------------------------------------------

    #region .  Public Properties  .

    public int          AvailableBalls = 3;
    public int          BallSpeed      = 250;
    public bool         IsBallLaunched = false;
    public bool         IsDebugOn      = true;
    public bool         IsGameStarted  = false;
    public bool         RandomPop      = false;
    public int          TestLevel      = 3;
    public Ball         BallPrefab;
    public Paddle       paddle;
    public GameObject[] Levels;
    public GameObject   PanelGameOver;
    public GameObject   PanelLevelCompleted;
    public GameObject   PanelMenu;
    public GameObject   PanelQuit;
    public GameObject   PanelStats;

    public int BallCount
    {
        get { return _ballCount; }
        set
        {
            _ballCount = value;
            EventManager.RaiseOnBallCountChanged();
        }
    }

    public int HighScore
    {
        get { return _speed; }
        set
        {
            _speed = value;
            EventManager.RaiseOnHighScoreChanged();
        }
    }

    public int Level
    {
        get { return _level; }
        set
        {
            _level = value;
            EventManager.RaiseOnLevelCountChanged();
        }
    }

    public int Score
    {
        get { return _score; }
        set
        {
            _score = value;
            EventManager.RaiseOnScoreChanged();
        }
    }

    public int Speed
    {
        get { return _speed; }
        set
        {
            _speed = value;
            EventManager.RaiseOnSpeedChanged();
        }
    }

    #endregion


    // -------------------------------------------------------------------------
    // Private Properties:
    // -------------------
    //   _ball
    //   _balls
    //   _ballRigidbody
    //   _currentLevel
    //   _ballCount
    //   _ballPaddleOffset
    //   _highScore
    //   _isSwitchingState
    //   _level
    //   _score
    //   _speed
    //   _state
    // -------------------------------------------------------------------------

    #region .  Private Properties  .

    private Ball       _ball;
    private List<Ball> _balls;
    private Rigidbody  _ballRigidbody;

    private GameObject _currentLevel;

    private int        _ballCount;
    private int        _highScore;
    private bool       _isSwitchingState;
    private int        _level;
    private int        _score;
    private int        _speed;
    private State      _state;

    #endregion


    // -------------------------------------------------------------------------
    // Public Methods:
    // ---------------
    //   BeginState()
    //   ClearBalls()
    //   EndState()
    //   NextLevelClicked()
    //   PlayClicked()
    //   QuitClicked()
    //   ResetBalls()
    //   ResetStats()
    //   SpawnBalls()
    //   SwitchState()
    // -------------------------------------------------------------------------

    #region .  BeginState()  .
    // -------------------------------------------------------------------------
    //   Method.......:  BeginState()
    //   Description..:  
    //   Parameters...:  None
    //   Returns......:  Nothing
    // -------------------------------------------------------------------------
    public void BeginState(State newState)
    {
        switch (newState)
        {
            case State.MENU:
                //Cursor.visible = true;

                this.PanelMenu.SetActive(true);
                break;

            case State.INIT:
                //Cursor.visible = false;

                this.BallCount = this.AvailableBalls;
                this.Level     = (this.TestLevel != 0) ? TestLevel : 1;
                this.Score     = 0;
                this.Speed     = this.BallSpeed;
                this.HighScore = PlayerPrefs.GetInt("HighScore");

                this.PanelStats.SetActive(true);

                // Maybe don't need this??
                //if (this._currentLevel != null)
                //{
                //    Destroy(this._currentLevel);
                //}

                this.SwitchState(State.LOADLEVEL);
                break;

            case State.LOADLEVEL:
                this._currentLevel = BricksManager.Instance.LoadLevel(this.Level - 1);
                this.SwitchState(State.PLAY);
                break;

            case State.PLAY:
                this.IsBallLaunched = false;
                this.paddle.ShowPaddle();
                this.InitializeBall();
                break;

            case State.LEVELCOMPLETED:
                this.paddle.HidePaddle();
                this.ClearBalls();

                this.PanelLevelCompleted.SetActive(true);
                SoundManager.Instance.PlaySound(SoundManager.Sounds.Victory);

                Destroy(this._currentLevel);

                this.Level++;
                if (this.Level >= this.Levels.Length)
                {
                    this.SwitchState(State.GAMEOVER, 2f);
                }
                //else
                //{
                //    this.PanelLevelCompleted.SetActive(true);
                //    SoundManager.Instance.PlaySound(SoundManager.Sounds.Victory);
                //}
                break;

            case State.GAMEOVER:
                this.PanelGameOver.SetActive(true);
                SoundManager.Instance.PlaySound(SoundManager.Sounds.Game_Over_3);

                if (Score > this.HighScore)
                {
                    PlayerPrefs.SetInt("HighScore", this.Score);
                }
                break;
        }

    }   // BeginState()
    #endregion


    #region .  ClearBalls()  .
    // -------------------------------------------------------------------------
    //  Method.......:  ClearBalls()
    //  Description..:  
    //  Parameters...:  None
    //  Returns......:  Nothing
    // --------------------------------------------------------------------------
    public void ClearBalls()
    {
        foreach (Ball ball in this._balls)
        {
            if (ball != null)
            {
                Destroy(ball.gameObject);
            }
        }

    }   // ClearBalls()
    #endregion


    #region .  EndState()  .
    // -------------------------------------------------------------------------
    //   Method.......:  EndState()
    //   Description..:  
    //   Parameters...:  None
    //   Returns......:  Nothing
    // -------------------------------------------------------------------------
    public void EndState()
    {
        switch (this._state)
        {
            case State.MENU:
                this.PanelMenu.SetActive(false);
                break;

            case State.INIT:
                break;

            case State.PLAY:
                break;

            case State.LEVELCOMPLETED:
                this.PanelLevelCompleted.SetActive(false);
                break;

            case State.LOADLEVEL:
                break;

            case State.GAMEOVER:
                this.PanelGameOver.SetActive(false);
                this.PanelStats.SetActive(false);
                break;
        }

    }   // EndState()
    #endregion


    #region .  GetHighScores()  .
    // -------------------------------------------------------------------------
    //  Method.......:  GetHighScores()
    //  Description..:  
    //  Parameters...:  None
    //  Returns......:  Nothing
    // --------------------------------------------------------------------------
    public void GetHighScores()
    {
        List<HighScoreEntry> ScoresList = XMLManager.Instance.LoadScores();

    }   // GetHighScores()
    #endregion


    #region .  SaveHighScore()  .
    // -------------------------------------------------------------------------
    //  Method.......:  SaveHighScore()
    //  Description..:  
    //  Parameters...:  None
    //  Returns......:  Nothing
    // --------------------------------------------------------------------------
    public void SaveHighScore()
    {
        List<HighScoreEntry> HighScoresList = new List<HighScoreEntry>();

        for (int i = 1; i <= 5; i++)
        {
            HighScoreEntry highScoreEntry = new HighScoreEntry();
            highScoreEntry.Name  = $"Randall_{i}";
            highScoreEntry.Score = i * 1000;

            HighScoresList.Add(highScoreEntry);
        }

        XMLManager.Instance.SaveScores(HighScoresList);

    }   // SaveHighScore()
    #endregion


    #region .  NextLevelClicked()  .
    // -------------------------------------------------------------------------
    //   Method.......:  NextLevelClicked()
    //   Description..:  
    //   Parameters...:  newState
    //   Returns......:  Nothing
    // -------------------------------------------------------------------------
    public void NextLevelClicked()
    {
        this.SwitchState(State.LOADLEVEL);

    }   // NextLevelClicked()
    #endregion


    #region .  PlayClicked()  .
    // -------------------------------------------------------------------------
    //   Method.......:  PlayClicked()
    //   Description..:  
    //   Parameters...:  newState
    //   Returns......:  Nothing
    // -------------------------------------------------------------------------
    public void PlayClicked()
    {
        this.SwitchState(State.INIT);

    }   // PlayClicked()
    #endregion


    #region .  QuitGameClicked()  .
    // -------------------------------------------------------------------------
    //   Method.......:  QuitGameClicked()
    //   Description..:  
    //   Parameters...:  newState
    //   Returns......:  Nothing
    // -------------------------------------------------------------------------
    public void QuitGameClicked()
    {
        if (this.IsDebugOn) Debug.Log("Application Quit");

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif

        //
        // Don't need this anymore since the code above works fom the Editor.
        //
        //this.PanelGameOver.SetActive(false);
        //this.PanelQuit.SetActive(true);
        //Application.Quit();

    }   // QuitGameClicked()
    #endregion


    #region .  ResetBalls()  .
    // -------------------------------------------------------------------------
    //  Method.......:  ResetBalls()
    //  Description..:  
    //  Parameters...:  None
    //  Returns......:  Nothing
    // --------------------------------------------------------------------------
    public void ResetBalls()
    {
        this.ClearBalls();
        this.InitializeBall();

    }   // ResetBalls()
    #endregion


    #region .  ResetStats()  .
    // -------------------------------------------------------------------------
    //  Method.......:  ResetStats()
    //  Description..:  
    //  Parameters...:  None
    //  Returns......:  Nothing
    // --------------------------------------------------------------------------
    public void ResetStats()
    {
        this.Level     = 0;
        this.BallCount = 0;
        this.Score     = 0;

        BricksManager.Instance.BrickCount       = 0;
        BricksManager.Instance.InitialBrickCount = 0;

        this.InitializeBall();

    }   // ResetStats()
    #endregion


    #region .  SpawnBalls()  .
    // -------------------------------------------------------------------------
    //  Method.......:  SpawnBalls()
    //  Description..:  
    //  Parameters...:  None
    //  Returns......:  Nothing
    // --------------------------------------------------------------------------
    public void SpawnBalls(Vector3 position, int count, bool isLightningBall)
    {
        for (int i = 0; i < count; i++)
        {
            Ball spawnedBall = Instantiate(this.BallPrefab, position, Quaternion.identity) as Ball;

            Rigidbody spawnedBallRigidbody   = spawnedBall.GetComponent<Rigidbody>();
            spawnedBallRigidbody.isKinematic = false;
            spawnedBallRigidbody.AddForce(new Vector2(0, this.BallSpeed));

            //if (isLightningBall)
            //{
            //    spawnedBall.StartLightningBall();
            //}

            this._balls.Add(spawnedBall);
        }

    }   // SpawnBalls()
    #endregion


    #region .  SwitchState()  .
    // -------------------------------------------------------------------------
    //   Method.......:  SwitchState()
    //   Description..:  
    //   Parameters...:  State
    //                   float
    //   Returns......:  Nothing
    // -------------------------------------------------------------------------
    public void SwitchState(State newState, float delay = 0f)
    {
        StartCoroutine(SwitchDelay(newState, delay));

    }   // SwitchState()
    #endregion


    // -------------------------------------------------------------------------
    // Private Methods:
    // ----------------
    //   BallTrackPaddle()
    //   InitializeBall()
    //   OnBallCollision()
    //   OnBallDeath()
    //   OnBrickDestroyed()
    //   OnDisable()
    //   OnEnable()
    //   OnLevelCompleted()
    //   PlayDestroyBrickSound()
    //   Start()
    //   SwitchDelay()
    //   Update()
    // -------------------------------------------------------------------------

    #region .  BallTrackPaddle()  .
    // -------------------------------------------------------------------------
    //  Method.......:  BallTrackPaddle()
    //  Description..:
    //  Parameters...:  None
    //  Returns......:  Nothing
    // --------------------------------------------------------------------------
    private void BallTrackPaddle()
    {
        float ballPositionY = paddle.transform.position.y + paddle.transform.localScale.y + (this._ball.transform.localScale.y / 2.0f);
        this._ball.transform.position = new Vector3(this.paddle.transform.position.x, ballPositionY, 0f);

    }   // BallTrackPaddle()
    #endregion


    #region .  InitializeBall()  .
    // -------------------------------------------------------------------------
    //  Method.......:  InitializeBall()
    //  Description..:
    //  Parameters...:  None
    //  Returns......:  Nothing
    // --------------------------------------------------------------------------
    private void InitializeBall()
    {
        this._ball = Instantiate(this.BallPrefab, Vector3.zero, Quaternion.identity);
        this._ballRigidbody = _ball.GetComponent<Rigidbody>();
        //this._initialBall.IsLightningBall = false;

        this.BallTrackPaddle();

        this._balls = new List<Ball> { this._ball };

    }   // InitializeBall()
    #endregion


    #region .  OnBallCollision()  .
    // -------------------------------------------------------------------------
    //   Method.......:  OnBallCollision()
    //   Description..:  
    //   Parameters...:  None
    //   Returns......:  Nothing
    // -------------------------------------------------------------------------
    private void OnBallCollision(Collision collision)
    {
        // Play the appropriate collision sound effect.
        switch (collision.gameObject.tag)
        {
            case "Brick":
                StartCoroutine(PlayDestroyBrickSound());
                break;

            case "Paddle":
                SoundManager.Instance.PlaySound(SoundManager.Sounds.Ball_Hit_Paddle);
                break;

            case "BottomWall":
                SoundManager.Instance.PlaySound(SoundManager.Sounds.Ball_Hit_Wall_Bottom);
                break;

            case "LeftWall":
                SoundManager.Instance.PlaySound(SoundManager.Sounds.Ball_Hit_Wall_Left);
                break;

            case "RightWall":
                SoundManager.Instance.PlaySound(SoundManager.Sounds.Ball_Hit_Wall_Right);
                break;

            case "TopWall":
                SoundManager.Instance.PlaySound(SoundManager.Sounds.Ball_Hit_Wall_Top);
                break;

            case "DeathWall":
                SoundManager.Instance.PlaySound(SoundManager.Sounds.Ball_Hit_Wall_Top);
                break;
        }

    }   // OnBallCollision()
    #endregion


    #region .  OnBallDeath()  .
    // -------------------------------------------------------------------------
    //   Method.......:  OnBallDeath()
    //   Description..:  
    //   Parameters...:  None
    //   Returns......:  Nothing
    // -------------------------------------------------------------------------
    private void OnBallDeath(Ball ball)
    {
        Destroy(ball);
        GameManager.Instance.BallCount--;

    }   // OnBallDeath()
    #endregion


    #region .  OnBrickDestroyed()  .
    // -------------------------------------------------------------------------
    //   Method.......:  OnBrickDestroyed()
    //   Description..:  
    //   Parameters...:  None
    //   Returns......:  Nothing
    // -------------------------------------------------------------------------
    private void OnBrickDestroyed(Brick brick)
    {
        this.Score += brick.Points;

    }   // OnBrickDestroyed()
    #endregion


    #region .  OnDisable()  .
    // -------------------------------------------------------------------------
    //   Method.......:  OnDisable()
    //   Description..:  
    //   Parameters...:  None
    //   Returns......:  Nothing
    // -------------------------------------------------------------------------
    private void OnDisable()
    {
        //Cursor.visible = true;

        Brick        .OnBrickDestroyed -= this.OnBrickDestroyed;
        BricksManager.OnLevelCompleted -= this.OnLevelCompleted;
        Ball         .OnBallCollision  -= this.OnBallCollision;
        Ball         .OnBallDeath      -= this.OnBallDeath;

    }   // OnDisable()

    #endregion


    #region .  OnEnable()  .
    // -------------------------------------------------------------------------
    //   Method.......:  OnEnable()
    //   Description..:  
    //   Parameters...:  None
    //   Returns......:  Nothing
    // -------------------------------------------------------------------------
    private void OnEnable()
    {
        //Cursor.visible = false;

        Brick        .OnBrickDestroyed += this.OnBrickDestroyed;
        BricksManager.OnLevelCompleted += this.OnLevelCompleted;
        Ball         .OnBallCollision  += this.OnBallCollision;
        Ball         .OnBallDeath      += this.OnBallDeath;

    }   // OnEnable()
    #endregion


    #region .  OnLevelCompleted()  .
    // -------------------------------------------------------------------------
    //   Method.......:  OnLevelCompleted()
    //   Description..:  
    //   Parameters...:  None
    //   Returns......:  Nothing
    // -------------------------------------------------------------------------
    private void OnLevelCompleted()
    {
        SwitchState(State.LEVELCOMPLETED);

    }   // OnLevelCompleted()
    #endregion


    #region .  PlayDestroyBrickSound()  .
    // -------------------------------------------------------------------------
    //   Method.......:  PlayDestroyBrickSound()
    //   Description..:  
    //   Parameters...:  None
    //   Returns......:  string -- the name of the random Pop sound.
    // -------------------------------------------------------------------------
    private IEnumerator PlayDestroyBrickSound()
    {
        string name = (this.RandomPop) ? $"Pop_{UnityEngine.Random.Range(1, 8)}" : SoundManager.Sounds.Brick_Crack_1.ToString();
        SoundManager.Instance.PlaySound(name);

        yield return null;

    }   // PlayDestroyBrickSound()
    #endregion


    #region .  Start()  .
    // -------------------------------------------------------------------------
    //   Method.......:  Start()
    //   Description..:  
    //   Parameters...:  None
    //   Returns......:  Nothing
    // -------------------------------------------------------------------------
    private void Start()
    {
        this.SaveHighScore();
        this.GetHighScores();

        this._state = State.MENU;
        this.SwitchState(_state);

    }   // Start()
    #endregion


    #region .  SwitchDelay()  .
    // -------------------------------------------------------------------------
    //   Method.......:  SwitchDelay()
    //   Description..:  
    //   Parameters...:  newState
    //   Returns......:  Nothing
    // -------------------------------------------------------------------------
    private IEnumerator SwitchDelay(State newState, float delay)
    {
        this._isSwitchingState = true;
        yield return new WaitForSeconds(delay);

        this.EndState();
        this._state = newState;

        this.BeginState(newState);
        this._isSwitchingState = false;

    }   // SwitchDelay()
    #endregion


    #region .  Update()  .
    // -------------------------------------------------------------------------
    //   Method.......:  Update()
    //   Description..:  
    //   Parameters...:  None
    //   Returns......:  Nothing
    // -------------------------------------------------------------------------
    private void Update()
    {
        switch (this._state)
        {
            //case State.MENU:
            //    break;

            //case State.INIT:
            //    break;

            case State.PLAY:
                // ----------
                if (!this.IsBallLaunched)
                {
                    // Start the ball on mouse click.
                    if (Input.GetMouseButtonDown(0))
                    {
                        //this._initialBallRigidbody.isKinematic = false;
                        //this._initialBallRigidbody.AddForce(new Vector2(0, this.InitialBallSpeed));

                        Ball.Instance.Launch();
                        this.IsBallLaunched = true;
                    }

                    // Align Ball position to center of Paddle.
                    this.BallTrackPaddle();
                }

                if (this.BallCount <= 0)
                {
                    SwitchState(State.GAMEOVER);
                }

                if (this._currentLevel != null && Utils.Instance.CountAllChildComponents(this._currentLevel) == 0 && !this._isSwitchingState)
                {
                    SwitchState(State.LEVELCOMPLETED);
                }
                break;

            //case State.LEVELCOMPLETED:
            //    break;

            //case State.LOADLEVEL:
            //    break;

            //case State.GAMEOVER:
            //    break;
        }

    }   // Update()
    #endregion


}   // class GameManager
