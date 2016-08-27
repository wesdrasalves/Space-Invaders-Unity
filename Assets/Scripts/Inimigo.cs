


using UnityEngine;
using System.Collections;

public class Inimigo : MonoBehaviour {

	public int valorPonto;
	public float velocidade = 1000f;
	public bool InimigoBonus = false;
	public int coluna = 0;
	public int linha = 0;
	public int quantidadeColunas = GameManager.QtdColunasInimigos;
	public GameObject Tiro; 

	const float tamanhoTotal = 180;
	const float distanciaEntre = 15f;
	const float altura = 130f;

	private float posInicialLocal;
	private float posFinalLocal;
	private float tamanhoCaminho;
	private bool isEsquerda = true;
	private Animator animacao;
	private bool statusAnimacao = true;
	private int _colunaOrigem;
	private bool _pararInimigoBonus = false;

	public int ColunaOrigem 
	{
		get{ return _colunaOrigem;}
	}

	// Use this for initialization
	void Start () 
	{
		_pararInimigoBonus = false;
		if (!InimigoBonus) 
		{
			_colunaOrigem = coluna;
			RecalcularPosicoes ();
			transform.position = new Vector3 (posFinalLocal, altura - (linha * 12f));
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (!InimigoBonus)
				Movimentar ();
		else
				MovimentarBonus ();
	}

	void OnCollisionEnter2D(Collision2D collisor)
	{
		if (collisor.gameObject.tag == "TiroPlayer") 
		{
			if(!InimigoBonus)GetComponent<Animator>().SetBool("InimigoAcertado", true );
			if(InimigoBonus){
				GetComponent<Animator>().SetInteger("Ponto", valorPonto );
				_pararInimigoBonus = true;
			}
			GameManager.Score += valorPonto;
			var gerador = GameObject.FindWithTag("Gerador");
			if(!InimigoBonus) gerador.GetComponent<GeradorInimigo>().AtualizarMatriz(linha,coluna,_colunaOrigem);
			gameObject.GetComponent<BoxCollider2D>().enabled = false;
			if(!InimigoBonus)Destroy(gameObject,0.3f);
			else Destroy(gameObject,0.6f);
		}
		if (collisor.gameObject.tag == "Parede") 
		{
			//isEsquerda = !isEsquerda;
			//espacoMovimento = 0;
		}
	}

	public void RecalcularPosicoes()
	{
		posInicialLocal = (coluna * distanciaEntre)-(tamanhoTotal/2);
		posFinalLocal = ((coluna - (quantidadeColunas-1)) * distanciaEntre)+(tamanhoTotal/2);
		tamanhoCaminho = Mathf.Abs(posInicialLocal - posFinalLocal);		
	}

	void MovimentarBonus()
	{
		if (!_pararInimigoBonus) 
		{
			Vector2 movimento;

			if (this.transform.position.x < (distanciaEntre + (tamanhoTotal / 2) + 50)) {
					movimento = new Vector2 (Vector2.right.x * 100 / velocidade, 0);
					transform.Translate (movimento);
			} else {
					Destroy (gameObject);
			}
		}
	}

	void Movimentar()
	{
		Vector2 movimento;

		if((isEsquerda && posInicialLocal < transform.position.x) || (!isEsquerda && posFinalLocal > transform.position.x) )
		{
			//animacao.SetBool("run",statusAnimacao);
			statusAnimacao  = !statusAnimacao;
			//Debug.Log(coluna.ToString() + " - " + posInicialLocal.ToString() + " - " + posFinalLocal.ToString() + " - " + transform.position.x.ToString());
			if (!isEsquerda) 
			{
				movimento = new Vector2(Vector2.right.x * (tamanhoCaminho/velocidade),0);
				transform.Translate (movimento);
			}
			else 
			{
				movimento = new Vector2(-Vector2.right.x * (tamanhoCaminho/velocidade),0);
				transform.Translate (movimento);
			}

		} 
		else
		{
			isEsquerda = !isEsquerda;
			if(!GeradorInimigo.DesativarDescer) transform.position = new Vector3(transform.position.x,transform.position.y-3f);
		}

		if (transform.position.y <= 10f) 
		{
			GameManager.GameOver();
		}
	}

	public void Atirar()
	{
		if (!GeradorInimigo.DesativarTiro) 
		{
			Vector3 _posicao = new Vector3 (this.transform.position.x, this.transform.position.y - 8f);
			Instantiate (Tiro, _posicao, Tiro.transform.rotation);
		}
	}
}
