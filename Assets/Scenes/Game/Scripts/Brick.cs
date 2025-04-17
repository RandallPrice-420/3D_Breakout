using System;
using System.Collections;
using UnityEngine;


public class Brick : Singleton<Brick>
{
    // -------------------------------------------------------------------------
    // Public Delegates and Static Events:
    // -----------------------------------
    //   OnBrickDestroyed
    // -------------------------------------------------------------------------

    #region .  Public Events  .

    public static event Action<Brick> OnBrickDestroyed = delegate { };

    #endregion


    // -------------------------------------------------------------------------
    // Public Properties:
    // ------------------
    //   Hits
    //   Points
    //   Rotator
    //   BrickHitMaterial
    //   DestroyEffect
    // -------------------------------------------------------------------------

    #region .  Public Properties  .

    public int            Hits   = 1;
    public int            Points = 100;
    public Vector3        Rotator;
    public Material       BrickHitMaterial;
    public ParticleSystem DestroyEffect;

    //// Sliders in the Inspector with a range of 0 to 360 for the brick rotation.
    //[Range(0, 360)] public int RotateX;
    //[Range(0, 360)] public int RotateY;
    //[Range(0, 360)] public int RotateZ;

    #endregion


    // -------------------------------------------------------------------------
    // Private Properties:
    // -------------------
    //   _originalMaterial
    //   _renderer
    // -------------------------------------------------------------------------

    #region .  Private Properties  .

    private Material _originalMaterial;
    private Renderer _renderer;

    #endregion


    // -------------------------------------------------------------------------
    // Private Methods:
    // ----------------
    //   Awake()
    //   DestroyBrick()
    //   OnCollisionEnter()
    //   SetBrickMaterial()
    //   SpawnDestroyEffect()
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
        this.transform.Rotate((this.transform.position.x + this.transform.position.y) * 0.1f * this.Rotator);

        this._renderer         = GetComponent<MeshRenderer>();
        this._originalMaterial = this._renderer.material;

        //this.DestroyEffect     = this.GetComponent<ParticleSystem>();

    }   // Awake()
    #endregion


    #region .  DestroyBrick()  .
    // -------------------------------------------------------------------------
    //   Method.......:  DestroyBrick()
    //   Description..:  
    //   Parameters...:  None
    //   Returns......:  Nothing
    // -------------------------------------------------------------------------
    private void DestroyBrick()
    {
        Destroy(this.gameObject);
        this.SpawnDestroyEffect(this.gameObject);

        // Raise the event.
        OnBrickDestroyed.Invoke(this);

    }   // DestroyBrick()
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
        //if (this._isDebugOn) Debug.Log($"Brick.OnCollisionEnter() - collision: {collision.gameObject.name}");

        if (collision.gameObject.CompareTag("Ball"))
        {
            StartCoroutine(SetBrickMaterial(0.0f, this.BrickHitMaterial));

            this.Hits--;

            if (this.Hits <= 0)
            {
                StartCoroutine(SetBrickMaterial(0.05f, this._originalMaterial));
                this.Invoke(nameof(DestroyBrick), 0.05f);
            }
        }

    }   // OnCollisionEnter()
    #endregion


    #region .  SetBrickMaterial()  .
    // -------------------------------------------------------------------------
    //   Method.......:  SetBrickMaterial()
    //   Description..:  
    //   Parameters...:  None
    //   Returns......:  Nothing
    // -------------------------------------------------------------------------
    private IEnumerator SetBrickMaterial(float delay, Material material)
    {
        yield return new WaitForSeconds(delay);

        this._renderer.sharedMaterial = material;

    }   // SetBrickMaterial()
    #endregion


    #region .  SpawnDestroyEffect()  .
    // -------------------------------------------------------------------------
    //  Method.......:  SpawnDestroyEffect()
    //  Description..:  
    //  Parameters...:  None
    //  Returns......:  Nothing
    // -------------------------------------------------------------------------
    private void SpawnDestroyEffect(GameObject brick)
    {
        Vector3 brickPosition = gameObject.transform.position;
        Vector3 spawnPosition = new Vector3(brickPosition.x, brickPosition.y, brickPosition.z - 0.2f);

        // Instantiate the ParticleSystem at the spawn position and get the ParticleSystem component.
        GameObject     destroyEffect  = Instantiate(this.DestroyEffect.gameObject, spawnPosition, Quaternion.identity);
        ParticleSystem particleSystem = destroyEffect.GetComponent<ParticleSystem>();

        // Set the ParticleSystem color and trail color to the Brick color.
        particleSystem.GetComponent<ParticleSystemRenderer>().material      = this._originalMaterial;
        particleSystem.GetComponent<ParticleSystemRenderer>().trailMaterial = this._originalMaterial;

        Destroy(destroyEffect, DestroyEffect.main.startLifetime.constant);

    }   // SpawnDestroyEffect()
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
        //if (this._isDebugOn) Debug.Log($"{RotateX}, {RotateY}, {RotateZ}");

        this.transform.Rotate(this.Rotator * Time.deltaTime);

    }   // Update()
    #endregion


}   // class Brick
