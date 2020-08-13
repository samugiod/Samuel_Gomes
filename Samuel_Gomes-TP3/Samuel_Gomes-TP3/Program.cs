using PesssoaLibrary;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;

namespace TP3
{
    class Program
    {
        static PessoaBiblioteca bibliotecaPessoa = new PessoaBiblioteca();

        static void Main(string[] args)
        {            
            CultureInfo cultureInfo = new CultureInfo("pt-BR");
            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;
            bibliotecaPessoa.Deserialize();
            ListarAniversariantes();
            Menu();
            bibliotecaPessoa.Serialize();
            Console.ReadKey();
        }

        public static void Menu()
        {
            bool menu = true;
            string opcaoMenu;
            do
            {                
                Console.WriteLine("\nGerenciador de aniversários" 
                    + "\n1 - Adicionar usuário"
                    + "\n2 - Buscar usuário"
                    + "\n3 - Deletar usuário"
                    + "\n4 - Editar usuário"
                    + "\n5 - Sair"
                    + "\nEscolha uma das opções acima:");
             
                opcaoMenu = Console.ReadLine();
                switch (opcaoMenu)
                {
                    case "1":
                        CriarUsuario();
                        Thread.Sleep(2500);
                        Console.Clear();
                        break;
                    case "2":
                        ExibirUsuario();
                        break;
                    case "3":
                        ExcluirUsuario();
                        Thread.Sleep(2500);
                        Console.Clear();
                        break;
                    case "4":
                        EditarUsuario();
                        Thread.Sleep(2500);
                        Console.Clear();
                        break;
                    case "5":
                        menu = false;
                        Console.WriteLine("Aperte qualquer tecla para sair...");
                        break;
                    default:
                        break;
                }
            } while (menu);
        }

        public static void CriarUsuario()
        {
            string nome, sobrenome;
            DateTime dataNascimento;

            Console.WriteLine("Digite seu nome:");
            nome = Console.ReadLine().ToUpper();

            Console.WriteLine("Digite seu sobrenome:");
            sobrenome = Console.ReadLine().ToUpper();

            do
                Console.WriteLine("Digite sua data de nascimento: (dd/mm/aaaa)");
            while (!DateTime.TryParse(Console.ReadLine(), out dataNascimento));
            Console.WriteLine($"\nNome:{nome} {sobrenome}" +
                $"\nData de nascimento: {dataNascimento.ToString("d")}" +
                $"\nSeus dados estão corretos? (S / N)");
            string resposta = Console.ReadLine().ToUpper();

            if (resposta == "S")
            {
                Pessoa pessoa = new Pessoa()
                {
                    Nome = nome,
                    Sobrenome = sobrenome,
                    DataDeNascimento = dataNascimento
                };
                bibliotecaPessoa.CriarUsuario(pessoa);
                Console.WriteLine("Usuário cadastrado com sucesso!" +
                    "\n\tAguarde...");
                bibliotecaPessoa.Serialize();
            }
            else
                Console.WriteLine("Usuário não cadastrado!"
                    + "\nTente novamente!");
        }

        public static int BuscarUsuario()
        {
            List<Pessoa> usuarios;
            int id = -1;
            Console.WriteLine("Digite o nome de usuário que deseja buscar:");
            string nome = Console.ReadLine().ToUpper();
            int index = 0;
            int escolha;
            usuarios = bibliotecaPessoa.BuscarUsuario(nome);

            if (!usuarios.Any())
                Console.WriteLine("Usuário não encontrado");
            else
            {
                foreach (var pessoa in usuarios)
                {
                    Console.WriteLine($"{index} - {pessoa.Nome} {pessoa.Sobrenome} {pessoa.DataDeNascimento.ToString("d")}");
                    index++;
                }
                do
                    Console.WriteLine("Escolha um usuário:");
                while (!int.TryParse(Console.ReadLine(), out escolha));
                if (escolha < usuarios.Count)
                {
                    id = usuarios[escolha].Id;
                }
                else
                    Console.WriteLine("Opção inválida!");
            }
            return id;
        }

        public static void ExcluirUsuario()
        {
            var id = BuscarUsuario();
            if (id > -1)
            {
                Console.WriteLine("Tem certeza que deseja excluir o usuário? (S / N)");
                string resposta = Console.ReadLine().ToUpper();
                if(resposta == "S")
                {
                    bibliotecaPessoa.ExcluirUsuario(id);
                    Console.WriteLine("O usuário foi excluido!" +
                        "\n\tAguarde...");
                    bibliotecaPessoa.Serialize();
                }
                else
                {
                    Console.WriteLine("O usuário não foi excluido.");
                }
            }
        }

        public static void EditarUsuario()
        {
            int id = BuscarUsuario();
            if(id > -1)
            {
                string nomeUsuario, sobrenome;
                DateTime dataNascimento;

                Console.WriteLine("Digite seu nome:");
                nomeUsuario = Console.ReadLine().ToUpper();

                Console.WriteLine("Digite seu sobrenome:");
                sobrenome = Console.ReadLine().ToUpper();

                do
                    Console.WriteLine("Digite sua data de nascimento: (dd/mm/aaaa)");
                while (!DateTime.TryParse(Console.ReadLine(), out dataNascimento));

                Console.WriteLine($"\nNome:{nomeUsuario} {sobrenome}" +
                 $"\nData de nascimento: {dataNascimento.ToString("d")}" +
                 $"\nSeus dados estão corretos? (S / N)");
                string resposta = Console.ReadLine().ToUpper();
                if (resposta == "S")
                {
                    bibliotecaPessoa.EditarUsuario(id, nomeUsuario, sobrenome, dataNascimento);
                    Console.WriteLine("Usuário Editado com sucesso!" +
                        "\n\tAguarde...");
                    bibliotecaPessoa.Serialize();
                }
                else
                {
                    Console.WriteLine("O usuário não foi editado.");
                };                
            }            
        }

        public static void ExibirUsuario()
        {
            int id = BuscarUsuario();
            if(id > -1)
            {
                Pessoa a  = bibliotecaPessoa.ObterPorId(id);
                Console.WriteLine(a.ExibirDadosCadastrados());
            }
        }
        public static void ListarAniversariantes()
        {
            List<Pessoa> aniversariantes = bibliotecaPessoa.ExibirAniversariantesDia();
            foreach (var item in aniversariantes)
            {
                Console.WriteLine(item.ExibirFelicitacoes());
            }
        }        
    }
}