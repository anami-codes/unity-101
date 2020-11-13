using UnityEngine;

public class Coin : MonoBehaviour
{
	public GameObject pickupCoin;

	public void GetCoin()
	{
		LevelManager.instance.UpdateCounter( true );
		Instantiate( pickupCoin , transform.position , transform.rotation );
		gameObject.SetActive( false );
	}
}
