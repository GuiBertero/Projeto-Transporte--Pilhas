
using System;

class Program
{
    static Empresa empresa = new Empresa();

    static void Main(string[] args)
    {
        int opcao;

        do
        {
            Console.WriteLine("\n===== SISTEMA DE TRANSPORTE =====");
            Console.WriteLine("0 - Finalizar");
            Console.WriteLine("1 - Cadastrar veiculo");
            Console.WriteLine("2 - Cadastrar garagem");
            Console.WriteLine("3 - Iniciar jornada");
            Console.WriteLine("4 - Encerrar jornada");
            Console.WriteLine("5 - Liberar viagem");
            Console.WriteLine("6 - Listar veiculos de uma garagem");
            Console.WriteLine("7 - Quantidade de viagens");
            Console.WriteLine("8 - Listar viagens");
            Console.WriteLine("9 - Quantidade de passageiros");
            Console.Write("Opcao: ");

            opcao = LerInt();

            switch (opcao)
            {
                case 1:
                    CadastrarVeiculo();
                    break;

                case 2:
                    CadastrarGaragem();
                    break;

                case 3:
                    IniciarJornada();
                    break;

                case 4:
                    EncerrarJornada();
                    break;

                case 5:
                    LiberarViagem();
                    break;

                case 6:
                    ListarVeiculos();
                    break;

                case 7:
                    QuantidadeViagens();
                    break;

                case 8:
                    ListarViagens();
                    break;

                case 9:
                    QuantidadePassageiros();
                    break;

                case 0:
                    if (empresa.jornadaIniciada)
                    {
                        Console.WriteLine(
                            "Encerre a jornada antes de finalizar!");
                        opcao = -1;
                    }
                    else
                    {
                        Console.WriteLine("Sistema finalizado!");
                    }
                    break;

                default:
                    Console.WriteLine("Opcao invalida!");
                    break;
            }

        } while (opcao != 0);
    }

    static int LerInt()
    {
        int valor;

        while (!int.TryParse(Console.ReadLine(), out valor))
        {
            Console.Write("Digite um numero valido: ");
        }

        return valor;
    }

    static void CadastrarVeiculo()
    {
        Veiculo veiculo = new Veiculo();

        Console.Write("ID do veiculo: ");
        veiculo.id = LerInt();

        Console.Write("Capacidade de passageiros: ");
        veiculo.capacidade = LerInt();

        if (veiculo.capacidade <= 0)
        {
            Console.WriteLine("Capacidade invalida!");
            return;
        }

        if (empresa.cadastrarVeiculo(veiculo))
            Console.WriteLine("Veiculo cadastrado!");
        else
            Console.WriteLine("Nao foi possivel cadastrar o veiculo!");
    }

    static void CadastrarGaragem()
    {
        Garagem garagem = new Garagem();

        Console.Write("ID da garagem: ");
        garagem.id = LerInt();

        Console.Write("Nome da garagem: ");
        garagem.nome = Console.ReadLine();

        if (empresa.cadastrarGaragem(garagem))
            Console.WriteLine("Garagem cadastrada!");
        else
            Console.WriteLine("Nao foi possivel cadastrar a garagem!");
    }

    static void IniciarJornada()
    {
        if (empresa.iniciarJornada())
        {
            Console.WriteLine("\nJornada iniciada!");

            foreach (Garagem garagem in empresa.garagens)
            {
                empresa.listarVeiculos(garagem);
            }
        }
        else
        {
            Console.WriteLine(
                "Nao foi possivel iniciar a jornada!");
        }
    }

    static void EncerrarJornada()
    {
        if (!empresa.encerrarJornada())
            Console.WriteLine("Nenhuma jornada em andamento!");
    }

    static void LiberarViagem()
    {
        if (!empresa.jornadaIniciada)
        {
            Console.WriteLine("Inicie a jornada primeiro!");
            return;
        }

        Console.WriteLine("\nGaragens cadastradas:");

        foreach (Garagem garagem in empresa.garagens)
        {
            Console.WriteLine(garagem.id + " - " + garagem.nome);
        }

        Console.Write("ID da origem: ");
        int origem = LerInt();

        Console.Write("ID do destino: ");
        int destino = LerInt();

        Console.Write("Quantidade de passageiros: ");
        int passageiros = LerInt();

        if (empresa.liberarViagem(origem, destino, passageiros))
        {
            Console.WriteLine("Viagem liberada com sucesso!");
        }
        else
        {
            Console.WriteLine(
                "Nao foi possivel liberar a viagem!");
            Console.WriteLine(
                "Verifique a origem, destino, disponibilidade do veiculo e lotacao.");
        }
    }

    static void ListarVeiculos()
    {
        Console.Write("ID da garagem: ");
        int id = LerInt();

        Garagem garagem = empresa.pesquisarGaragem(id);

        if (garagem == null)
        {
            Console.WriteLine("Garagem nao encontrada!");
            return;
        }

        empresa.listarVeiculos(garagem);
    }

    static void QuantidadeViagens()
    {
        Console.Write("ID da origem: ");
        int origem = LerInt();

        Console.Write("ID do destino: ");
        int destino = LerInt();

        if (empresa.pesquisarGaragem(origem) == null ||
            empresa.pesquisarGaragem(destino) == null ||
            origem == destino)
        {
            Console.WriteLine("Garagem invalida!");
            return;
        }

        int quantidade = empresa.quantidadeViagens(
            origem, destino);

        Console.WriteLine("Quantidade de viagens: " + quantidade);
    }

    static void ListarViagens()
    {
        Console.Write("ID da origem: ");
        int origem = LerInt();

        Console.Write("ID do destino: ");
        int destino = LerInt();

        if (empresa.pesquisarGaragem(origem) == null ||
            empresa.pesquisarGaragem(destino) == null ||
            origem == destino)
        {
            Console.WriteLine("Garagem invalida!");
            return;
        }

        empresa.listarViagens(origem, destino);
    }

    static void QuantidadePassageiros()
    {
        Console.Write("ID da origem: ");
        int origem = LerInt();

        Console.Write("ID do destino: ");
        int destino = LerInt();

        if (empresa.pesquisarGaragem(origem) == null ||
            empresa.pesquisarGaragem(destino) == null ||
            origem == destino)
        {
            Console.WriteLine("Garagem invalida!");
            return;
        }

        int total = empresa.quantidadePassageiros(
            origem, destino);

        Console.WriteLine(
            "Total de passageiros transportados: " + total);
    }
}
