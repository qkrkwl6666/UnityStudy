using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Random = UnityEngine.Random;
using UnityEngine.SceneManagement;
using UnityEngine.AddressableAssets;
using System.Collections.Generic;
using UnityEngine.ResourceManagement.ResourceLocations;
using UnityEngine.ResourceManagement.AsyncOperations;

public class Done_GameController : MonoBehaviour
{
	//public AssetReference player;

	public AssetLabelReference hazardLabel;
	private List<IResourceLocation> hazardLocations;

	public Vector3 spawnValues;
	public int hazardCount;
	public float spawnWait;
	public float startWait;
	public float waveWait;

    public Text scoreText;
    public Text restartText;
    public Text gameOverText;
    public Text loadingText;

    private bool gameOver;
	private bool restart;
	private int score;

	private void LoadHazards()
	{
		Addressables.LoadResourceLocationsAsync(hazardLabel.labelString).Completed += OnHazardLoaded;
	}

	private void OnHazardLoaded(AsyncOperationHandle<IList<IResourceLocation>> op)
	{
		if(op.Status == AsyncOperationStatus.Failed)
		{
			Debug.LogError("Hazard Load Fail");
			return;
		}
		else
		{
			hazardLocations = new List<IResourceLocation>(op.Result);

        }
	}
	
	void Start ()
	{
        LoadHazards();
		Addressables.InstantiateAsync("Done_Player");
        gameOver = false;
		restart = false;
		//restartText.text = "";
		//gameOverText.text = "";
		score = 0;
		UpdateScore ();
		StartCoroutine (SpawnWaves ());

    }

	public string nextSceneAddress = "Done_Main";
	
	void Update ()
	{
		//Debug.Log(restart);
		if (restart)
		{
			if (Input.GetKeyDown(KeyCode.R))
			{
				Addressables.LoadSceneAsync(nextSceneAddress);
				//SceneManager.LoadScene("Done_Main");
			}
		}
	}
	
	IEnumerator SpawnWaves()
	{
		yield return new WaitForSeconds (startWait);
		while (true)
		{
			for (int i = 0; i < hazardCount; i++)
			{
				var hazardAddress = hazardLocations[Random.Range(0, hazardLocations.Count)];
                Vector3 spawnPosition = new Vector3 (Random.Range (-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
				Quaternion spawnRotation = Quaternion.identity;
				Addressables.InstantiateAsync(hazardAddress, spawnPosition, spawnRotation);
				yield return new WaitForSeconds (spawnWait);
			}
			yield return new WaitForSeconds (waveWait);
			
			if (gameOver)
			{
				//restartText.text = "Press 'R' for Restart";
				restart = true;
				break;
			}
		}
	}
	
	public void AddScore (int newScoreValue)
	{
		score += newScoreValue;
		UpdateScore ();
	}
	
	void UpdateScore ()
	{
		//scoreText.text = "Score: " + score;
	}
	
	public void GameOver ()
	{
		//gameOverText.text = "Game Over!";
		gameOver = true;
	}
}