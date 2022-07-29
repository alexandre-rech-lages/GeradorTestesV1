using Autofac;
using GeradorTeste.WinApp.ModuloConfiguracao;
using GeradorTeste.WinApp.ModuloDisciplina;
using GeradorTeste.WinApp.ModuloMateria;
using GeradorTeste.WinApp.ModuloQuestao;
using GeradorTeste.WinApp.ModuloTeste;
using GeradorTestes.Aplicacao.ModuloDisciplina;
using GeradorTestes.Aplicacao.ModuloMateria;
using GeradorTestes.Aplicacao.ModuloQuestao;
using GeradorTestes.Aplicacao.ModuloTeste;
using GeradorTestes.Dominio;
using GeradorTestes.Dominio.ModuloDisciplina;
using GeradorTestes.Dominio.ModuloMateria;
using GeradorTestes.Dominio.ModuloQuestao;
using GeradorTestes.Dominio.ModuloTeste;
using GeradorTestes.Infra.Configs;
using GeradorTestes.Infra.Orm;
using GeradorTestes.Infra.Orm.ModuloDisciplina;
using GeradorTestes.Infra.Orm.ModuloMateria;
using GeradorTestes.Infra.Orm.ModuloQuestao;
using GeradorTestes.Infra.Orm.ModutoTeste;

namespace GeradorTeste.WinApp.Compartilhado.Ioc
{
    public class ServiceLocatorAutofac : IServiceLocator
    {
        private readonly IContainer container;

        public ServiceLocatorAutofac()
        {
            var builder = new ContainerBuilder();

            builder.Register((x) => new ConfiguracaoAplicacaoGeradorTeste().ConnectionStrings)
                .As<ConnectionStrings>()
                .SingleInstance(); //Singleton

            builder.RegisterType<ConfiguracaoAplicacaoGeradorTeste>()
                .SingleInstance(); //Singleton

            builder.RegisterType<GeradorTesteDbContext>().As<IContextoPersistencia>()
                .InstancePerLifetimeScope(); //Scoped

            builder.RegisterType<RepositorioDisciplinaOrm>().As<IRepositorioDisciplina>();
            builder.RegisterType<ServicoDisciplina>();
            builder.RegisterType<ControladorDisciplina>(); //Transient

            builder.RegisterType<RepositorioMateriaOrm>().As<IRepositorioMateria>();
            builder.RegisterType<ServicoMateria>();
            builder.RegisterType<ControladorMateria>();

            builder.RegisterType<RepositorioQuestaoOrm>().As<IRepositorioQuestao>();
            builder.RegisterType<ServicoQuestao>();
            builder.RegisterType<ControladorQuestao>();

            builder.RegisterType<RepositorioTesteOrm>().As<IRepositorioTeste>();
            builder.RegisterType<ServicoTeste>();
            builder.RegisterType<ControladorTeste>();

            builder.RegisterType<ControladorConfiguracao>();

            container = builder.Build();
        }

        public T Get<T>() where T : ControladorBase
        {
            return container.Resolve<T>();
        }
    }
}
