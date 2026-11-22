namespace SIGOS.Classes
{
    public static class FluxoStatus
    {
        public const string Aberta = "ABERTA";
        public const string Diagnostico = "DIAGNOSTICO";
        public const string AguardandoAprovacao = "AGUARDANDO_APROVACAO";
        public const string EmExecucao = "EM_EXECUCAO";
        public const string Concluida = "CONCLUIDA";
        public const string Cancelada = "CANCELADA";

        // Próximo status do fluxo normal (null se não houver)
        public static string Proximo(string atual)
        {
            switch (atual)
            {
                case Aberta: return Diagnostico;
                case Diagnostico: return AguardandoAprovacao;
                case AguardandoAprovacao: return EmExecucao;
                case EmExecucao: return Concluida;
                default: return null;
            }
        }

        public static bool Finalizada(string status)
        {
            return status == Concluida || status == Cancelada;
        }

        // Regra: finalizada não muda mais; cancelar vale de qualquer status aberto;
        // fora isso, só o próximo status do fluxo.
        public static bool TransicaoValida(string atual, string novo)
        {
            if (Finalizada(atual)) return false;
            if (novo == Cancelada) return true;
            return Proximo(atual) == novo;
        }

        public static string Descricao(string status)
        {
            switch (status)
            {
                case Aberta: return "Aberta";
                case Diagnostico: return "Em diagnóstico";
                case AguardandoAprovacao: return "Aguardando aprovação";
                case EmExecucao: return "Em execução";
                case Concluida: return "Concluída";
                case Cancelada: return "Cancelada";
                default: return status ?? "";
            }
        }
    }
}