using GeradorTestes.Infra.Configs;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace GeradorTestes.Infra.Orm.Compartilhado
{
    public static class MigradorBancoDadosGeradorTeste
    {
        public static void AtualizarBancoDados()
        {
            var config = new ConfiguracaoAplicacaoGeradorTeste();

            var db = new GeradorTesteDbContext(config.ConnectionStrings);

            var migracoesPendentes = db.Database.GetPendingMigrations();

            if (migracoesPendentes.Count() > 0)  
                db.Database.Migrate();
        }
    }
}
