using System;
using System.Collections;
using UnityEngine;


public class Ball : Singleton<Ball>
{
    // -------------------------------------------------------------------------
    // Public Events:
    // --------------
    //   OnBallCollision
    //   OnBallDeath
    // -------------------------------------------------------------------------

    #region .  Public Events  .

    public static event Action<Collision> OnBallCollision = delegate { };
    public static event Action<Ball>      OnBallDeath     = delegate { };

    #endregion


    // -------------------------------------------------------------------------
    // Public Properties:
    // ------------------
    //   CheckInterval
    //   FixVelocityX
    //   FixVelocityY
    //   Speed
    // -------------------------------------------------------------------------

    #region .  Public Properties  .

    public float  CheckInterval = 5.0f;
    public float  FixVelocityX  = 10.0f;
    public float  FixVelocityY  = 10.0f;
    public float  Speed         = 20f;

    #endregion


    // -------------------------------------------------------------------------
    // Private Properties:
    // -------------------
    //   _isChecking
    //   _isDebugOn
    //   _initialVelocity
    //   _paddle
    //   _renderer
    //   _rigidbody
    //   _velocity
    // -------------------------------------------------------------------------

    #region .  Private Properties  .

    private bool      _isChecking = false;
    private bool      _isDebugOn  = false;
    private Vector3   _initialVelocity;
    private Paddle    _paddle;
    private Renderer  _renderer;
    private Rigidbody _rigidbody;
    private Vector3   _velocity;

    #endregion


    // -------------------------------------------------------------------------
    // Public Methods:
    // ---------------
    //   Launch()
    // -------------------------------------------------------------------------

    #region .  Launch()  .
    // -------------------------------------------------------------------------
    //   Method.......:  Launch()
    //   Description..:  
    //   Parameters...:  None
    //   Returns......:  Nothing
    // -------------------------------------------------------------------------
    public void Launch()
    {
        this._rigidbody.velocity = Vector3.up * this.Speed;

    }   // Launch()
    #endregion


    // -------------------------------------------------------------------------
    // Private Methods:
    // ----------------
    //   Awake()
    //   CheckVelocityX()
    //   CheckVelocityY()
    //   FixedUpdate()
    //   OnCollisionEnter()
    // -------------------------------------------------------------------------

    #region .  Awake()  .
    // -------------------------------------------------------------------------
    //   Method.......:  Awake()
    //   Description..:  
    //   Parameters...:  None
    //   Returns......:  Nothing
    // -------------------------------------------------------------------------
    private void Awake()
    {
        this._isDebugOn = GameManager.Instance.IsDebugOn;
        this._renderer  = GetComponent<Renderer>();
        this._rigidbody = GetComponent<Rigidbody>();

    }   // Awake()
    #endregion


    #region .  CheckVelocityX()  .
    // -------------------------------------------------------------------------
    //   Method.......:  CheckVelocityX()
    //   Description..:  
    //   Parameters...:  None
    //   Returns......:  Nothing
    // -------------------------------------------------------------------------
    private IEnumerator CheckVelocityX()
    {
        while (true)
        {
            if (!_isChecking)
            {
                this._isChecking      = true;
                this._initialVelocity = this._rigidbody.velocity;
                yield return new WaitForSeconds(this.CheckInterval);

                if (this._rigidbody.velocity == this._initialVelocity)
                {
                    this._rigidbody.AddForce(new Vector3(this.FixVelocityX, 0f, 0f));
                    if (this._isDebugOn) Debug.Log($"X Velocity has not changed:  this._initialVelocity = {this._initialVelocity}, this._rigidbody.velocity:  {this._rigidbody.velocity}");
                }

                _isChecking = false;
            }
            yield return null;
        }

    }   // CheckVelocityX()
    #endregion


    #region .  CheckVelocityY()  .
    // -------------------------------------------------------------------------
    //   Method.......:  CheckVelocityY()
    //   Description..:  
    //   Parameters...:  None
    //   Returns......:  Nothing
    // -------------------------------------------------------------------------
    private IEnumerator CheckVelocityY()
    {
        while (true)
        {
            if (!_isChecking)
            {
                this._isChecking      = true;
                this._initialVelocity = this._rigidbody.velocity;
                yield return new WaitForSeconds(this.CheckInterval);

                if (this._rigidbody.velocity == this._initialVelocity)
                {
                    this._rigidbody.AddForce(new Vector3(0f, this.FixVelocityY, 0f));
                    if (this._isDebugOn) Debug.Log($"Y Velocity has not changed:  this._initialVelocity = {this._initialVelocity}, this._rigidbody.velocity:  {this._rigidbody.velocity}");
                }

                _isChecking = false;
            }
            yield return null;
        }

    }   // CheckVelocityY()
    #endregion


    #region .  FixedUpdate()  .
    // -------------------------------------------------------------------------
    //   Method.......:  FixedUpdate()
    //   Description..:  
    //   Parameters...:  None
    //   Returns......:  Nothing
    // -------------------------------------------------------------------------
    private void FixedUpdate()
    {
        if (GameManager.Instance.IsBallLaunched)
        {
            this._rigidbody.velocity = this._rigidbody.velocity.normalized * this.Speed;
            this._velocity           = this._rigidbody.velocity;

            //if (BricksManager.Instance.Bricks.Count > 0)
            //{
            //    this._rigidbody.velocity = this._rigidbody.velocity.normalized * this.Speed;
            //    this._velocity           = this._rigidbody.velocity;

            //    if (!this._renderer.isVisible)
            //    {
            //        GameManager.Instance.BallCount--;
            //        Destroy(gameObject);
            //    }
            //}
            //else
            //{
            //    this._rigidbody.velocity = Vector3.zero;
            //    this.transform.position  = Vector3.zero;
            //    this.transform.rotation  = Quaternion.identity;     // Optional if rotation is frozen in Inspector
            //}
        }

    }   // FixedUpdate()
    #endregion


    #region .  OnCollisionEnter()  .
    // -------------------------------------------------------------------------
    //   Method.......:  OnCollisionEnter()
    //   Description..:  
    //   Parameters...:  None
    //   Returns......:  Nothing
    // -------------------------------------------------------------------------
    private void OnCollisionEnter(Collision collision)
    {
        if (this._isDebugOn) Debug.Log($"Ball.OnCollisionEnter() - collision: {collision.gameObject.name}");

        switch (collision.gameObject.tag)
        {
            case "DeathWall":
                // Raise the event.
                OnBallDeath?.Invoke(this);
                break;

            case "Paddle":
                if (this._paddle == null)
                {
                    this._paddle = collision.gameObject.GetComponent<Paddle>();
                }

                //      Calcute the bounce angle base on where the ball hit the paddle.
                Vector3 hitPoint     = collision.contacts[0].point;
                Vector3 paddleCenter = new Vector3(this._paddle.transform.position.x, this._paddle.transform.position.y, 0);
                float   difference   = (paddleCenter.x - hitPoint.x) / this._paddle.transform.localScale.x;
                int     sign         = (hitPoint.x < paddleCenter.x ? -1 : 1);

                this._rigidbody.velocity = Vector3.zero;
                this._rigidbody.AddForce(new Vector2(sign * (Mathf.Abs(difference * 100)), this.Speed));

                // Raise the event.
                OnBallCollision?.Invoke(collision);
                break;

            default:
                this._rigidbody.velocity = Vector3.Reflect(this._velocity, collision.contacts[0].normal);

                // Check for ball "stuck" horizontally and/or vertically.
                StartCoroutine(CheckVelocityX());
                StartCoroutine(CheckVelocityY());

                // Raise the event.
                OnBallCollision?.Invoke(collision);
                break;
        }

        //if (collision.gameObject.tag == "DeathWall")
        //{
        //    // Raise the event.
        //    OnBallDeath?.Invoke(this);
        //}
        //else
        //{
        //    ////this._rigidbody.velocity = Vector3.Reflect(this._velocity, collision.contacts[0].normal);

        //    ////// Check for ball "stuck" horizontally and/or vertically.
        //    ////StartCoroutine(CheckVelocityX());
        //    ////StartCoroutine(CheckVelocityY());

        //    Paddle paddle = collision.gameObject.GetComponent<Paddle>();
        //    Vector3 hitPoint     = collision.contacts[0].point;
        //    Vector3 paddleCenter = new Vector3(paddle.transform.position.x, paddle.transform.position.y, 0);

        //    this._rigidbody.velocity = Vector3.zero;
        //    float difference = paddleCenter.x - hitPoint.x;
        //    int   sign       = (hitPoint.x < paddleCenter.x ? -1 : 1);

        //    this._rigidbody.AddForce(new Vector2(sign * (Mathf.Abs(difference * 200)), this.Speed));

        //    // Raise the event.
        //    OnBallCollision?.Invoke(collision);
        //}

    }   // OnCollisionEnter()
    #endregion


}   // class Ball
