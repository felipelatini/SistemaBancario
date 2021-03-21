using SistemaBancario.Classe;
using System;
using System.Collections.Generic;

namespace SistemaBancario
{
    class Program
    {
        static readonly List<Conta> lstConta = new();

        static void Main()
        {
            string opcaoUsuario = ObterOpcaoUsuario();

            while(opcaoUsuario.ToUpper() != "X")
            {                
                switch (opcaoUsuario)
                {
                    case "1":
                        ListarContas();
                        break;
                    case "2":
                        InserirConta();
                        break;
                    case "3":
                        Transferir();
                        break;
                    case "4":
                        Sacar();
                        break;
                    case "5":
                        Depositar();
                        break;
                    case "C":
                        Console.Clear();
                        break;
                    default:
                        Console.WriteLine("Opção inválida!");
                        break;
                }

                opcaoUsuario = ObterOpcaoUsuario();
            }

            Console.WriteLine("Obrigado por utilizar o sistema de controle bancário.");
        }

        private static void Depositar()
        {
            Console.WriteLine("Depositar conta");

            if (!VerificarExistenciaContas()) return;

            Console.WriteLine("Digite o número da conta: ");
            int indiceConta      = VerificarIndice(Console.ReadLine());
            if (indiceConta == -1) return;
            if (!VerificarExistenciaConta(indiceConta)) return;

            Console.WriteLine("Digite o valor a ser depositado: ");
            double valorDeposito = VerificarValor(Console.ReadLine());
            if (valorDeposito == -1) return;

            lstConta[indiceConta].Depositar(valorDeposito);            
        }

        private static void InserirConta()
        {
            Console.WriteLine("Inserir uma nova conta");

            Console.WriteLine("Digite 1 para conta física ou 2 para conta jurídica: ");
            int entradaTipoConta  = VerificarTipoConta(Console.ReadLine());
            if (entradaTipoConta == -1) return;

            Console.WriteLine("Digite o nome do cliente: ");
            string entradaNome    = Console.ReadLine();
            if (String.IsNullOrEmpty(entradaNome))
            {
                Console.WriteLine("Nome inválido!");
                return;
            }

            Console.WriteLine("Digite o saldo inicial: ");
            double entradaSaldo   = VerificarValor(Console.ReadLine());
            if (entradaSaldo == -1) return;

            Console.WriteLine("Digite o crédito inicial: ");
            double entradaCredito = VerificarValor(Console.ReadLine());
            if (entradaCredito == -1) return;

            Conta novaConta       = new(tipoConta: (Enum.TipoConta)entradaTipoConta, saldo: entradaSaldo, credito: entradaCredito, nome: entradaNome );
            lstConta.Add(novaConta);

            Console.WriteLine("Conta inserida com sucesso!");
        }

        private static void ListarContas()
        {
            Console.WriteLine("Lista as contas");

            if (!VerificarExistenciaContas()) return;

            for(int i = 0; i < lstConta.Count; i++)
            {
                Conta conta = lstConta[i];
                Console.Write("#{0} . ", i);
                Console.WriteLine(conta);
            }            
        }

        private static void Sacar()
        {
            Console.WriteLine("Sacar conta");

            Console.WriteLine("Digite o número da conta: ");
            int indiceConta   = VerificarIndice(Console.ReadLine());
            if (indiceConta == -1) return;
            if (!VerificarExistenciaConta(indiceConta)) return;

            Console.WriteLine("Digite o valor a ser sacado: ");
            double valorSaque = VerificarValor(Console.ReadLine());
            if (valorSaque == -1) return;

            lstConta[indiceConta].Sacar(valorSaque);
        }

        private static void Transferir()
        {
            Console.WriteLine("Transferir conta");

            Console.WriteLine("Digite o número da conta de origem: ");
            int indiceContaOrigem     = VerificarIndice(Console.ReadLine());
            if (indiceContaOrigem == -1) return;
            if (!VerificarExistenciaConta(indiceContaOrigem)) return;

            Console.WriteLine("Digite o número da conta de destino: ");
            int indiceContaDestino    = VerificarIndice(Console.ReadLine());
            if (indiceContaDestino == -1) return;
            if (!VerificarExistenciaConta(indiceContaDestino)) return;

            Console.WriteLine("Digite o valor a ser transferido: ");
            double valorTransferencia = VerificarValor(Console.ReadLine());
            if (valorTransferencia == -1) return;

            lstConta[indiceContaOrigem].Transferir(valorTransferencia, lstConta[indiceContaDestino]);
        }

        public static string ObterOpcaoUsuario()
        {
            Console.WriteLine();
            Console.WriteLine("Bem vindo ao sistema de controle bancário!!");
            Console.WriteLine("Informe a opção desejada:");

            Console.WriteLine("1 - Listar contas");
            Console.WriteLine("2 - Inserir nova conta");
            Console.WriteLine("3 - Transferir");
            Console.WriteLine("4 - Sacar");
            Console.WriteLine("5 - Depositar");
            Console.WriteLine("C - Limpar tela");
            Console.WriteLine("X - Sair");
            Console.WriteLine();

            string opcaoUsuario = Console.ReadLine().ToUpper();
            return opcaoUsuario;
        }

        public static bool VerificarExistenciaConta(int indice)
        {
            if (indice + 1 <= lstConta.Count) return true;
            else
            {
                Console.WriteLine("Não há nenhuma conta cadastrada.");
                return false;
            }
        }

        public static bool VerificarExistenciaContas()
        {
            if (lstConta.Count == 0)
            {
                Console.WriteLine("Não há nenhuma conta cadastrada.");
                return false;
            }
            else 
                return true;
        }

        public static int VerificarIndice(string indice)
        {
            if (int.TryParse(indice, out int retorno)) return retorno;
            else
            {
                Console.WriteLine("Índice inválido!");
                return -1;
            }
        }

        public static int VerificarTipoConta(string indice)
        {
            if (int.TryParse(indice, out int retorno))
            {
                if ((retorno != 1) && (retorno != 2))
                {
                    Console.WriteLine("Índice inválido!");
                    return -1;
                }
                else 
                    return retorno;
            }                
            else
            {
                Console.WriteLine("Índice inválido!");
                return -1;
            }
        }

        public static double VerificarValor(string valor)
        {
            if (double.TryParse(valor, out double retorno))
            {
                if (retorno < 0)
                {
                    Console.WriteLine("Valor digitado inválido!");
                    return -1;
                }
                else
                    return retorno;
            }                
            else
            {
                Console.WriteLine("Valor digitado inválido!");
                return -1;
            }                
        }
    }
}
