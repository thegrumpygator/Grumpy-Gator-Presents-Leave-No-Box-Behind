using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class GameManagerLvl2 : MonoBehaviour {

	// make game manager public static so can access this from other scripts
	public static GameManagerLvl2 gm;
    public static int ammo = 0;

	// public variables
	public int score=0;

	public bool canBeatLevel = false;
	public int beatLevelScore=0;

	public float startTime=5.0f;
    public int startAmmo = 20; // BLF 20151011
	
	public Text mainScoreDisplay;
	public Text mainTimerDisplay;
    public Text mainAmmoDisplay;

	public GameObject gameOverScoreOutline;

	public AudioSource musicAudioSource;

	public bool gameIsOver = false;

	public GameObject playAgainButtons;
	public string playAgainLevelToLoad;

	public GameObject nextLevelButtons;
	public string nextLevelToLoad;

	private float currentTime;

	// setup the game
	void Start () {

		// set the current time to the startTime specified
		currentTime = startTime;

        // set the current ammo to the starting ammo
        ammo = startAmmo;

		// get a reference to the GameManager component for use by other scripts
		if (gm == null) 
			gm = this.gameObject.GetComponent<GameManagerLvl2>();

		// init scoreboard to 0
		mainScoreDisplay.text = "0";

		// inactivate the gameOverScoreOutline gameObject, if it is set
		if (gameOverScoreOutline)
			gameOverScoreOutline.SetActive (false);

		// inactivate the playAgainButtons gameObject, if it is set
		if (playAgainButtons)
			playAgainButtons.SetActive (false);

		// inactivate the nextLevelButtons gameObject, if it is set
		if (nextLevelButtons)
			nextLevelButtons.SetActive (false);
	}

	// this is the main game event loop
	void Update () {
		if (!gameIsOver) {
			if (canBeatLevel && (score >= beatLevelScore)) {  // check to see if beat game
				BeatLevel ();
			} else if (currentTime < 0 || ammo < 0) { // check to see if timer or ammo has run out
				EndGame ();
			} else { // game playing state, so update the timer
				currentTime -= Time.deltaTime;
                if (!gameIsOver)
                {
                    
                    mainAmmoDisplay.text = "Ammo: " + ammo.ToString() + " Rounds";
                }

                mainTimerDisplay.text = currentTime.ToString ("0.00");				
			}
		}
	}

	void EndGame() {
		// game is over
		gameIsOver = true;
        ammo = 10000000;

		// repurpose the timer to display a message to the player
		mainTimerDisplay.text = "GAME OVER";

		// activate the gameOverScoreOutline gameObject, if it is set 
		if (gameOverScoreOutline)
			gameOverScoreOutline.SetActive (true);
	
		// activate the playAgainButtons gameObject, if it is set 
		if (playAgainButtons)
			playAgainButtons.SetActive (true);

		// reduce the pitch of the background music, if it is set 
		if (musicAudioSource)
			musicAudioSource.pitch = 0.5f; // slow down the music
	}
	
	void BeatLevel() {
		// game is over
		gameIsOver = true;

		// repurpose the timer to display a message to the player
		mainTimerDisplay.text = "LEVEL COMPLETE";

		// activate the gameOverScoreOutline gameObject, if it is set 
		if (gameOverScoreOutline)
			gameOverScoreOutline.SetActive (true);

		// activate the nextLevelButtons gameObject, if it is set 
		if (nextLevelButtons)
			nextLevelButtons.SetActive (true);
		
		// reduce the pitch of the background music, if it is set 
		if (musicAudioSource)
			musicAudioSource.pitch = 0.75f; // slow down the music
	}

	// public function that can be called to update the score or time
	public void targetHit (int scoreAmount, float timeAmount, int ammoAmount)
	{
		// increase the score by the scoreAmount and update the text UI
		score += scoreAmount;
		mainScoreDisplay.text = score.ToString ();

        // increase the ammo by the ammo amount and update the text UI
        if(!gameIsOver)
        {
            ammo += ammoAmount;
            mainAmmoDisplay.text = "Ammo: " + ammo.ToString() + " Rounds";
        }

        // increase the time by the timeAmount
        currentTime += timeAmount;
		
		// don't let it go negative
		if (currentTime < 0)
			currentTime = 0.0f;

		// update the text UI
		mainTimerDisplay.text = currentTime.ToString ("0.00");
	}

    // public function to determine if the player still has ammo and to
    // update the ammo value
    public bool weaponFired()
    {
        if (ammo>0)
        {
            ammo--;
            if (!gameIsOver)
            {
                
                mainAmmoDisplay.text = "Ammo: " + ammo.ToString() + " Rounds";
            }

            return true;
        }
        return false;
    }

	// public function that can be called to restart the game
	public void RestartGame ()
	{
		// we are just loading a scene (or reloading this scene)
		// which is an easy way to restart the level
		Application.LoadLevel (playAgainLevelToLoad);
	}

	// public function that can be called to go to the next level of the game
	public void NextLevel ()
	{
		// we are just loading the specified next level (scene)
		Application.LoadLevel (nextLevelToLoad);
	}
	

}
