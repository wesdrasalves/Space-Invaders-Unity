using UnityEngine;
using System.Collections;

public class GeradorInimigo : MonoBehaviour {

	public GameObject[] listInimigos;
	public GameObject InimigoBonus;
	private GameObject[,] _matrizInimigos = new GameObject[5,GameManager.QtdColunasInimigos];
	private int[] _totalInimigosColuna = new int[GameManager.QtdColunasInimigos];
	private int _colunasDestruidas = 0; 
	private int _linhaRand = 0;
	private int _colunaRand = 0;
	public static bool DesativarTiro = false;
	public static bool DesativarDescer = false;
	private float _totalInimigos = GameManager.QtdColunasInimigos * 5;

	// Use this for initialization
	void Start () {
		GerarInimigos ();
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void LimparFase()
	{
		_colunasDestruidas = 0; 
		_linhaRand = 0;
		_colunaRand = 0;
		DesativarTiro = false;
		DesativarDescer = false;
		_totalInimigos = GameManager.QtdColunasInimigos * 5;

		for (int _i=0; _i<5; _i++) 
		{
			for(int _j=0;_j<GameManager.QtdColunasInimigos;_j++)
			{
				if(_matrizInimigos[_i,_j] != null) 
				{
					Destroy(_matrizInimigos[_i,_j]);
					_matrizInimigos[_i,_j] = null;
				}
			}
		}

		for(int _i = 0;_i<GameManager.QtdColunasInimigos;_i++)
			_totalInimigosColuna [_i] = 5;
	}

	public void GerarInimigos()
	{
		LimparFase ();

		CancelInvoke ();
		InvokeRepeating ("ChamarTiroInimigo", 3f,2f);
		InvokeRepeating ("GerarInimigoBonus", 20f,50f);
		
		for (int _i=0; _i<5; _i++) 
		{
			for(int _j=0;_j<GameManager.QtdColunasInimigos;_j++)
			{
				Vector3 _posicao = new Vector3(this.transform.position.x,this.transform.position.y+0.8f,10);
				_matrizInimigos[_i,_j] = (GameObject)Instantiate(listInimigos[_i],_posicao,listInimigos[_i].transform.rotation);
				_matrizInimigos[_i,_j].GetComponent<Inimigo>().quantidadeColunas = GameManager.QtdColunasInimigos;
				_matrizInimigos[_i,_j].GetComponent<Inimigo>().linha = _i;
				_matrizInimigos[_i,_j].GetComponent<Inimigo>().coluna = _j;
				_matrizInimigos[_i,_j].GetComponent<Inimigo>().velocidade = 300f;
			}
		}
	}

	private void GerarInimigoBonus()
	{
		int _valorPontoBonus = 100;
		int[] _arrayValores = {50,100,150,200}; 
		_valorPontoBonus = _arrayValores [Random.Range (0, _arrayValores.Length - 1)];

		Vector3 _posicao = new Vector3(-151f,147f);
		var _inimigo = (GameObject)Instantiate(InimigoBonus,_posicao,InimigoBonus.transform.rotation);
		_inimigo.GetComponent<Inimigo> ().valorPonto = _valorPontoBonus;
	}
	
	private void ChamarTiroInimigo()
	{
		int _linhaMin = 0;
		int _colunaMin = 0;
		float _min = 100;
		float _positionIni;
		var player = GameObject.FindWithTag("Player");

		_colunaRand = Random.Range (0, GameManager.QtdColunasInimigos);

		if (player != null) 
		{
			for (int _i=4; _i>=0; _i--) {
				for (int _j=0; _j<GameManager.QtdColunasInimigos; _j++) {
					if (_matrizInimigos [_i, _j] != null) 
					{
						_positionIni = Vector3.Distance (new Vector3 (_matrizInimigos [_i, _j].transform.position.x, 0), new Vector3 (player.transform.position.x, 0));

						if (_min > _positionIni) {
								_min = _positionIni;
								_linhaMin = _i;
								_colunaMin = _j;
						}
						
						if (_colunaRand == _j && _linhaRand < _i){
							_linhaRand = _i;
						}
					}
				}
			}

			ExecutarTiro(_linhaMin,_colunaMin);
			ExecutarTiro();
		}
	}

	private void ExecutarTiro()
	{
		ExecutarTiro (_linhaRand, _colunaRand);
	}
	
	private void ExecutarTiro(int LinhaInimigo, int ColunaInimigo)
	{
		if (_matrizInimigos [LinhaInimigo, ColunaInimigo] != null)
			_matrizInimigos [LinhaInimigo, ColunaInimigo].GetComponent<Inimigo> ().Atirar ();
	}

	public void AtualizarMatriz(int linha, int coluna, int colunaOrigem)
	{
		bool _reduzirColuna = true;
		int _quatidadeCols = 0;
		int _primeiroItem = 0;
		int _ultimoItem = 0;
		int _posicao = 0;
		_totalInimigos--;

		_matrizInimigos [linha, colunaOrigem] = null;
		_totalInimigosColuna [colunaOrigem] -= 1;  

		//Calcular quantidade de Colunas existem no game
		for(int _i=0;_i<GameManager.QtdColunasInimigos;_i++)
		{
			if(_totalInimigosColuna[_i] >0)
			{
				_primeiroItem = _i;
				break;
			}
			_quatidadeCols++;
		}

		for(int _i = GameManager.QtdColunasInimigos-1;_i>=0;_i--)
		{
			if(_totalInimigosColuna[_i] >0)
			{
				_ultimoItem = _i;
				break;
			}
			_quatidadeCols++;
		}

		_quatidadeCols = GameManager.QtdColunasInimigos - _quatidadeCols;

		for (int _i=4; _i>=0; _i--) 
		{
			_posicao = 0;
			for (int _j=_primeiroItem; _j<=_ultimoItem; _j++) 
			{
				if (_matrizInimigos [_i, _j] != null) 
				{
					if (_totalInimigos % (GameManager.QtdColunasInimigos / 2) == 0) 
					{
						_matrizInimigos [_i, _j].GetComponent<Inimigo>().velocidade -=15;
					}

					_matrizInimigos [_i, _j].GetComponent<Inimigo>().coluna =_posicao;
					_matrizInimigos [_i, _j].GetComponent<Inimigo>().quantidadeColunas = _quatidadeCols;
					/*if((coluna +_colunasDestruidas) <_j)
					{
					}*/
					_matrizInimigos [_i, _j].GetComponent<Inimigo>().RecalcularPosicoes();
				}
				_posicao++;
			}
		}

		if (_totalInimigos == 0) 
		{
			GerarInimigos();
		}
	}
}
