using UnityEngine;
using System.Collections;

public class PlayAgainLvl2 : MonoBehaviour {

	// respond on collisions
	void OnCollisionEnter(Collision newCollision)
	{
		// only do stuff if hit by a projectile
		if (newCollision.gameObject.tag == "Projectile") {
			// call the RestartGame function in the game manager
			GameManagerLvl2.gm.RestartGame();
		}
	}
}
