using System;

namespace SIGOS.Classes
{
    public class OrdemServico
    {
        public int Id { get; set; }
        public int ClienteId { get; set; }
        public int EquipamentoId { get; set; }
        public int? TecnicoId { get; set; }          // pode não ter técnico ainda
        public string Prioridade { get; set; }
        public string Status { get; set; }
        public string DescricaoProblema { get; set; }
        public string Diagnostico { get; set; }
        public string Solucao { get; set; }
        public decimal Subtotal { get; set; }
        public decimal Desconto { get; set; }
        public decimal Total { get; set; }
        public DateTime DataAbertura { get; set; }
    }
}