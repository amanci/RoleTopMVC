using System.Collections.Generic;
using RoleTopMVC.Models;

namespace RoleTopMVC.ViewModels
{
    public class AgendamentoViewModel : BaseViewModel
    {
        public List<Agendamento> Agendamentos {get;set;}
        public string NomeUsuario {get;set;}
        public Cliente Cliente {get;set;}

        public AgendamentoViewModel()
        {
            this.Agendamentos = new List<Agendamento>();
            this.NomeUsuario = "CLIENTE";
            this.Cliente = new Cliente();
        }

    }
}