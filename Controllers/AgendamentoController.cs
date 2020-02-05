using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RoleTopMVC.Enums;
using RoleTopMVC.Models;
using RoleTopMVC.Repositories;
using RoleTopMVC.ViewModels;

namespace RoleTopMVC.Controllers
{
    public class AgendamentoController : AbstractController
    {
        AgendamentoRepository agendamentoRepository = new AgendamentoRepository ();
        ClienteRepository clienteRepository = new ClienteRepository();
        public IActionResult Index(){
            AgendamentoViewModel avm = new AgendamentoViewModel();
            

            var usuarioLogado = ObterUsuarioSession();
            var nomeUsuarioLogado = ObterUsuarioNomeSession();
            if (!string.IsNullOrEmpty(nomeUsuarioLogado))
            {
                avm.NomeUsuario = nomeUsuarioLogado;
            }
            
            var clienteLogado = clienteRepository.ObterPor(usuarioLogado);
            if (clienteLogado != null)
            {
                avm.Cliente = clienteLogado;
            }

            avm.NomeView = "Agendamento";
            avm.UsuarioEmail = usuarioLogado;
            avm.UsuarioNome = nomeUsuarioLogado;
            
            return View (avm);
        }


        public IActionResult Agendar (IFormCollection form) {
            ViewData["Action"] = "Agendamento";

            Agendamento agendamento = new Agendamento ();
            
            Cliente cliente = new Cliente () {
                Nome = form["nome"],
                CpfCnpj = form["cpf-cnpj"],
                Telefone = form["telefone"],
                Endereco = form["endereco"],
                Email = form["email"]
            };

            Som som = new Som (){
                ServicoSelecionado = bool.Parse(form["som"])

            };

            Iluminacao iluminacao = new Iluminacao(){
                ServicoSelecionado = bool.Parse(form["iluminacao"])
            };

            Evento evento = new Evento () {
                NomeEvento = form["nome-evento"],
                DescricaoEvento = form["descricao"],
                Iluminacao = iluminacao,
                Som = som
            };
            
            agendamento.Cliente = cliente;
            agendamento.Evento = evento;
            agendamento.DataAgendamento = DateTime.Parse(form["data-evento"]);

            if (agendamentoRepository.Inserir (agendamento)) {
                return View ("Sucesso", new RespostaViewModel("AGENDAMENTO REALIZADO")
                {
                    NomeView = "Agendamento",
                    UsuarioEmail = ObterUsuarioSession(),
                    UsuarioNome = ObterUsuarioNomeSession() 
                });
            } else {
                return View ("Erro", new RespostaViewModel("OPS! ERRO NO AGENDAMENTO :)")
                {
                    NomeView = "Agendamento",
                    UsuarioEmail = ObterUsuarioSession(),
                    UsuarioNome = ObterUsuarioNomeSession()  
                });
            }
        }


        public IActionResult Aprovar(ulong id)
        {
            Agendamento agendamento = agendamentoRepository.ObterPor(id);
            agendamento.Status = (uint) StatusPedido.APROVADO;

            if(agendamentoRepository.Atualizar(agendamento))
            {
                return RedirectToAction("Index", "DashboardAdministrador");
            }
            else {
                return View("Erro", new RespostaViewModel()
                {
                    Mensagem = "HOUVE UM ERRO AO APROVAR O AGENDAMENTO.",
                    NomeView = "DashboardAdministrador",
                    UsuarioEmail = ObterUsuarioSession(),
                    UsuarioNome = ObterUsuarioNomeSession()
                });
            }
        }



        public IActionResult Reprovar(ulong id)
        {
            Agendamento agendamento = agendamentoRepository.ObterPor(id);
            agendamento.Status = (uint) StatusPedido.REPROVADO;

            if(agendamentoRepository.Atualizar(agendamento))
            {
                return RedirectToAction("Index", "DashboardAdministrador");
            }
            else {
                return View("Erro", new RespostaViewModel()
                {
                    Mensagem = "HOUVE UM ERRO AO REPROVAR O AGENDAMENTO.",
                    NomeView = "DashboardAdministrador",
                    UsuarioEmail = ObterUsuarioSession(),
                    UsuarioNome = ObterUsuarioNomeSession()
                });
            }
        }
    }
}