using GeradorTestes.Infra.Configs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GeradorTeste.WinApp.ModuloConfiguracao
{
    public partial class ConfiguracaoControl : UserControl
    {
        public ConfiguracaoControl(ConfiguracaoAplicacaoGeradorTeste configuracao)
        {
            InitializeComponent();

            txtConnectionString.Text = configuracao.ConnectionStrings.SqlServer;
            txtDiretorioLogs.Text = configuracao.ConfiguracaoLogs.DiretorioSaida;
        }
    }
}
