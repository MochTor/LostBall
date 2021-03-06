using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class LostBallGameManager : MonoBehaviour {
	// make game manager public static so can access this from other scripts

	public static LostBallGameManager gm;

	[Tooltip("If not set, the player will default to the gameObject tagged as Player.")]

	// Public variables which are accessible from Unity GUI

	public GameObject player;
	public enum gameStates {Playing, GameOver};
	public gameStates gameState = gameStates.Playing;
	public GameObject mainCanvas;
	public Text mainTimerDisplay;
	public GameObject gameOverCanvas;
	public Text gameOverScreenText;
	public GameObject playAgainButtonGameObject;
	public string playAgainLevelToLoad;



	private float startTime;
	private float currentTime;
	private int timeToDisplay;
	private Health playerHealth;
	private Button playAgainButton;


	// setup the game
	void Start () {

		//subscribe to the onClick event of the button and call the function to load new level
		playAgainButton = playAgainButtonGameObject.GetComponent<Button>();
		playAgainButton.onClick.AddListener(newLevelToLoad);

		if (gm == null) 
			gm = gameObject.GetComponent<LostBallGameManager>();

		if (player == null) {
			player = GameObject.FindWithTag("Player");
		}

		playerHealth = player.GetComponent<Health>();

		// set the start time which will be used for calculation of finished time
		startTime = Time.time;

		// inactivate the gameOverScoreOutline gameObject, if it is set
		if (gameOverCanvas)
			gameOverCanvas.SetActive (false);

		// inactivate the playAgainButtons gameObject, if it is set
		if (playAgainButton)
			playAgainButtonGameObject.SetActive (false);

	
	}

	// this is the main game event loop
	void Update () {
		//calculating the time from the start
		currentTime = Time.time - startTime;
		// displaying only the int value
		timeToDisplay = Mathf.RoundToInt (currentTime);
		mainTimerDisplay.text = timeToDisplay.ToString ();
		switch (gameState)
		{
		case gameStates.Playing:
			if (playerHealth.isAlive == false)
			{
				// update gameState
				gameState = gameStates.GameOver;

				// set the end game score
				gameOverScreenText.text = currentTime.ToString();

				// switch which GUI is showing		
				mainCanvas.SetActive (false);
				gameOverCanvas.SetActive (true);
				playAgainButtonGameObject.SetActive (true);
			} 
			break;
	
		case gameStates.GameOver:
			
			break;
		}

	}

	public void newLevelToLoad(){
		
		Application.LoadLevel(playAgainLevelToLoad);
		gameState = gameStates.Playing;
	}

	

}
