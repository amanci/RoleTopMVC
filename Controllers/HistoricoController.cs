using Microsoft.AspNetCore.Mvc;
using RoleTopMVC.Repositories;
using RoleTopMVC.ViewModels;

namespace RoleTopMVC.Controllers
{
    public class HistoricoController: AbstractController
    {   
        private AgendamentoRepository agendamentoRepository = new AgendamentoRepository();

        public IActionResult Index(){

    
            var emailCliente = ObterUsuarioSession();
            var agendamentoCliente = agendamentoRepository.ObterTodosPorCliente(emailCliente);

            return View(new HistoricoViewModel()
            {
                Agendamentos = agendamentoCliente,
                NomeView = "Historico",
                UsuarioEmail = ObterUsuarioSession(),
                UsuarioNome = ObterUsuarioNomeSession()
            });
        }
    }
}