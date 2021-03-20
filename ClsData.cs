using System;



public class ClsData
{
	public string CalcularData(string data, char operacao, long valor)
	{
		int[] DiasMes = new int[12] { 31,28,31,30,31,30,31,31,30,31,30,31};
		int dia;
		int mes;
		int ano;
		
		
		//transforma em horas o valor usado na operação
		int horasAdd = valor % 60;
		
		if(horasAdd > 24)
        {
			
        }

	}
}
