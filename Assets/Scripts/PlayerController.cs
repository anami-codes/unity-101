using UnityEngine;

public class PlayerController : MonoBehaviour
{
	// Variables Públicas
	[Header( "Velocidades" )]
	public float walkingSpeed	= 1f;
	public float runningSpeed	= 1f;
	public float turningSpeed	= 1f;
	public float jumpSpeed		= 1f;

	// Instancia de Player
	public static PlayerController instance;

	// [MonoBehaviour] Awake() -> Cuando se inicializa el GameObject
	private void Awake()
	{
		instance = this;
		initPos = transform.position;
	}

	// [MonoBehaviour] Start() ->
	private void Start()
    {
		rb = GetComponent<Rigidbody>();
		//animator = GetComponentInChildren<Animator>();
    }

	// [MonoBehaviour] OnEnable() -> Cada vez que se activa el GameObject
	private void OnEnable() {}

	// [MonoBehaviour] Update() -> Cada frame (aprox 30 veces por segundo)
	private void Update()
    {
		// Revisar inputs
		isRunning = Input.GetKey( KeyCode.LeftShift );	// Boolean
		float h = Input.GetAxis( "Horizontal" );		// Float (-1.0 a 1.0)
		float v = Input.GetAxis( "Vertical" );          // Float (-1.0 a 1.0)

		// Transformar input en dirección
		rotationDir.y = h;
		movementDir.x = v;
		isWalking = ( v != 0 );

		// Rotar Personaje
		float step = turningSpeed * Time.deltaTime;
		transform.Rotate( rotationDir * turningSpeed );

		if ( Input.GetKeyDown( KeyCode.Space ) && isGrounded )
		{
			isJumping = true;
		}

		// UpdateAnimator();
    }

	// [MonoBehaviour] FixedUpdate() -> Cada segundo, usar para físicas
	private void FixedUpdate()
	{
		// Mover Personaje
		vel = ( isWalking ) ? transform.forward * movementDir.x : Vector3.zero;
		vel *= ( isRunning ) ? runningSpeed : walkingSpeed;
		vel.y = rb.velocity.y;

		if ( isJumping && isGrounded )
		{
			vel.y += jumpSpeed;
			isGrounded = false;
		}

		rb.velocity = vel;
	}

	// Actualizar variables del Animator
	private void UpdateAnimator()
	{
		animator.SetBool( "Walking" , isWalking );
		animator.SetBool( "Running" , isRunning );
		animator.SetBool( "Jump" , isJumping );
	}

	public void RestartPlayer()
	{
		transform.position = initPos;
		transform.eulerAngles = new Vector3();
	}

	private void OnTriggerEnter( Collider other )
	{
		Debug.Log( "Player - OnTriggerEnter" );

		if ( other.CompareTag( "Coin" ) )
		{
			other.GetComponent<Coin>().GetCoin();
		}
	}

	private void OnTriggerStay( Collider other )
	{
		Debug.Log( "Player - OnTriggerStay" );
	}

	private void OnTriggerExit( Collider other )
	{
		Debug.Log( "Player - OnTriggerExit" );
	}

	private void OnCollisionEnter( Collision collision )
	{
		Debug.Log( "Player - OnCollisionEnter" );

		Vector3 normal = collision.GetContact( 0 ).normal;

		if ( !isGrounded && normal.y >= 0.5f )
		{
			isJumping = false;
			isGrounded = true;
		}
	}

	private void OnCollisionStay( Collision collision )
	{
		Debug.Log( "Player - OnCollisionStay" );
	}

	private void OnCollisionExit( Collision collision )
	{
		Debug.Log( "Player - OnCollisionExit" );
	}

	// Variables Privadas
	private Rigidbody rb;
	private Animator animator;

	private Vector3 initPos;
	private Vector3 movementDir = Vector3.zero;
	private Vector3 rotationDir = Vector3.zero;
	private Vector3 vel = Vector3.zero;

	private bool isWalking = false;
	private bool isRunning = false;
	private bool isGrounded = true;
	private bool isJumping = false;

}
