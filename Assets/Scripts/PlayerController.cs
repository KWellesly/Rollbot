using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {
	public float speed;
	private Rigidbody rb;
	private int count;
	public Text countText;
	private float ballSize = 0; 
	public Text winText;
	float LoseTimer = 0F;


	void Start ()
	{
		
		rb = GetComponent<Rigidbody> (); 
		//transform.position = new Vector3 (0, 0, 0);
		count = 0; 
		setCountText ();
		winText.text = "";
		 

	}
	public void RestartGame()
	{
		rb = GetComponent<Rigidbody> ();
		count = 0; 
		setCountText ();
		winText.text = ""; 
		transform.position = new Vector3 (0, 0, 0);
		rb.velocity = new Vector3 (0, 0, 0);
		transform.localScale = new Vector3 (2F, 2F, 2F);
		LoseTimer = 0F;

	}



	void Update()
	{
		
	}	
	/**
	void OnGUI() {
		if (winText.Equals("GG") || winText.Equals("You Win!!!")) {
			if (GUI.Button (new Rect(100, 100, 100, 100), "Play Again?"))
				SceneManager.LoadScene ("MiniGame");
		}
	}
**/

	void FixedUpdate ()
	{
		float moveHorizontal = Input.GetAxis ("Horizontal"); 
		float moveVertical = Input.GetAxis ("Vertical"); 

		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
		rb.AddForce (movement * speed);  

		if (transform.position.y < 0) {
			winText.text = "GG";
			if (LoseTimer == 0F) {
				LoseTimer = Time.realtimeSinceStartup;
			}
			if (Time.realtimeSinceStartup - LoseTimer > 3F) {
				RestartGame ();
			}
		
		}
		/**if (winText.text == "You Win!!!") {
			if (LoseTimer == 0F) {
				LoseTimer = Time.realtimeSinceStartup;
			}
			if (Time.realtimeSinceStartup - LoseTimer > 3F) {
				RestartGame ();
			}
		} **/		
	}

	void OnTriggerEnter(Collider other) 
	{
		if (other.gameObject.CompareTag ("Pick Up")) 
		{
			other.gameObject.SetActive (false); 
			count = count + 1;
			setCountText (); 

		}

		if (other.gameObject.CompareTag ("Bomb")) {
			setBomb ();

		}
		if (other.gameObject.CompareTag ("Speed")) {
			other.gameObject.SetActive (false);
			count = count + 1;
			setSpeed ();

		}
	}
	void setCountText()
	{
		countText.text = "Count: " + count.ToString ();
		ballSize = ballSize + 0;
		transform.localScale += new Vector3 (.3F, .3F, .3F);
		if (count >= 14) {
			winText.text = "You Win!!!";
			if (LoseTimer == 0F) {
				LoseTimer = Time.realtimeSinceStartup;
			}
			if (Time.realtimeSinceStartup - LoseTimer > 3F) {
				RestartGame ();
			}



		}
	}
			
	void setBomb()
	{
		float moveHorizontal = Input.GetAxis ("Horizontal"); 
		float moveVertical = Input.GetAxis ("Vertical");
		Vector3 upwards = new Vector3 (-1f, .7f, -1f);
		rb.AddForce (upwards *20, ForceMode.Impulse);

	}

	void setSpeed()
	{
		speed += 5;
	}
}
