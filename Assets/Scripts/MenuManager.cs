using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
	public AudioSource audioSource;

	//SFXs
	public AudioClip select;
	public AudioClip cancel;

	public void ButtonPlay()
	{
		Debug.Log( "Start Game" );
		audioSource.clip = select;
		audioSource.Play();
		SceneManager.LoadScene( 1 );
	}

	public void ButtonQuit()
	{
		Debug.Log( "Quit Game" );
		audioSource.clip = cancel;
		audioSource.Play();
		Application.Quit();
	}
}
