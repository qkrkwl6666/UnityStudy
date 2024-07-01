using UnityEngine;
using System.Collections;
using UnityEngine.AddressableAssets;

public class Done_WeaponController : MonoBehaviour
{
	public AssetReference shot;
	//public GameObject shot;
	public Transform shotSpawn;
	public float fireRate;
	public float delay;

	void Start ()
	{
		InvokeRepeating ("Fire", delay, fireRate);
	}

	void Fire ()
	{
		shot.InstantiateAsync(shotSpawn.position, shotSpawn.rotation);
        //Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
        GetComponent<AudioSource>().Play();
	}
}
