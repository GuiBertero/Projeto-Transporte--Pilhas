
public class Veiculo
{
    public int id;
    public int capacidade;
    public int passageirosTransportados;

    public Veiculo()
    {
        capacidade = 15;
        passageirosTransportados = 0;
    }

    public Veiculo(int id, int capacidade)
    {
        this.id = id;
        this.capacidade = capacidade;
        passageirosTransportados = 0;
    }
}
