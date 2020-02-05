using Microsoft.AspNetCore.Mvc;
using RoleTopMVC.ViewModels;

namespace RoleTopMVC.Controllers
{
    public class DashboardUsuarioController : AbstractController
    {
        public IActionResult Index(){


            return View(new BaseViewModel()
            {
                NomeView = "DashboardUsuario",
                UsuarioEmail = ObterUsuarioSession(),
                UsuarioNome = ObterUsuarioNomeSession()
            });
        }
        
    }
}