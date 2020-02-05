using System;
using RoleTopMVC.ViewModels;
using RoleTopMVC.Repositories;
using RoleTopMVC.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RoleTopMVC.Controllers
{
    public class LoginController : AbstractController {   
        private ClienteRepository clienteRepository = new ClienteRepository();


        [HttpGet]
        public IActionResult Index() {
            return View(new BaseViewModel()
            {
                NomeView = "Login",
                UsuarioEmail = ObterUsuarioSession(),
                UsuarioNome = ObterUsuarioNomeSession()
            });
        }

        [HttpPost]
        public IActionResult Index(IFormCollection form) {
            ViewData["Action"] = "Index";
            
            try{
                System.Console.WriteLine("==================");
                System.Console.WriteLine(form["email"]);
                System.Console.WriteLine(form["senha"]);
                System.Console.WriteLine("==================");

                var usuario = form["email"];
                var senha = form["senha"];

                var cliente = clienteRepository.ObterPor(usuario);

                if(cliente != null){
                    if(cliente.Senha.Equals(senha)){
                        switch (cliente.TipoUsuario){

                            case (uint) TiposUsuario.CLIENTE:
                                HttpContext.Session.SetString(SESSION_CLIENTE_EMAIL, cliente.Email);
                                HttpContext.Session.SetString(SESSION_CLIENTE_NOME, cliente.Nome);
                                HttpContext.Session.SetString(SESSION_TIPO_USUARIO, cliente.TipoUsuario.ToString());
                            return RedirectToAction ("Index", "DashboardUsuario", new BaseViewModel(){
                                NomeView = "DashboardUsuario",
                                UsuarioEmail = ObterUsuarioSession(),
                                UsuarioNome = ObterUsuarioNomeSession() });
                            
                            default:
                                HttpContext.Session.SetString(SESSION_CLIENTE_EMAIL, cliente.Email);
                                HttpContext.Session.SetString(SESSION_CLIENTE_NOME, cliente.Nome);
                                HttpContext.Session.SetString(SESSION_TIPO_USUARIO, cliente.TipoUsuario.ToString());
                            return RedirectToAction ("Index", "DashboardAdministrador", new BaseViewModel(){
                                NomeView = "DashboardAdministrador",
                                UsuarioEmail = ObterUsuarioSession(),
                                UsuarioNome = ObterUsuarioNomeSession() });
                        }
                    } else {
                        return View("Erro", new RespostaViewModel("SENHA INCORRETA"){
                            NomeView = "Login"});
                        } 
                } else {
                    return View("Erro", new RespostaViewModel("USUÁRIO NÃO ENCONTRADO"){
                        NomeView = "Login"});
                    }  

            } catch (Exception e){
                System.Console.WriteLine(e.StackTrace);
                return View("Erro", new RespostaViewModel("OPS! ALGO DEU ERRADO!"){
                    NomeView = "Login"});
            }
        }

        public IActionResult Logoff() {
            HttpContext.Session.Remove(SESSION_CLIENTE_EMAIL);
            HttpContext.Session.Remove(SESSION_CLIENTE_NOME);
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}