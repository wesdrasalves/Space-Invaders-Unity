using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public float velocidade;
	public GameObject tiro; 
	private float ultimoTiro;
	public static bool PlayerDestruido= false; 
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Movimentar ();
	}

	void OnGUI()
	{

	}

	void OnCollisionEnter2D(Collision2D collisor)
	{

		if (collisor.gameObject.tag == "TiroInimigo" || collisor.gameObject.tag == "Inimigo") 
		{
			if(GameManager.VidaPlayer == 0 || collisor.gameObject.tag == "Inimigo")
			{
				Destroy(gameObject);
				GameManager.GameOver();
				//Application.LoadLevel("GameOver");
			}
			else
			{
				if(GameManager.VidaPlayer==2)
					Destroy(GameObject.Find("Vida2"));
				if(GameManager.VidaPlayer==1)
					Destroy(GameObject.Find("Vida1"));

				GeradorInimigo.DesativarDescer  = true;
				GeradorInimigo.DesativarTiro = true;
				GameManager.VidaPlayer -=1;
				PlayerDestruido = true;
				GetComponent<Animator>().SetBool("PlayerAcertado",true);
				Invoke("PlayerAtingido",1f);
			}
		}
	}

	void PlayerAtingido()
	{
		GetComponent<Animator>().SetBool("PlayerAcertado",false);
		GeradorInimigo.DesativarDescer  = false;
		GeradorInimigo.DesativarTiro = false;
		Player.PlayerDestruido = false;
	}
	
	void Movimentar()
	{
		if (!PlayerDestruido) 
		{
			if (Input.GetAxisRaw ("Horizontal") > 0) {
					transform.Translate (Vector2.right * velocidade * Time.deltaTime);
			}
			if (Input.GetAxisRaw ("Horizontal") < 0) {
					transform.Translate (-Vector2.right * velocidade * Time.deltaTime);

			}
			if (Input.GetKey (KeyCode.Space) && (ultimoTiro < Time.time || GameObject.FindWithTag ("TiroPlayer") == null)) {
					Vector3 _posicao = new Vector3 (this.transform.position.x, this.transform.position.y + 6f);
					Instantiate (tiro, _posicao, tiro.transform.rotation);
					ultimoTiro = Time.time + 0.8f; 
			}
		}
	}
}
