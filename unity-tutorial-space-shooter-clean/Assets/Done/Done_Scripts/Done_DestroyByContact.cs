using UnityEngine;
using UnityEngine.AddressableAssets;
using System.Collections;

public class Done_DestroyByContact : MonoBehaviour
{
	public AssetReference explosionAsset;
	public AssetReference playerExplosionAsset;

	//public GameObject explosion;
	//public GameObject playerExplosion;
	public int scoreValue;
	private Done_GameController gameController;

	void Start ()
	{
		GameObject gameControllerObject = GameObject.FindGameObjectWithTag ("GameController");
		if (gameControllerObject != null)
		{
			gameController = gameControllerObject.GetComponent <Done_GameController>();
		}
		if (gameController == null)
		{
			Debug.Log ("Cannot find 'GameController' script");
		}
	}

	void OnTriggerEnter (Collider other)
	{
		if (other.tag == "Boundary" || other.tag == "Enemy")
		{
			return;
		}

		//if (explosionAsset.Asset != null)
		//{
			//Instantiate(explosion, transform.position, transform.rotation);
		explosionAsset.InstantiateAsync(transform.position, transform.rotation);
		//}

		gameController.AddScore(scoreValue);
		//Destroy (gameObject);
		Addressables.ReleaseInstance(gameObject);
		
		if (other.tag == "Player")
		{
			//Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
			playerExplosionAsset.InstantiateAsync(other.transform.position, other.transform.rotation);
			// return here to survive
			gameController.GameOver();
		}

        Destroy(other.gameObject);
        //Addressables.ReleaseInstance(other.gameObject);
    }
}