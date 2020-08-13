using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PesssoaLibrary
{
    public class PessoaBiblioteca : IPessoa
    {
        private static List<Pessoa> ListaUsuarios = new List<Pessoa>();
        private static string Dir = @"C:\\Temp";
        private static string Arq = "File.json";

        public PessoaBiblioteca()
        {
            if (!Directory.Exists(Dir))
                Directory.CreateDirectory(Dir);
        }
        public int GerarId()
        {
            int id;
            if (!ListaUsuarios.Any())
            {
                id = 1;
            } 
            else
            {
                id = ListaUsuarios[ListaUsuarios.Count - 1].Id + 1;
            }
            return id;
        }

        public void CriarUsuario(Pessoa pessoa)
        {
            if (pessoa != null)
                pessoa.Id = GerarId();
                ListaUsuarios.Add(pessoa);
        }

        public List<Pessoa> BuscarUsuario(string busca)
        {
            List<Pessoa> usuario = new List<Pessoa>();
            foreach (var pessoa in ListaUsuarios)
            {
                if (pessoa.Nome.Contains(busca) || pessoa.Sobrenome.Contains(busca))
                    usuario.Add(pessoa);
            }
            return usuario;
        }        

        public void ExcluirUsuario(int Id)
        {
            foreach (var usuario in ListaUsuarios)
            {
                if (Id == usuario.Id)
                {
                    ListaUsuarios.Remove(usuario);
                    break;
                }
            }
        }

        public void EditarUsuario(int Id, string Nome, string Sobrenome, DateTime DataNascimento)
        {
            foreach (var usuario in ListaUsuarios)
            {
                if (Id == usuario.Id)
                {
                    usuario.Nome = Nome;
                    usuario.Sobrenome = Sobrenome;
                    usuario.DataDeNascimento = DataNascimento;
                }
            }
        }

        public List<Pessoa> ExibirAniversariantesDia()
        {
            DateTime dataHoje = DateTime.Today;
            List<Pessoa> aniversariantesList = new List<Pessoa>();

            if (ListaUsuarios != null)
            {
                foreach (var aniversariantes in ListaUsuarios)
                {
                    if (aniversariantes.DataDeNascimento.Day == dataHoje.Day & aniversariantes.DataDeNascimento.Month == dataHoje.Month)
                    {
                        aniversariantesList.Add(aniversariantes);
                    }
                }
            }
            return aniversariantesList;            
        }

        public Pessoa ObterPorId (int id)
        {
            foreach (var item in ListaUsuarios)
            {
                if(item.Id == id)
                {
                    return item;
                }
            }
            return null;
        }

        public void Serialize()
        {
            var pathJson = Path.Combine(Dir, Arq);

            JsonManager<List<Pessoa>> jsonManager = new JsonManager<List<Pessoa>>(pathJson);

            jsonManager.Serializer(ListaUsuarios);

        }

        public void Deserialize()
        {
            var pathJson = Path.Combine(Dir, Arq);

            JsonManager<List<Pessoa>> jsonManager = new JsonManager<List<Pessoa>>(pathJson);
            List<Pessoa> listaUsuarios = new List<Pessoa>();
            listaUsuarios = jsonManager.Deserializer();

            if (listaUsuarios != null)
            {
                foreach (var usuario in listaUsuarios)
                {
                    ListaUsuarios.Add(usuario);
                }
            }
        }
    }
}