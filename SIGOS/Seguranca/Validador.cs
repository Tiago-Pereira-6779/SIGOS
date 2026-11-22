using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGOS.Seguranca
{
    public class Validador
    {

        public static string SomenteNumeros (string texto)
        {
            return new string((texto ?? "").Where(char.IsDigit).ToArray());
        }
        public static bool DocumentoValido(string documento)
        {
            string d = SomenteNumeros(documento);
            return d.Length == 11 ? CpfValido(d) : d.Length == 14 && CnpjValido(d);
        }

        public static bool TelefoneValido(string telefone)
        {
            string t = SomenteNumeros(telefone);
            return t.Length == 10 || t.Length == 11;
        }

        private static bool CpfValido(string cpf)
        {
            if (cpf.Distinct().Count() == 1) return false;
            for (int t = 9; t < 11; t++)
            {
                int soma = 0;
                for (int i = 0; i < t; i++)
                    soma += (cpf[i] - '0') * (t + 1 - i);
                int dv = (soma * 10) % 11;
                if (dv == 10) dv = 0;
                if (dv != cpf[t] - '0') return false;
            }
            return true;
        }

        private static bool CnpjValido(string cnpj)
        {
            if (cnpj.Distinct().Count() == 1) return false;
            int[] m1 = { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] m2 = { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

            int soma = 0;
            for (int i = 0; i < 12; i++) soma += (cnpj[i] - '0') * m1[i];
            int resto = soma % 11;
            int dv1 = resto < 2 ? 0 : 11 - resto;

            soma = 0;
            for (int i = 0; i < 13; i++) soma += (cnpj[i] - '0') * m2[i];
            resto = soma % 11;
            int dv2 = resto < 2 ? 0 : 11 - resto;

            return dv1 == cnpj[12] - '0' && dv2 == cnpj[13] - '0';
        }

    }
}
