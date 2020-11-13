using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
	public Text counterText;
	public Text timerText;

	public GameObject itemContainer;
	public EndLevelScreen endLevel;

	public float timeLimit = 1; //En minutos

	// Instancia de LevelManager
	public static LevelManager instance;

	private void Awake()
	{
		instance = this;
	}

	void Start()
    {
		bgm = GetComponent<AudioSource>();
		coins = itemContainer.GetComponentsInChildren<Coin>();
		StartGame();
    }

	public void StartGame()
	{
		for ( int i = 0 ; i < coins.Length ; i++ )
		{
			coins[i].gameObject.SetActive( true );
		}

		coinCounter = coins.Length;
		timeCounter += Mathf.CeilToInt( timeLimit * 60 );

		UpdateCounter( false );
		UpdateTimer();

		PlayerController.instance.RestartPlayer();
		bgm.Play();
		inGame = true;
	}

    void Update()
    {
		if ( inGame && timer <= Time.time )
		{
			timeCounter -= 1;
			timer = Time.time + 1;
			UpdateTimer();
		}
    }

	public void UpdateCounter( bool coinCollected)
	{
		if( coinCollected ) coinCounter--;

		if ( coinCounter == 1 )
		{
			counterText.text = "Falta " + coinCounter + " moneda";
		}
		else
		{
			counterText.text = "Faltan " + coinCounter + " monedas";
		}

		// Victoria
		if ( coinCounter <= 0 )
		{
			EndGame( true );
		}
	}

	private void UpdateTimer()
	{
		// Convertir timer a minutos y segundos
		int mins = Mathf.FloorToInt( timeCounter / 60 );
		int secs = timeCounter - ( mins * 60 );

		// Traducir tiempo en float a un texto legible
		string timeToString = ( mins < 10 ) ? "0" + mins.ToString() : mins.ToString();
		timeToString += ( secs < 10 ) ? ":0" + secs.ToString() : ":" + secs.ToString();

		timerText.text = timeToString;

		// Derrota
		if ( timeCounter <= 0  && coinCounter > 0)
		{
			EndGame( false );
		}
	}

	private void EndGame( bool victory )
	{
		bgm.Pause();
		inGame = false;
		endLevel.CallEndLevel( victory );
	}

	private Coin[] coins;

	private int coinCounter = 0;
	private int timeCounter = 0;
	private float timer;
	private bool inGame = false;

	private AudioSource bgm;
}
