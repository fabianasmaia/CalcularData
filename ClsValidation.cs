using System;


public class ClsValidation
{
	public bool ValidaOperador( char operador)
	{
        try
        {
            return (operador = '+' || operador = '-');
        }
        catch (Exception)
        {
            return false; 
            throw;
        }
	}
}
