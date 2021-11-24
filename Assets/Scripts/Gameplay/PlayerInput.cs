using UnityEngine;
using System.Collections;
using Gameplay.OldInput;

[RequireComponent (typeof (OldPlayer))]
public class PlayerInput : MonoBehaviour {

	OldPlayer _oldPlayer;
    
	void Start () {
		_oldPlayer = GetComponent<OldPlayer> ();
	}

	void Update () {
		Vector2 directionalInput = new Vector2 (Input.GetAxisRaw ("Horizontal"), Input.GetAxisRaw ("Vertical"));
		_oldPlayer.SetDirectionalInput (directionalInput);

		if (Input.GetKeyDown (KeyCode.Space)) {
			//_player.OnJumpInputDown ();
		}
		if (Input.GetKeyUp (KeyCode.Space)) {
			//_player.OnJumpInputUp ();
		}
	}
}
