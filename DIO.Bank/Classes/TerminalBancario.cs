using System;
using System.Collections.Generic;

namespace DIO.Bank
{
    public class TerminalBancario        
    {
        private List<Conta> listContas;
        
        public TerminalBancario()
        {
            this.listContas = new List<Conta>();
        }

        public void Iniciar()
        {
            while(ObterOpcaoUsuario() != "X") {}
        }

        private string ObterOpcaoUsuario()
        {
            Console.Write(@"
DIO Bank ao seu dispor!!!
1 - Listar contas
2 - Inserir nova conta
3 - Transferir
4 - Sacar
5 - Depositar
C - Limpar tela
X - Sair

Informe a opção desejada: ");

            string opcao = Console.ReadLine().ToUpper();

            switch (opcao)
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
                    LimparTela();
                    break;
                
                case "X":
                    Sair();
                    break;
                
                default:
                    TratarOpcaoInvalida();
                    break;
            }

            return opcao;

        }

        private void ListarContas()
        {
            Console.WriteLine("Lista de contas");

            if(listContas.Count == 0)
            {
                Console.WriteLine("Nenhuma conta cadastrada.");
                return;
            }

            for(int i=0; i < listContas.Count; i++)
                Console.WriteLine($"#{i} - {listContas[i]}");
        }
        private void InserirConta()
        {
            Console.Write("Digite 1 para conta física ou 2 para jurídica: ");
            TipoConta tipoConta = (TipoConta)int.Parse(Console.ReadLine());

            Console.Write("Digite o nome do cliente: ");
            string nomeCliente = Console.ReadLine().Trim();

            Console.Write("Digite o saldo inicial: ");
            double saldoInicial = double.Parse(Console.ReadLine());

            Console.Write("Digite o limite de crédito: ");
            double limiteCredito = double.Parse(Console.ReadLine());

            Conta novaConta = new Conta(tipoConta, saldoInicial, limiteCredito, nomeCliente);

            listContas.Add(novaConta);
        }

        private void Sacar()
        {
            Console.Write("Digite o número da conta: ");
            int numeroConta = int.Parse(Console.ReadLine());

            if(!VerificaExistenciaConta(numeroConta))
            {
                ExibeMensagemContaInvalida(numeroConta);
                return;
            }

            Console.Write("Digite o valor a ser sacado: ");
            double valorSaque = double.Parse(Console.ReadLine());

            listContas[numeroConta].Sacar(valorSaque);
        }

        private void Transferir()
        {
            Console.Write("Digite o número da conta de origem: ");
            int numeroContaOrigem = int.Parse(Console.ReadLine());

            if(!VerificaExistenciaConta(numeroContaOrigem))
            {
                ExibeMensagemContaInvalida(numeroContaOrigem);
                return;
            }

            Console.Write("Digite o número da conta de destino: ");
            int numeroContaDestino = int.Parse(Console.ReadLine());

            if(!VerificaExistenciaConta(numeroContaDestino))
            {
                ExibeMensagemContaInvalida(numeroContaDestino);
                return;
            }

            Console.Write("Digite o valor a ser transferido: ");
            double valorTransferencia = double.Parse(Console.ReadLine());

            listContas[numeroContaOrigem].Transferir(valorTransferencia, listContas[numeroContaDestino]);

        }
        private void Depositar()
        {
            Console.Write("Digite o número da conta: ");
            int numeroConta = int.Parse(Console.ReadLine());

            if(numeroConta < 0 || numeroConta >= listContas.Count)
            {
                Console.WriteLine($"Conta #{numeroConta} inexistente.");
                return;
            }

            Console.Write("Digite o valor a ser depositado: ");
            double valorDeposito = double.Parse(Console.ReadLine());

            listContas[numeroConta].Depositar(valorDeposito);
        }

        private void LimparTela()
        {
            Console.Clear();
        }

        private void Sair()
        {
            Console.WriteLine("Obrigado por utilizar os nossos serviços.\n\n");
        }

        private void TratarOpcaoInvalida()
        {
            Console.WriteLine("Selecione uma opção válida.");
        }

        private bool VerificaExistenciaConta(int numeroConta)
        {
            return listContas.Count > 0 && numeroConta >= 0 && numeroConta < listContas.Count;
        }

        private void ExibeMensagemContaInvalida(int numeroConta)
        {
            Console.WriteLine($"Conta #{numeroConta} inexistente.");
        }
    }
}