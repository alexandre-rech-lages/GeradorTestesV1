namespace GeradorTeste.WinApp.ModuloConfiguracao
{
    public class ConfiguracaoToolboxConfiguracao : ConfiguracaoToolboxBase
    {
        public override string TipoCadastro => "Configurações";

        public override string TooltipInserir => "";

        public override string TooltipEditar => "";

        public override string TooltipExcluir => "";

        public override bool InserirHabilitado => false;

        public override bool EditarHabilitado => false;

        public override bool ExcluirHabilitado => false;
    }
}