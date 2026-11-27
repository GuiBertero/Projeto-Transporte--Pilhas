
using System;

public class Viagem
{
    public Garagem origem;
    public Garagem destino;
    public Veiculo veiculo;
    public int passageiros;
    public DateTime data;

    public Viagem(Garagem origem, Garagem destino,
                  Veiculo veiculo, int passageiros)
    {
        this.origem = origem;
        this.destino = destino;
        this.veiculo = veiculo;
        this.passageiros = passageiros;
        data = DateTime.Now;
    }
}
