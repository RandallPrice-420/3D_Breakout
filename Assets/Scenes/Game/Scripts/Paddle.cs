using UnityEngine;


//public class Paddle : Singleton<Paddle>
public class Paddle : MonoBehaviour
{
    // -------------------------------------------------------------------------
    // Public Properties:
    // ------------------
    //   LeftWall
    //   RightWall
    //   PaddleWidth
    // -------------------------------------------------------------------------

    #region .  Public Properties  .

    public GameObject LeftWall;
    public GameObject RightWall;
    public float      PaddleWidth;

    #endregion


    // -------------------------------------------------------------------------
    // Private Properties:
    // -------------------
    //   _halfWidth
    //   _maxRight
    //   _minLeft
    //   _rigidbody
    // -------------------------------------------------------------------------

    #region .  Private Properties  .

    private float     _halfWidth;
    private float     _maxRight;
    private float     _minLeft;
    private Rigidbody _rigidbody;

    #endregion


    // -------------------------------------------------------------------------
    // Public Methods:
    // ---------------
    //   HidePaddle()
    //   ShowPaddle()
    // -------------------------------------------------------------------------

    #region .  HidePaddle()  .
    // -------------------------------------------------------------------------
    //   Method.......:  HidePaddle()
    //   Description..:  
    //   Parameters...:  None
    //   Returns......:  Nothing
    // -------------------------------------------------------------------------
    public void HidePaddle()
    {
        this.gameObject.SetActive(false);

    }   // HidePaddle()
    #endregion


    #region .  ShowPaddle()  .
    // -------------------------------------------------------------------------
    //   Method.......:  ShowPaddle()
    //   Description..:  
    //   Parameters...:  None
    //   Returns......:  Nothing
    // -------------------------------------------------------------------------
    public void ShowPaddle()
    {
        this.gameObject.SetActive(true);

    }   // ShowPaddle()
    #endregion


    // -------------------------------------------------------------------------
    // Private Methods:
    // ----------------
    //   Awake()
    //   FixedUpdate()
    //   SetPaddleWidth()
    //   Start()
    //   Update()
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
        this._rigidbody = this.GetComponent<Rigidbody>();

        this._halfWidth = this.transform.localScale.x / 2.0f;
        this._minLeft   = this.LeftWall .transform.position.x + this._halfWidth;
        this._maxRight  = this.RightWall.transform.position.x - this._halfWidth;

    }   // Awake()
    #endregion


    #region .  FixedUpdate()  .
    // -------------------------------------------------------------------------
    //   Method.......:  FixedUpdate()
    //   Description..:  This method hndles the paddle movement.  It does not
    //                   let the paddle go past the left and right walls.
    //   Parameters...:  None
    //   Returns......:  Nothing
    // -------------------------------------------------------------------------
    private void FixedUpdate()
    {
        Vector3 newPosition = new Vector3(Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, 0f, 0f)).x, this.transform.position.y, 0f);

        newPosition.x = (newPosition.x < this._minLeft ) ? this._minLeft  :
                        (newPosition.x > this._maxRight) ? this._maxRight :
                            newPosition.x;

        this._rigidbody.MovePosition(newPosition);

    }   // FixedUpdate()
    #endregion


    #region .  SetPaddleWidth()  .
    // -------------------------------------------------------------------------
    //   Method.......:  SetPaddleWidth()
    //   Description..:  
    //   Parameters...:  None
    //   Returns......:  Nothing
    // -------------------------------------------------------------------------
    private void SetPaddleWidth()
    {
        Utils.Instance.ScaleObjectX(this.gameObject, this.PaddleWidth);

    }   // SetPaddleWidth()
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
        this.SetPaddleWidth();

    }   // Start()
    #endregion


    #region .  Update()  .
    // -------------------------------------------------------------------------
    //   Method.......:  Update()
    //   Description..:  If paddle width property changed, changed paddle width.
    //   Parameters...:  None
    //   Returns......:  Nothing
    // -------------------------------------------------------------------------
    private void Update()
    {
        if (this.transform.localScale.x != this.PaddleWidth)
        {
            Utils.Instance.ScaleObjectX(this.gameObject, this.PaddleWidth);
        }

    }   // Update()
    #endregion


}   // class Paddle
