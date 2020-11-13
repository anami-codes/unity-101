using UnityEngine;

public class TemporaryObject : MonoBehaviour
{
	//El TemporaryObject es un objeto que cumple una función única y se elimina al poco tiempo de crearse

    void Start()
    {
		GetComponent<AudioSource>().Play();
		Destroy( gameObject , 2f );
    }
}
