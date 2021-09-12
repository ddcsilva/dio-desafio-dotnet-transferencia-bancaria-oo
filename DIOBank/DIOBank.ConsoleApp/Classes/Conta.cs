using System;

namespace DIOBank.ConsoleApp
{
	public class Conta
	{
		public Conta(TipoConta tipoConta, double saldo, double creditoEspecial, string nome)
		{
			this.TipoConta = tipoConta;
			this.Saldo = saldo;
			this.CreditoEspecial = creditoEspecial;
			this.Nome = nome;
		}

		private TipoConta TipoConta { get; set; }
		private double Saldo { get; set; }
		private double CreditoEspecial { get; set; }
		private string Nome { get; set; }

		public bool Sacar(double valor)
		{
			if (this.Saldo + this.CreditoEspecial < valor)
			{
				Console.WriteLine("Saldo insuficiente!");
				return false;
			}

			this.Saldo -= valor;

			Console.WriteLine("Saldo atual da conta de {0} é {1}", this.Nome, this.Saldo);
			return true;
		}

		public void Depositar(double valor)
		{
			this.Saldo += valor;

			Console.WriteLine("Saldo atual da conta de {0} é {1}", this.Nome, this.Saldo);
		}

		public void Transferir(double valorTransferencia, Conta contaDestino)
		{
			if (this.Sacar(valorTransferencia))
			{
				contaDestino.Depositar(valorTransferencia);
			}
		}

		public override string ToString()
		{
			string retorno = "";
			retorno += "Tipo da Conta: " + this.TipoConta + " | ";
			retorno += "Nome: " + this.Nome + " | ";
			retorno += "Saldo: " + this.Saldo + " | ";
			retorno += "Crédito Especial: " + this.CreditoEspecial;
			return retorno;
		}
	}
}
