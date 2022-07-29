using GeradorTestes.Infra.Configs;
using Microsoft.EntityFrameworkCore.Design;

namespace GeradorTestes.Infra.Orm.Compartilhado
{
    public class GeradorTesteDbContextFactory : IDesignTimeDbContextFactory<GeradorTesteDbContext>
    {
        public GeradorTesteDbContext CreateDbContext(string[] args)
        {
            var config = new ConfiguracaoAplicacaoGeradorTeste();

            return new GeradorTesteDbContext(config.ConnectionStrings);
        }
    }
}
