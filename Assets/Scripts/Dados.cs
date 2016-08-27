using UnityEngine;
using System.Collections;

public static  class Dados {

	public static void SalvarMaiorPonto(int pPonto)
	{
		PlayerPrefs.SetInt ("Pontos", pPonto);
	}

	public static int PegarMaiorPonto()
	{
		return PlayerPrefs.GetInt ("Pontos");
	}

}
