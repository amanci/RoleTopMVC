using RoleTopMVC.Enums;
using RoleTopMVC.Repositories;
using RoleTopMVC.ViewModels;
using RoleTopMVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace RoleTopMVC.Controllers
{
    public class DashboardAdministrador : AbstractController
    {  
        AgendamentoRepository agendamentoRepository = new AgendamentoRepository();
        public IActionResult Index(){

            var tipoUsuarioSessao = uint.Parse(ObterUsuarioTipoSession());

            if (tipoUsuarioSessao.Equals((uint) TiposUsuario.ADMINISTRADOR)){

            var agendamentos = agendamentoRepository.ObterTodos();
            DashboardViewModel dashboardViewModel = new DashboardViewModel();
            
            foreach (var agendamento in agendamentos)
            {
                switch(agendamento.Status)
                {
                    case (uint) StatusPedido.APROVADO:
                        dashboardViewModel.AgendamentosAprovados++;
                    break;
                    case (uint) StatusPedido.REPROVADO:
                        dashboardViewModel.AgendamentosReprovados++;
                    break;
                    default:
                        dashboardViewModel.AgendamentosPendentes++;
                        dashboardViewModel.Agendamentos.Add(agendamento);
                    break;
                }
            }
            dashboardViewModel.NomeView = "DashboardAdministrador";
            dashboardViewModel.UsuarioEmail = ObterUsuarioSession();

            return View(dashboardViewModel);

            } 

            return View("Erro", new RespostaViewModel(){
                NomeView = "DashboardAdministrasor",
                Mensagem = "VOCê NÃO ESTÁ AUTORIZADO" });
        }  
    }
}