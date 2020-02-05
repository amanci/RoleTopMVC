using System;

namespace RoleTopMVC.Models
{
    public class Cliente
    {
        public string Nome {get;set;}
        public string CpfCnpj {get;set;}
        public string Endereco {get;set;}
        public string Telefone {get;set;}
        public string Senha {get;set;}
        public string Email {get;set;}
        public uint TipoUsuario {get;set;}

        public Cliente(){
        }

        public Cliente(string nome, string cpfCnpj, string telefone, string endereco, string email, string senha)
        {
            this.Nome = nome;
            this.CpfCnpj = cpfCnpj;
            this.Telefone = telefone;
            this.Endereco = endereco;
            this.Email = email;
            this.Senha = senha;
        }
    }
}