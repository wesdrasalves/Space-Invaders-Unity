using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public static int Score = 0;
	public static int QtdColunasInimigos = 12;
	public static int VidaPlayer = 2;
	public static bool gameOver = false;

	// Use this for initialization
	void Start () {
		Score = 0;
		VidaPlayer = 2;
		QtdColunasInimigos = 12;
		Player.PlayerDestruido = false;
		GeradorInimigo.DesativarDescer = false;
		GeradorInimigo.DesativarTiro = false;
	}

	void OnGUI()
	{
		//DontDestroyOnLoad (gameObject);
		
		GUIStyle myStyle = new GUIStyle(GUI.skin.textField);
		myStyle.alignment = TextAnchor.MiddleCenter;
		myStyle.fontStyle = FontStyle.Bold;
		myStyle.fontSize = (int)Mathf.Round(Screen.width * 0.02f);

		float _persentualTela = 0.3f;

		GUI.Label(new Rect(Screen.width*0.37f,Screen.height*0.02f,Screen.width*0.09f,Screen.height*0.09f),Score.ToString(),myStyle);

		if (Player.PlayerDestruido && VidaPlayer < 0)
		{
			string _msg;

			if(Dados.PegarMaiorPonto() < Score)
			{
				Dados.SalvarMaiorPonto(Score);
			}

			_msg = "GAME OVER\n\r\n\rPONTUAÇAO FINAL : " + Score.ToString() + "\n\rMAIOR PONTUAÇAO: " + Dados.PegarMaiorPonto().ToString() +  "\n\r TECLE ENTER PARA\n\r REINICIAR O JOGO.";

			GUI.TextField(new Rect(Screen.width/2 - ((Screen.width*_persentualTela)/2),Screen.height/2 -((Screen.height*_persentualTela)/2),Screen.width*(_persentualTela+0.01f),Screen.height*(_persentualTela+0.04f)),_msg,myStyle);

			if (Input.GetKey (KeyCode.Return))
			{
				Application.LoadLevel("GamePlay");
			}
		}
	}
	

	public static void GameOver()
	{
		GameManager.VidaPlayer = 0;
		GeradorInimigo.DesativarDescer  = true;
		GeradorInimigo.DesativarTiro = true;
		GameManager.VidaPlayer -=1;
		Player.PlayerDestruido = true;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Input.GetKey (KeyCode.Escape))
		{
			Application.Quit();
		}
	}
}
