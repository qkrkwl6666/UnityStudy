using UnityEngine;
using UnityEngine.AddressableAssets;
using System.Collections;

public class Done_DestroyByBoundary : MonoBehaviour
{
	void OnTriggerExit (Collider other) 
	{
		Addressables.ReleaseInstance(other.gameObject);
		//Destroy(other.gameObject);
	}
}