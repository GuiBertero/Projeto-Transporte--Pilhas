
using System;
using System.Collections.Generic;
using System.Linq;

public class Empresa
{
    public List<Veiculo> veiculos;
    public List<Garagem> garagens;
    public List<Viagem> viagens;
    public bool jornadaIniciada;

    public Empresa()
    {
        veiculos = new List<Veiculo>();
        garagens = new List<Garagem>();
        viagens = new List<Viagem>();
        jornadaIniciada = false;

        // Cadastro inicial das garagens

        Garagem g1 = new Garagem();
        g1.id = 1;
        g1.nome = "Congonhas";

        Garagem g2 = new Garagem();
        g2.id = 2;
        g2.nome = "Guarulhos";

        garagens.Add(g1);
        garagens.Add(g2);

        // Cadastro inicial das 8 vans

        for (int i = 1; i <= 8; i++)
        {
            veiculos.Add(new Veiculo(i, 15));
        }
    }

    public bool cadastrarVeiculo(Veiculo veiculo)
    {
        if (jornadaIniciada)
            return false;

        if (veiculos.Any(v => v.id == veiculo.id))
            return false;

        veiculos.Add(veiculo);
        return true;
    }

    public bool cadastrarGaragem(Garagem garagem)
    {
        if (jornadaIniciada)
            return false;

        if (garagens.Any(g => g.id == garagem.id))
            return false;

        garagens.Add(garagem);
        return true;
    }

    public Garagem pesquisarGaragem(int id)
    {
        return garagens.FirstOrDefault(g => g.id == id);
    }

    public Veiculo pesquisarVeiculo(int id)
    {
        return veiculos.FirstOrDefault(v => v.id == id);
    }

    public bool iniciarJornada()
    {
        if (jornadaIniciada || garagens.Count < 2)
            return false;

        // Retira todos os veículos das garagens

        foreach (Garagem garagem in garagens)
        {
            garagem.veiculos.Clear();
        }

        // Distribui os veículos alternadamente

        for (int i = 0; i < veiculos.Count; i++)
        {
            int indice = i % garagens.Count;

            garagens[indice].estacionar(veiculos[i]);
        }

        viagens.Clear();

        foreach (Veiculo veiculo in veiculos)
        {
            veiculo.passageirosTransportados = 0;
        }

        jornadaIniciada = true;

        return true;
    }

    public bool liberarViagem(int idOrigem, int idDestino,
                              int passageiros)
    {
        if (!jornadaIniciada)
            return false;

        Garagem origem = pesquisarGaragem(idOrigem);
        Garagem destino = pesquisarGaragem(idDestino);

        if (origem == null || destino == null)
            return false;

        if (origem == destino)
            return false;

        // Verifica se existe veículo na garagem de origem

        if (origem.veiculos.Count == 0)
            return false;

        // Consulta o próximo veículo

        Veiculo veiculo = origem.veiculos.Peek();

        // Verifica se a lotação está completa

        if (passageiros != veiculo.capacidade)
            return false;

        // Retira o veículo da origem

        veiculo = origem.retirar();

        // Estaciona no destino

        destino.estacionar(veiculo);

        // Atualiza a quantidade de passageiros

        veiculo.passageirosTransportados += passageiros;

        // Registra a viagem

        Viagem viagem = new Viagem(
            origem, destino, veiculo, passageiros);

        viagens.Add(viagem);

        return true;
    }

    public void listarVeiculos(Garagem garagem)
    {
        if (garagem == null)
            return;

        Console.WriteLine("\nGaragem: " + garagem.nome);
        Console.WriteLine("Quantidade de veiculos: " +
                          garagem.veiculos.Count);

        int capacidadeTotal = 0;

        foreach (Veiculo veiculo in garagem.veiculos)
        {
            Console.WriteLine("Van " + veiculo.id +
                              " - Capacidade: " + veiculo.capacidade);

            capacidadeTotal += veiculo.capacidade;
        }

        Console.WriteLine("Potencial de transporte: " +
                          capacidadeTotal + " passageiros");
    }

    public int quantidadeViagens(int idOrigem, int idDestino)
    {
        return viagens.Count(v =>
            v.origem.id == idOrigem &&
            v.destino.id == idDestino);
    }

    public void listarViagens(int idOrigem, int idDestino)
    {
        foreach (Viagem viagem in viagens)
        {
            if (viagem.origem.id == idOrigem &&
                viagem.destino.id == idDestino)
            {
                Console.WriteLine("-------------------------");
                Console.WriteLine("Veiculo: " + viagem.veiculo.id);
                Console.WriteLine("Origem: " + viagem.origem.nome);
                Console.WriteLine("Destino: " + viagem.destino.nome);
                Console.WriteLine("Passageiros: " + viagem.passageiros);
                Console.WriteLine("Data: " + viagem.data);
            }
        }
    }

    public int quantidadePassageiros(int idOrigem, int idDestino)
    {
        int total = 0;

        foreach (Viagem viagem in viagens)
        {
            if (viagem.origem.id == idOrigem &&
                viagem.destino.id == idDestino)
            {
                total += viagem.passageiros;
            }
        }

        return total;
    }

    public bool encerrarJornada()
    {
        if (!jornadaIniciada)
            return false;

        Console.WriteLine("\n===== RELATORIO DA JORNADA =====");

        foreach (Veiculo veiculo in veiculos)
        {
            Console.WriteLine("Veiculo: " + veiculo.id);
            Console.WriteLine("Passageiros transportados: " +
                              veiculo.passageirosTransportados);
        }

        // Limpa os registros da jornada

        viagens.Clear();

        foreach (Veiculo veiculo in veiculos)
        {
            veiculo.passageirosTransportados = 0;
        }

        jornadaIniciada = false;

        Console.WriteLine("\nJornada encerrada!");

        return true;
    }
}
