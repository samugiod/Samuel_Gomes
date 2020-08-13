using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PesssoaLibrary
{
    interface IPessoa
    {
        void CriarUsuario(Pessoa pessoa);
        List<Pessoa> BuscarUsuario(string busca);
        void ExcluirUsuario(int Id);
        void EditarUsuario(int Id, string Nome, string Sobrenome, DateTime DataNascimento);
    }
}
