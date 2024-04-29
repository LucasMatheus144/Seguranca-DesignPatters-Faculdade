using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using EspacoPotencial.Areas.Cadastro.Models;
using EspacoPotencial.Areas.Cadastro.Models.Financeiro;


namespace EspacoPotencial.Context
{
    public class ApaDbContext : IdentityDbContext<IdentityUser>
    {
        public ApaDbContext(DbContextOptions<ApaDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                foreach (var property in entityType.GetProperties())
                {
                    if (property.ClrType == typeof(DateTime))
                    {
                        property.SetColumnType("timestamp");
                    }
                }
            }
        }
        public DbSet<EspacoPotencial.Areas.Cadastro.Models.Public.geral> geral { get; set; }
        public DbSet<EspacoPotencial.Areas.Cadastro.Models.Funcionarios.cid> cid { get; set; }
        public DbSet<EspacoPotencial.Areas.Cadastro.Models.Funcionarios.centro_custo> centro_custo { get; set; }
        public DbSet<EspacoPotencial.Areas.Cadastro.Models.Funcionarios.epi> epi { get; set; }
        public DbSet<EspacoPotencial.Areas.Cadastro.Models.Funcionarios.banco> banco { get; set; }
        public DbSet<EspacoPotencial.Areas.Cadastro.Models.Funcionarios.motivo_desligamento> motivo_desligamento { get; set; }
        public DbSet<EspacoPotencial.Areas.Cadastro.Models.Funcionarios.cargo_funcionario> cargo_funcionario { get; set; }
        public DbSet<EspacoPotencial.Areas.Cadastro.Models.Funcionarios.funcionario> funcionario { get; set; }
        public DbSet<EspacoPotencial.Areas.Cadastro.Models.Funcionarios.epi_funcionario> epi_funcionario { get; set; }
        public DbSet<EspacoPotencial.Areas.Cadastro.Models.Funcionarios.atestado_medico> atestado_medico { get; set; }
        public DbSet<EspacoPotencial.Areas.Cadastro.Models.Funcionarios.cargos> cargos { get; set; }
        public DbSet<EspacoPotencial.Areas.Cadastro.Models.Funcionarios.exame_medico> exame_medico { get; set; }
        public DbSet<EspacoPotencial.Areas.Cadastro.Models.Usuarios.comorbidade> comorbidade { get; set; }
        public DbSet<EspacoPotencial.Areas.Cadastro.Models.Usuarios.motorista> motorista { get; set; }
        public DbSet<EspacoPotencial.Areas.Cadastro.Models.Usuarios.escolas> escolas { get; set; }
        public DbSet<EspacoPotencial.Areas.Cadastro.Models.Usuarios.beneficio> beneficio { get; set; }
        public DbSet<EspacoPotencial.Areas.Cadastro.Models.Usuarios.convenio> convenio { get; set; }
        public DbSet<EspacoPotencial.Areas.Cadastro.Models.Usuarios.sala> sala { get; set; }
        public DbSet<EspacoPotencial.Areas.Cadastro.Models.Usuarios.educador_sala> educador_sala { get; set; }
        public DbSet<EspacoPotencial.Areas.Cadastro.Models.Usuarios.datas> datas { get; set; }
        public DbSet<EspacoPotencial.Areas.Cadastro.Models.Usuarios.calendario> calendario { get; set; }
        public DbSet<EspacoPotencial.Areas.Cadastro.Models.Usuarios.salas_diario> salas_diario { get; set; }
        public DbSet<EspacoPotencial.Areas.Cadastro.Models.Usuarios.usuario> usuario { get; set; }
        public DbSet<EspacoPotencial.Areas.Cadastro.Models.Usuarios.responsavel> responsavel { get; set; }
        public DbSet<EspacoPotencial.Areas.Cadastro.Models.Usuarios.comorbidade_usuario> comorbidade_usuario { get; set; }
        public DbSet<EspacoPotencial.Areas.Cadastro.Models.Usuarios.usuario_motorista> usuario_motorista { get; set; }
        public DbSet<EspacoPotencial.Areas.Cadastro.Models.Usuarios.dias_terapia> dias_terapia { get; set; }
        public DbSet<EspacoPotencial.Areas.Cadastro.Models.Usuarios.usuario_sala> usuario_sala { get; set; }
        public DbSet<EspacoPotencial.Areas.Cadastro.Models.Usuarios.frequencia> frequencia { get; set; }
        public DbSet<EspacoPotencial.Areas.Cadastro.Models.Usuarios.desligamento_motivos_usuario> desligamento_motivos_usuario { get; set; }

        public DbSet<EspacoPotencial.Areas.Cadastro.Models.Financeiro.grupo_permitido> grupo_permitido { get; set; }
        public DbSet<EspacoPotencial.Areas.Cadastro.Models.Financeiro.eventos> eventos { get; set; }
        public DbSet<EspacoPotencial.Areas.Cadastro.Models.Financeiro.produtos> produtos { get; set; }
        public DbSet<EspacoPotencial.Areas.Cadastro.Models.Financeiro.produtos_geral> produtos_geral { get; set; }
        public DbSet<EspacoPotencial.Areas.Cadastro.Models.Financeiro.produtos_evento> produtos_evento { get; set; }
        public DbSet<EspacoPotencial.Areas.Cadastro.Models.Financeiro.movimento_produtos> movimento_produtos { get; set; }
        public DbSet<EspacoPotencial.Areas.Cadastro.Models.Financeiro.movimento_venda> movimento_venda { get; set; }
        public DbSet<EspacoPotencial.Areas.Cadastro.Models.Financeiro.cc> cc { get; set; }
        public DbSet<EspacoPotencial.Areas.Cadastro.Models.Financeiro.cta_receber> cta_receber { get; set; }

        public DbSet<EspacoPotencial.Areas.Cadastro.Models.Financeiro.armazem> armazem { get; set; }

        public DbSet<EspacoPotencial.Areas.Cadastro.Models.Financeiro.tipo> tipo { get; set; }
        public DbSet<EspacoPotencial.Areas.Cadastro.Models.Financeiro.item> item { get; set; }
        public DbSet<EspacoPotencial.Areas.Cadastro.Models.Financeiro.movimentacao> movimentacao { get; set; }

    }
}


