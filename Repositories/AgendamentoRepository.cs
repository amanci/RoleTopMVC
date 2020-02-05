using System;
using System.Collections.Generic;
using System.IO;
using RoleTopMVC.Models;

namespace RoleTopMVC.Repositories {
    public class AgendamentoRepository : RepositoryBase {
        private const string PATH = "Database/Agendamento.csv";
        public AgendamentoRepository () {
            if (!File.Exists (PATH)) {
                File.Create (PATH).Close ();
            }

        }
        public bool Inserir (Agendamento agendamento) {
            var idAgendamentos = File.ReadAllLines(PATH).Length;
            agendamento.Id = (ulong) ++idAgendamentos;
            var linha = new string[] { PrepararPedidoCSV (agendamento) };
            File.AppendAllLines (PATH, linha);

            return true;
        }

        public List<Agendamento> ObterTodosPorCliente(string emailCliente)
        {
            var agendamentos = ObterTodos();
            List<Agendamento> agendamentosCliente = new List<Agendamento>();

            foreach (var agendamento in agendamentos)
            {
                if(agendamento.Cliente.Email.Equals(emailCliente))
                {
                    agendamentosCliente.Add(agendamento);
                }
            }
            return agendamentosCliente;
        }

        public Agendamento ObterPor(ulong id)
            {
                var pedidosTotais = ObterTodos();
                foreach (var pedido in pedidosTotais)
                {
                    if(pedido.Id == id)
                    {
                        return pedido;
                    }
                }
                return null;
            }

        public List<Agendamento> ObterTodos () {
            var linhas = File.ReadAllLines (PATH);
            List<Agendamento> agendamentos = new List<Agendamento>();

            foreach (var linha in linhas) {
                Agendamento agendamento = new Agendamento ();

                agendamento.Id = ulong.Parse(ExtrairValorDoCampo("id", linha));
                agendamento.Status = uint.Parse(ExtrairValorDoCampo("status_agendamento", linha));
                agendamento.Cliente.Nome = ExtrairValorDoCampo("cliente_nome", linha);
                agendamento.Cliente.CpfCnpj = ExtrairValorDoCampo("cliente_cpf-cnpj", linha);
                agendamento.Cliente.Telefone = ExtrairValorDoCampo("cliente_telefone", linha);
                agendamento.Cliente.Endereco = ExtrairValorDoCampo("cliente_endereco",linha);
                agendamento.Cliente.Email = ExtrairValorDoCampo("cliente_email", linha);
                agendamento.Evento.NomeEvento = ExtrairValorDoCampo("nome_evento", linha);
                agendamento.DataAgendamento = DateTime.Parse(ExtrairValorDoCampo("data_agendamento", linha));
                agendamento.Evento.Iluminacao.ServicoSelecionado = bool.Parse(ExtrairValorDoCampo("servico_iluminacao", linha));
                agendamento.Evento.Som.ServicoSelecionado = bool.Parse(ExtrairValorDoCampo("servico_som", linha));
                agendamento.Evento.DescricaoEvento = ExtrairValorDoCampo("descricao_evento", linha);

                agendamentos.Add(agendamento);
            }
            return agendamentos;
        } 

        public bool Atualizar(Agendamento agendamento)
        {
            var agendamentosTotais = File.ReadAllLines(PATH);
            var agendamentoCSV = PrepararPedidoCSV(agendamento);
            var linhaAgendamento = -1;
            var resultado = false;

            for (int i = 0; i < agendamentosTotais.Length; i++)
            {
                var idConvertido = ulong.Parse(ExtrairValorDoCampo("id",agendamentosTotais[i]));
                if (agendamento.Id.Equals(idConvertido))
                {
                    linhaAgendamento = i;
                    resultado = true;
                    break;
                }
            }

            if(resultado){
                agendamentosTotais[linhaAgendamento] = agendamentoCSV;
                File.WriteAllLines(PATH, agendamentosTotais);
            }

            return resultado;
        }

        private string PrepararPedidoCSV (Agendamento agendamento) {
            Cliente c = agendamento.Cliente;
            Evento e = agendamento.Evento;

            return $"id={agendamento.Id};status_agendamento={agendamento.Status};cliente_nome={c.Nome};cliente_cpf-cnpj={c.CpfCnpj};cliente_telefone={c.Telefone};cliente_endereco={c.Endereco};cliente_email={c.Email};nome_evento={e.NomeEvento};data_agendamento={agendamento.DataAgendamento};servico_iluminacao={e.Iluminacao.ServicoSelecionado};servico_som={e.Som.ServicoSelecionado};descricao_evento={e.DescricaoEvento};";
        }
    }
}