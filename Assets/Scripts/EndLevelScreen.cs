using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndLevelScreen : MonoBehaviour
{
	public Text resultText;
	public AudioSource audioSource;

	public void CallEndLevel( bool victory )
	{
		this.victory = victory;

		gameObject.SetActive( true );
		resultText.text = ( victory ) ? "GANASTE!" : "Perdiste...";
		GetComponentInChildren<Animator>().SetBool( "Victory", victory );
		GetComponentInChildren<Animator>().SetBool( "Defeat" , !victory );
	}

	public void ButtonRetry()
	{
		Debug.Log( "Retry" );
		audioSource.Play();
		victory = false;
		gameObject.SetActive( false );
		LevelManager.instance.StartGame();
	}

	public void ButtonMenu ()
	{
		Debug.Log( "Return to menu" );
		audioSource.Play();
		SceneManager.LoadScene( 0 );
	}

	public void ButtonQuit ()
	{
		Debug.Log( "Quit Game" );
		audioSource.Play();
		Application.Quit();
	}

	private bool victory;
}