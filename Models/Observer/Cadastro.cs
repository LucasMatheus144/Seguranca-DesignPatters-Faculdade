using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EspacoPotencial.Areas.Cadastro.Models.Funcionarios;
using EspacoPotencial.Areas.Cadastro.Models.Usuarios;
using EspacoPotencial.Context;

namespace EspacoPotencial.Models.Observer
{
    public class FuncionarioObserver : ITipo
    {
        private readonly CreateViewModel _viewModel;
        private readonly int _novoGeralId;
        private readonly funcionario _funcionario;
        private readonly ApaDbContext _context;

        public FuncionarioObserver(CreateViewModel viewModel, int novoGeralId, funcionario funcionario, ApaDbContext context)
        {
            _viewModel = viewModel;
            _novoGeralId = novoGeralId;
            _funcionario = funcionario;
            _context = context;
        }

        public async void AtualizarTipo(string tipo)
        {
            if (tipo != "2")
            {
                _funcionario.geral_id = _novoGeralId;
                _funcionario.Rg = _viewModel.FuncionarioModel.Rg;
                _funcionario.Nascimento = _viewModel.FuncionarioModel.Nascimento;
                _funcionario.centrocusto_id = _viewModel.FuncionarioModel.centrocusto_id;
                _funcionario.CestaBasica = _viewModel.FuncionarioModel.CestaBasica;
                _funcionario.banco_id = _viewModel.FuncionarioModel.banco_id;
                _funcionario.Agencia = _viewModel.FuncionarioModel.Agencia;
                _funcionario.Conta = _viewModel.FuncionarioModel.Conta;
                _funcionario.Escolaridade = _viewModel.FuncionarioModel.Escolaridade;
                _funcionario.Formacao = _viewModel.FuncionarioModel.Formacao;

                if (_viewModel.FuncionarioModel.ImagemFile != null)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await _viewModel.FuncionarioModel.ImagemFile.CopyToAsync(memoryStream);
                        _funcionario.Foto = memoryStream.ToArray();
                    }
                }

                _context.Add(_funcionario);
                await _context.SaveChangesAsync();
            }
        }
    }

    public class UsuarioObserver : ITipo
    {
        private readonly CreateViewModel _viewModel;
        private readonly int _novoGeralId;
        private readonly usuario _usuario;
        private readonly ApaDbContext _context;

        public UsuarioObserver(CreateViewModel viewModel, int novoGeralId, usuario usuario, ApaDbContext context)
        {
            _viewModel = viewModel;
            _novoGeralId = novoGeralId;
            _usuario = usuario;
            _context = context;
        }

        public async void AtualizarTipo(string tipo)
        {
            if (tipo == "2")
            {
                _usuario.geral_id = _novoGeralId;
                _usuario.Situacao = _viewModel.UsuarioModel.Situacao;
                _usuario.Foto = _viewModel.UsuarioModel.Foto;
                _usuario.Sus = _viewModel.UsuarioModel.Sus;
                _usuario.Nascimento = _viewModel.UsuarioModel.Nascimento;
                _usuario.Ingresso = _viewModel.UsuarioModel.Ingresso;
                _usuario.DataLaudo = _viewModel.UsuarioModel.DataLaudo;
                _usuario.escolas_id = _viewModel.UsuarioModel.escolas_id;
                _usuario.comorbidade_id = _viewModel.UsuarioModel.comorbidade_id;
                _usuario.beneficio_id = _viewModel.UsuarioModel.beneficio_id;
                _usuario.HistoricoContato = _viewModel.UsuarioModel.HistoricoContato;
                _usuario.desligamento_id = _viewModel.UsuarioModel.desligamento_id;
                _usuario.usuario_descritivo_desligamento = _viewModel.UsuarioModel.usuario_descritivo_desligamento;
                _usuario.Alergia = _viewModel.UsuarioModel.Alergia;
                _usuario.Medicacao = _viewModel.UsuarioModel.Medicacao;
                _usuario.RestricaoAlimentar = _viewModel.UsuarioModel.RestricaoAlimentar;
                _usuario.Transporte = _viewModel.UsuarioModel.Transporte;

                if (_viewModel.UsuarioModel.ImagemFile != null)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await _viewModel.UsuarioModel.ImagemFile.CopyToAsync(memoryStream);
                        _usuario.Foto = memoryStream.ToArray();
                    }
                }

                _context.Add(_usuario);
                await _context.SaveChangesAsync();
            }
        }
    }

}
