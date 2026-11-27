
using System.Collections.Generic;

public class Garagem
{
    public int id;
    public string nome;
    public Stack<Veiculo> veiculos;

    public Garagem()
    {
        veiculos = new Stack<Veiculo>();
    }

    public void estacionar(Veiculo veiculo)
    {
        veiculos.Push(veiculo);
    }

    public Veiculo retirar()
    {
        if (veiculos.Count == 0)
            return null;

        return veiculos.Pop();
    }
}
