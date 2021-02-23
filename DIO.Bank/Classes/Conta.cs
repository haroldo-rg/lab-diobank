using System;

namespace DIO.Bank
{
    public class Conta
    {
        private TipoConta TipoConta { get; set; }
        private double Saldo { get; set; }
        private double Credito { get; set; }
        private string Nome { get; set; }
        
        public Conta(TipoConta tipoConta, double saldo, double credito, string nome)
        {
            this.TipoConta = tipoConta;
            this.Saldo = saldo;
            this.Credito = credito;
            this.Nome = nome;
        }

        public bool Sacar(double valorSaque)
        {
            if(this.Saldo - valorSaque < (this.Credito * -1))
            {
                Console.WriteLine("Saldo insuficiente!");
                return false;
            }

            this.Saldo -= valorSaque;

            ImprimeSaldo();

            return true;
        }

        public void Depositar(double valorDeposito)
        {
            this.Saldo += valorDeposito;
            ImprimeSaldo();
        }

        private void ImprimeSaldo()
        {
            Console.WriteLine($"O saldo da conta de {this.Nome} é { String.Format("{0:0.00}", this.Saldo) }");
        }

        public void Transferir(double valorTransferencia, Conta contaDestino)
        {
            if(this.Sacar(valorTransferencia))
                contaDestino.Depositar(valorTransferencia);
        }

        public override string ToString()
        {
            return $"TipoConta: {this.TipoConta} | Nome: {this.Nome} | Saldo: { String.Format("{0:0.00}", this.Saldo) } | Crédito: { String.Format("{0:0.00}", this.Credito) }";
        }

    }
}