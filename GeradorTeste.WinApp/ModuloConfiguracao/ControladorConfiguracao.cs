using GeradorTestes.Infra.Configs;
using System;
using System.Windows.Forms;

namespace GeradorTeste.WinApp.ModuloConfiguracao
{
    public class ControladorConfiguracao : ControladorBase
    {
        private readonly ConfiguracaoAplicacaoGeradorTeste configuracao;

        public ControladorConfiguracao(ConfiguracaoAplicacaoGeradorTeste configuracao)
        {
            this.configuracao = configuracao;
        }

        public override void Editar()
        {
        }

        public override void Excluir()
        {
        }

        public override void Inserir()
        {
        }

        public override ConfiguracaoToolboxBase ObtemConfiguracaoToolbox()
        {
            return new ConfiguracaoToolboxConfiguracao();
        }

        public override UserControl ObtemListagem()
        {
            return new ConfiguracaoControl(configuracao);
        }
    }
}
