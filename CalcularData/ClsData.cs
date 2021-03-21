using System;



public class ClsData
{
    public enum posicaoData
    {
        dia = 0,
        mes = 1,
        ano = 2
    }

    public enum posicaoHorario
    {
        hora = 0,
        min = 1
    }
    //variavel para armazenar os dias de cada mês;
    public int[] DiasMes = new int[12] { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };

    public string CalcularData(string data, char operacao, long valor)
    {
        //variáveis nomeadas com "operação" guardam numeros calculados ao longo do método.
        //variaveis nomeadas com "final" guardam os valores finais
        int dia, mes, ano, hora, min, horasOperacao, minOperacao, diasOperacao,  horaFinal, minFinal, diaFinal, mesFinal, anoFinal;
        try
        {
            horasOperacao = 0;
            minOperacao = 0;
            diasOperacao = 0;           
            diaFinal = 0;
            mesFinal = 0;
            anoFinal = 0;
            horaFinal = 0;
            minFinal = 0;

            

            //separa data e hora;
            string[] splitDataHora = data.Split();

            //separa dia, mes e ano. Já convertendo para inteiro;
            int[] splitData = Array.ConvertAll<string, int>(splitDataHora[0].Split('/'), int.Parse);

            dia = splitData[(int)posicaoData.dia];
            mes = splitData[(int)posicaoData.mes];
            ano = splitData[(int)posicaoData.ano];

            //separa hora e minutos;
            int[] splitHora = Array.ConvertAll<string, int>(splitDataHora[1].Split(':'), int.Parse);

            hora = splitHora[(int)posicaoHorario.hora];
            min = splitHora[(int)posicaoHorario.min];

            //valida se a data informada é válida
            if (!ValidarData(dia, mes, hora, min))
                throw new System.ArgumentException("Data/hora informada não é válida.", "Data");

            //obtem o resto inteiro da divisao do valor por 60 min.
            minOperacao = (int)valor % 60;

            //horas compreendem o valor informado
            horasOperacao = (int)valor / 60;

            //dias que compreendem o valor informado
            diasOperacao = horasOperacao / 24;

            //define, em caso de operação +, o resto inteiro da divisão por 24 + hora informado; ou subtração, hora informada - hora calculada.
            horaFinal = (operacao == '+') ? (horasOperacao % 24) + hora : hora - (horasOperacao % 24);
            
            //se hora maior que 24, aumenta dias de operação
            while (horaFinal > 24)
            {
                diasOperacao++;
                horaFinal = horaFinal - 24;
            }

            //se hora menor que zero, quebra mais um dia e aumenta o dia de operação 
            if (horaFinal < 0)
            {
                horaFinal = 24 + horaFinal;
                diasOperacao++;
            }

            //valida se há min quebrados na operação
            if (minOperacao > 0)
                //se sim, soma aos min da hora ou diminui, de acordo com a opeação
                minFinal = (operacao == '+') ? min + minOperacao : min - minOperacao;

            if(minOperacao == 0)
                //se é igual a zero. Significa que a conta é inteira com a hora informada. atribui os min da hr aos min finais.
                minFinal = min;

            if (minFinal < 0)
            {
                //se menor que 0, deve descontar de 60min para saber o minFinal
                //dimiui a hora
                minFinal = 60 + minFinal;
                horaFinal = horaFinal - 1;
            }

            if (minFinal > 60)
            {
                //se maior que 60, deve somar a horaFinal.
                horaFinal = (operacao == '+') ? horaFinal + (minFinal / 60) : horaFinal - (minFinal / 60);
                minFinal = minFinal % 60;
            }

            //soma o dia informado mais os dias de operação
            diaFinal = (operacao == '-') ? dia - diasOperacao : dia + diasOperacao;

            anoFinal = ano;
            mesFinal = mes;

            int mesAtual = mes;

            //verifica se não virou o mes -- em caso de operação +
            if (diaFinal > DiasMes[mes - 1])
            {

                while (diaFinal > DiasMes[mes - 1])
                {
                    //estabelece o corte do mes
                    diaFinal = diaFinal - DiasMes[mesAtual - 1];
                    mesAtual = mesAtual + 1;

                    if (mesAtual == 12)
                    {
                        mesAtual = 1;
                        anoFinal = anoFinal + 1;
                    }
                }
                mesFinal = mesAtual;
                
            }

            if (diaFinal < 0)//no caso de operação '-' pode ser que mês será descrescido
            {
                diaFinal = (diaFinal * (-1));

                //valida se vira o mes
                if (diaFinal > DiasMes[mes - 1])
                {
                    while (diaFinal > DiasMes[mes - 1])
                    {
                        //estabelece o corte do mes
                        diaFinal = diaFinal - DiasMes[mesAtual - 1];
                        mesAtual = mesAtual - 1;

                        if (mesAtual == 0)
                        {
                            mesAtual = 12;
                            anoFinal = anoFinal - 1;
                        }
                    }
                    mesFinal = mesAtual - 1;
                    diaFinal = DiasMes[mesFinal - 1] - diaFinal;
                }
                else
                {
                    mesFinal = mes - 1;

                    if (mesFinal == 0)
                    {
                        mesFinal = 12;
                        anoFinal = anoFinal - 1;
                    }

                    diaFinal = DiasMes[mesFinal - 1] - diaFinal;
                }

            }

            //se zerar o dia cai na virada de mes
            if (diaFinal == 0)
            {
                mesFinal = mes - 1;
                //se zerou então definir como dez
                if (mesFinal == 0)
                {
                    mesFinal = 12;
                    anoFinal = ano - 1;
                }

                diaFinal = DiasMes[mesFinal - 1];
            }

            string dataFinal = "";
            dataFinal = dataFinal + $"{ diaFinal.ToString("00") }";
            dataFinal = dataFinal + $"/{mesFinal.ToString("00") }";
            dataFinal = dataFinal + $"/{anoFinal.ToString("0000")}";
            dataFinal = dataFinal + $" {horaFinal.ToString("00")}";
            dataFinal = dataFinal + $":{minFinal.ToString("00")}";

            //return $"{diaFinal}/{mesFinal.ToString()}/{anoFinal} {horaFinal}:{minFinal}";
            return dataFinal;
        }


        catch (Exception ex)
        {
            return null;
            throw ex;

        }
    }

    public bool ValidarData(int dia, int mes, int hora, int min)
    {
        //valida se dia é maior que 1 e menor que DiasMes[]
        if (!(dia >= 1 && dia <= DiasMes[mes - 1]))
            return false;

        //valida se o mês é valido.
        if (!(mes >= 1 && mes <= 12))
            return false;

        //valida se a hora está dentro de um intervalo válido
        if (!(hora >= 0 && hora <= 23))
            return false;

        //valida se os minutos estão dentro de um intervalo válido
        if (!(min >= 0 && min <= 60))
            return false;

        return true;
    }

    public bool StringEmpty(string campo)
    {
        return campo == string.Empty;
    }
}
