using System;
using System.Collections.Generic;
using System.Globalization;

namespace DIOBank.ConsoleApp
{
    class Program
    {
		static string opcaoUsuario = "";
		static List<Conta> listaContas = new List<Conta>();

		static void Main(string[] args)
        {
			InserirCabecalho();

			do
            {
				MontarMenu();

				opcaoUsuario = ObterOpcaoUsuario();

				switch (opcaoUsuario)
				{
					case "1":
                        ListarContas();
                        break;
					case "2":
						InserirConta();
						break;
					case "3":
						Sacar();
						break;
					case "4":
						Depositar();
						break;
					case "5":
                        Transferir();
                        break;
					case "C":
						LimparConsole();
						InserirCabecalho();
						break;
					case "X":
						break;
					default:
						Console.WriteLine("Opção Inválida!");
						break;
				}

			} while (opcaoUsuario != "X");

			Console.WriteLine("Obrigado por utilizar nossos serviços!");
			Console.ReadLine();
		}

		private static void InserirCabecalho()
        {
			CultureInfo culture = new CultureInfo("pt-BR");
			DateTimeFormatInfo dateTimeFormat = culture.DateTimeFormat;
			DateTime dataAtual = DateTime.Now;

			var dia = dataAtual.Day;
			var ano = dataAtual.Year;
			var mes = culture.TextInfo.ToTitleCase(dateTimeFormat.GetMonthName(DateTime.Now.Month));
			string diaDaSemana = culture.TextInfo.ToTitleCase(dateTimeFormat.GetDayName(DateTime.Now.DayOfWeek));

            Console.WriteLine(".:: Seja Bem Vindo ao DIO Bank! ::.");
            Console.WriteLine($"{diaDaSemana}, {dia} de {mes} de {ano}");
        }

		private static void MontarMenu()
		{
			Console.WriteLine();
			Console.WriteLine("Informe a opção desejada:");

			Console.WriteLine("1 - Listar Contas");
			Console.WriteLine("2 - Criar Conta");
			Console.WriteLine("3 - Sacar");
			Console.WriteLine("4 - Depositar");
			Console.WriteLine("5 - Transferir");
			Console.WriteLine("C - Limpar Tela");
			Console.WriteLine("X - Sair");
			Console.WriteLine();
		}

		private static string ObterOpcaoUsuario()
		{
            Console.Write("Opção: ");
			string opcaoUsuario = Console.ReadLine().ToUpper();
			Console.WriteLine();
			return opcaoUsuario;
		}
		
		private static void LimparConsole()
		{
			Console.Clear();
		}

        private static void ListarContas()
        {
            Console.WriteLine(".:: Listando Todas as Contas ::.");

            if (listaContas.Count == 0)
            {
                Console.WriteLine("Nenhuma Conta Cadastrada!");
                return;
            }

            for (int i = 0; i < listaContas.Count; i++)
            {
                Conta conta = listaContas[i];
                Console.WriteLine($"#{i + 1} - {conta}");
            }

            Console.WriteLine();
        }

		private static void InserirConta()
		{
			Console.WriteLine(".:: Inserindo uma Nova Conta ::.");

			var tipoConta = GetInputTipoConta();
			var nome = GetInputNomeCliente();
			var saldo = GetInputSaldoInicial();
			var creditoEspecial = GetInputCreditoEspecial();

			Conta novaConta = new (tipoConta, saldo, creditoEspecial, nome);

			listaContas.Add(novaConta);
		}

		private static void Sacar()
		{
			int codigoConta;

			do
			{
				codigoConta = GetInputConta();

			} while (!VerificarSeContaValida(codigoConta, listaContas));

			double valorSaque = GetInputValor();

			listaContas[codigoConta - 1].Sacar(valorSaque);
		}

		private static void Depositar()
		{
			int codigoConta;

			do
			{
				codigoConta = GetInputConta();

			} while (!VerificarSeContaValida(codigoConta, listaContas));

			double valorSaque = GetInputValor();

			listaContas[codigoConta - 1].Depositar(valorSaque);
		}

		private static void Transferir()
		{
			if (listaContas.Count == 0)
			{
				Console.WriteLine("Nenhuma Conta Cadastrada!");
				return;
			}

			Console.WriteLine(".:: Transferência Entre Contas ::.");

			int codigoContaOrigem; 
			int codigoContaDestino;

            do
            {
				codigoContaOrigem = GetInputContaDestino();
				codigoContaDestino = GetInputContaOrigem();

			} while (!VerificarSeContasValidas(codigoContaOrigem, codigoContaDestino, listaContas));
			
			var valorTransferencia = GetInputValor();

			listaContas[codigoContaOrigem - 1].Transferir(valorTransferencia, listaContas[codigoContaDestino - 1]);
		}

        private static TipoConta GetInputTipoConta()
		{
			Console.Write("Digite 1 para Conta Fisica ou 2 para Jurídica: ");
			int tipoConta;
			while (!int.TryParse(Console.ReadLine(), out tipoConta))
			{
				Console.WriteLine("Opção Inválida!");
				Console.Write("Digite 1 para Conta Fisica ou 2 para Jurídica: ");
			}

			return (TipoConta)tipoConta;
		}

		private static string GetInputNomeCliente()
		{
			Console.Write("Digite o Nome do Cliente: ");
			string nomeCliente = Console.ReadLine();

			return nomeCliente;
		}

		private static double GetInputSaldoInicial()
		{
			Console.Write("Digite o Saldo Inicial: ");
			double saldoInicial;
			while (!double.TryParse(Console.ReadLine(), out saldoInicial))
			{
				Console.WriteLine("Valor inválido!");
				Console.Write("Digite o Saldo Inicial: ");
			}

			return saldoInicial;
		}

		private static double GetInputCreditoEspecial()
		{
			Console.Write("Digite o Crédito Especial: ");
			double creditoEspecial;
			while (!double.TryParse(Console.ReadLine(), out creditoEspecial))
			{
				Console.WriteLine("Valor inválido!");
				Console.Write("Digite o Crédito Especial: ");
			}

			return creditoEspecial;
		}

		private static int GetInputConta()
		{
			Console.Write("Digite o Número da Conta: ");
			int codigoConta;
			while (!int.TryParse(Console.ReadLine(), out codigoConta))
			{
				Console.WriteLine("Opção Inválida!");
				Console.Write("Digite o Número da Conta: ");
			}

			return codigoConta;
		}

		private static int GetInputContaOrigem()
		{
			Console.Write("Digite o Número da Conta de Origem: ");
			int codigoContaOrigem;
			while (!int.TryParse(Console.ReadLine(), out codigoContaOrigem))
			{
				Console.WriteLine("Opção Inválida!");
				Console.Write("Digite o Número da Conta de Origem: ");
			}

			return codigoContaOrigem;
		}

		private static int GetInputContaDestino()
		{
			Console.Write("Digite o Número da Conta de Destino: ");
			int codigoContaDestino;
			while (!int.TryParse(Console.ReadLine(), out codigoContaDestino))
			{
				Console.WriteLine("Opção Inválida!");
				Console.Write("Digite o Número da Conta de Destino: ");
			}

			return codigoContaDestino;
		}

		private static double GetInputValor()
		{
			Console.Write("Digite o Valor Desejado: ");
			double valor;
			while (!double.TryParse(Console.ReadLine(), out valor))
			{
				Console.WriteLine("Valor inválido!");
				Console.Write("Digite o Valor Desejado: ");
			}

			return valor;
		}

		private static bool VerificarSeContasValidas(int contaOrigem, int contaDestino, List<Conta> listaContas)
		{
			var quantidadeContas = listaContas.Count;

			if (contaOrigem > quantidadeContas)
            {
				Console.WriteLine();
				Console.WriteLine("Código da Conta de Origem Inexistente!");
                Console.WriteLine("Operação Falhou. Tente novamente!");
				Console.WriteLine();
				return false;
            }

			if (contaDestino > quantidadeContas)
			{
                Console.WriteLine();
				Console.WriteLine("Código da Conta de Destino Inexistente!");
				Console.WriteLine("Operação Falhou. Tente novamente!");
				Console.WriteLine();
				return false;
			}

			return true;
		}

		private static bool VerificarSeContaValida(int conta, List<Conta> listaContas)
        {
			var quantidadeContas = listaContas.Count;

			if (conta > quantidadeContas)
			{
				Console.WriteLine();
				Console.WriteLine("Código da Conta Inexistente!");
				Console.WriteLine("Operação Falhou. Tente novamente!");
				Console.WriteLine();
				return false;
			}

			return true;
        }
	}
}

