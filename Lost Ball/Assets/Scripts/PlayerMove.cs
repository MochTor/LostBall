using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;

public class PlayerMove : NetworkBehaviour
{
	private int count;
	public Text countText;
	public Text winText;
	public Camera cam;

	//Setting the color of local player as red as opposed to the other player which will be blue
	public override void OnStartLocalPlayer()
	{
		GetComponent<MeshRenderer>().material.color = Color.red;
		//Camera.main.GetComponent<CameraFollow>().target=transform;
		count = 0;
		setCountText ();
		winText.text = "";

	}

	void Update()
	{
		// checking is this command given by the local player
		if (!isLocalPlayer) {
			cam.enabled = false;
			//return;
		}
			

		var x = Input.GetAxis("Horizontal")*0.1f;
		var z = Input.GetAxis("Vertical")*0.1f;

		transform.Translate(x, 0, z);
	}

	void OnTriggerEnter(Collider other) 
	{
		if (other.gameObject.CompareTag ("Pick Up"))
		{
			other.gameObject.SetActive (false);
			count++;
			setCountText ();
		}
	}

	void setCountText() {
		countText.text = "Count: " + count.ToString ();
		if (count >= 15) {
			winText.text = "You win!";
		}
	}
}
