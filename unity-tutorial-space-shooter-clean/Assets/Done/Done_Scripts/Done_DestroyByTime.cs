using UnityEngine;
using UnityEngine.AddressableAssets;
using System.Collections;

public class Done_DestroyByTime : MonoBehaviour
{
	public float lifetime;

	void Start ()
	{
		Invoke("Release", lifetime);
		Destroy (gameObject, lifetime);
	}

	void Release()
	{
		Addressables.ReleaseInstance(gameObject);
	}
}
