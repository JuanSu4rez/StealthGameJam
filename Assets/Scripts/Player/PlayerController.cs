using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class PlayerController : MonoBehaviour, IPlayerController{
    public LayerMask ObstaclesLayerMask;
    //Ray cast origin
    public GameObject origin = null;
    public float movementSpeed = 4;
    public float movementCrouchSpeed = 1;
    private Rigidbody rb;
    private PlayerStates _playerState;
    private PlayerStates _playerStateBefore;
    public PlayerStates PlayerState { get => _playerState; }
    private bool _isOnCrouchPosition;
    public bool IsOnCrouchPosition { get => _isOnCrouchPosition; }
    private int _indexCapsuleCollider = 0;
    private CapsuleCollider[] capsuleColliders;
    private Animator animator;
    private HealthBehaviour healthBehaviour;
    private AudioSource audioSource;
    void Start() {
        _playerState = PlayerStates._none;
        rb = gameObject.GetComponent<Rigidbody>();
        animator = gameObject.GetComponent<Animator>();
        healthBehaviour = gameObject.GetComponent<HealthBehaviour>();
        capsuleColliders = this.GetComponents<CapsuleCollider>();
        audioSource = this.GetComponent<AudioSource>();
        capsuleColliders[0].enabled = true;
        if(capsuleColliders.Length > 1) {
            capsuleColliders[1].enabled = false;
        }
    }
    // Update is called once per frame
    void Update() {
        MovePlayer();
    }
    void MovePlayer() {
        _playerState = PlayerStates.idle;

        /// shift button validation
        _isOnCrouchPosition = Input.GetKey(KeyCode.LeftShift);
        if(_isOnCrouchPosition && Input.GetKeyUp(KeyCode.LeftShift)) {
            _isOnCrouchPosition = false;
        }
        ///

        /// colliders management depending on _isOnCrouchPosition
        if(capsuleColliders.Length > 1) {
            _indexCapsuleCollider = _isOnCrouchPosition ? 0 : 1;
            capsuleColliders[_indexCapsuleCollider].enabled = true;
            capsuleColliders[( _indexCapsuleCollider + 1 ) % 2].enabled = false;
        }
        ///

        ///Movement
        var speed = _isOnCrouchPosition ? movementCrouchSpeed : movementSpeed;
        //Debug.Log($"IsOnCrouchPosition {_isOnCrouchPosition}");
        var isMoving = Input.GetButton("Vertical") || Input.GetButton("Horizontal");
        if(isMoving) {
            _playerState = PlayerStates.walking;
            float verticalInput = Input.GetAxis("Vertical");
            float horizontalInput = Input.GetAxis("Horizontal");
            var vector2 = new Vector2(-horizontalInput, verticalInput);
            var angle = Vector2.SignedAngle(Vector2.up, vector2);
            //Debug.Log(angle + " " + vector2);
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.up);
            var movement = ( transform.forward * verticalInput ) +
                           ( transform.right * -horizontalInput );
            transform.Translate(movement * Time.deltaTime * speed);
        }
        ///

        var isAlive = IsAlive();
        if( isAlive ) {
            if( isMoving ) {
                _playerState = PlayerStates.walking;
                if( _isOnCrouchPosition ) {
                    _playerState = PlayerStates.walkingCrouch;
                    Debug.Log("STOP"); ;
                    audioSource.Stop();
                }
                else {
                    if(!audioSource.isPlaying) {
                        Debug.Log("PLAYING");
                        audioSource.Play();
                    }
                }
            }
            else {
                _playerState = PlayerStates.idle;
                Debug.Log("STOP");
                audioSource.Stop();
                if( _isOnCrouchPosition )
                    _playerState = PlayerStates.idleCrouch;
            }
        }
        else {
            _playerState = PlayerStates.dying;
        }

        animator.SetInteger("playerState", (int)_playerState);
        if(_playerStateBefore != PlayerStates._none &&
            ( _playerState == PlayerStates.dying && _playerStateBefore != PlayerStates.dying )) { 
            animator.SetTrigger("hasDead");
        }
        animator.SetBool("isCrouching", _isOnCrouchPosition);
        _playerStateBefore = _playerState;
    }

    CapsuleCollider GetActiveCollider() {
        return capsuleColliders.FirstOrDefault(p => p.enabled);
    }

    private bool IsAlive() {
        bool? result = this.healthBehaviour?.IsAlive;
        return result == null || result.Value;
    }

    public bool CollideWithWall(GameObject reference) {
        var _origin = origin.transform.position;
        var gopos = reference.transform.position;
        var distance = reference.transform.position - origin.transform.position;
        Debug.DrawLine(gopos, _origin, Color.green);
        RaycastHit[] hits = Physics.RaycastAll(_origin, ( distance ).normalized, distance.magnitude, ObstaclesLayerMask);
        if(hits.Length > 0) {
            var values = hits.Select(p => p.collider.gameObject.name);
            Debug.Log("Object hit " + hits.Length + string.Join(";", values));
            return true;
        }
        return false;
    }
}
