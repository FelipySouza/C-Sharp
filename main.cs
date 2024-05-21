using System;

namespace atividadejeferson2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string FG = "";
            Cliente[] clientes = new Cliente[5];
            for (int i = 0; i < clientes.Length; i++)
            {
                clientes[i] = new Cliente();
            }

            do
            {
                Console.Clear();
                Console.WriteLine("\n1. Adicionar clientes");
                Console.WriteLine("2. Atender cliente");
                Console.WriteLine("3. Exibir lista de clientes");
                Console.WriteLine("4. Sair");
                Console.Write("Escolha uma opção: ");
                int opcao = int.Parse(Console.ReadLine());

                switch (opcao)
                {
                    case 1:
                        Console.Clear();
                        // Adicionar clientes
                        for (int i = 0; i < clientes.Length; i++)
                        {
                            Console.Write("Digite o nome do cliente {0}: ", i + 1);
                            string nome = Console.ReadLine();
                            string prioridadeInput;
                            bool? prioridade;
                            do
                            {
                                Console.Write("Digite a prioridade do cliente {0} (alta/baixa): ", i + 1);
                                prioridadeInput = Console.ReadLine();
                                prioridade = ParsePrioridade(prioridadeInput);

                                if (!prioridade.HasValue)
                                {
                                    Console.WriteLine("Prioridade inválida. Por favor, digite 'alta' ou 'baixa'.");
                                }
                            } while (!prioridade.HasValue);

                            clientes[i].Cadastrar(nome, prioridade);
                        }
                        for(int i = 0; i < clientes.Length; i++){
                        AjustarPrioridade(clientes);
                        }
                        break;

                    case 2:
                    Console.Clear();
                        // Atender o cliente
                        if (TodosClientesAtendidos(clientes))
                        {
                            Console.WriteLine("Todos os clientes foram atendidos");
                            Console.ReadKey();
                        }
                        else
                        {
                            AtenderCliente(clientes);
                            AjustarPrioridade(clientes);
                            Console.WriteLine("Cliente atendido. Fila atualizada.");
                            Console.ReadKey();
                        }
                        break;

                    case 3:
                    Console.Clear();
                        // Exibir lista de clientes
                        Console.WriteLine("\nLista de Clientes:");
                        for (int i = 0; i < clientes.Length; i++)
                        {
                            Console.WriteLine("Cliente {0}: {1}\t\tPrioridade: {2}", i + 1, clientes[i].Nome, clientes[i].Prioridade.HasValue ? (clientes[i].Prioridade.Value ? "alta" : "baixa") : "null");
                        Console.ReadKey();
                        }
                        break;

                    case 4:
                    Console.Clear();
                        // Sair do programa
                        FG = "q";
                        break;

                    default:
                    Console.Clear();
                        Console.WriteLine("Opção inválida.");
                        Console.ReadKey();
                        break;
                }
                Console.Clear();
                if (FG != "q")
                {
                    Console.WriteLine("\nVocê deseja continuar?");
                    Console.WriteLine("(Digite 'q' para fechar)");
                    Console.WriteLine("(Digite qualquer tecla para continuar)");

                    FG = Console.ReadLine();
                }

            } while (FG != "q");
        }

        static void AtenderCliente(Cliente[] clientes)
        {
            for (int i = 0; i < clientes.Length - 1; i++)
            {
                clientes[i] = clientes[i + 1];
            }
            clientes[clientes.Length - 1] = new Cliente();
            clientes[clientes.Length - 1].Cadastrar("", null);
        }

        static void AjustarPrioridade(Cliente[] clientes)
        {
            for (int i = 0; i < clientes.Length - 1; i++)
            {
                if (clientes[i].Prioridade == false && clientes[i + 1].Prioridade == true)
                {
                    Cliente clienteAux = clientes[i];
                    clientes[i] = clientes[i + 1];
                    clientes[i + 1] = clienteAux;
                }
            }
        }

        static bool? ParsePrioridade(string input)
        {
            if (input.Equals("alta", StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
            else if (input.Equals("baixa", StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }
            return null;
        }

        static bool TodosClientesAtendidos(Cliente[] clientes)
        {
            foreach (var cliente in clientes)
            {
                if (!string.IsNullOrEmpty(cliente.Nome) || cliente.Prioridade.HasValue)
                {
                    return false;
                }
            }
            return true;
        }
    }

    public class Cliente
    {
        public string Nome { get; set; }
        public bool? Prioridade { get; set; } // Nullable bool

        public void Cadastrar(string nome, bool? prioridade)
        {
            Nome = nome;
            Prioridade = prioridade;
        }
    }
}
