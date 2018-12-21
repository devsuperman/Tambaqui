using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Tambaqui.Extensions
{
    public class CPF : ValidationAttribute, IClientModelValidator
    {
        public CPF()
        {
            
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string cpf = System.Convert.ToString(value);

            if (!CpfEhValido(cpf))            
                return new ValidationResult("CPF inválido");            

            return ValidationResult.Success;
        }               

        public void AddValidation(ClientModelValidationContext context)
        {
            if (context == null)            
                throw new ArgumentNullException(nameof(context));            

            MergeAttribute(context.Attributes, "data-val", "true");            
            MergeAttribute(context.Attributes, "data-val-cpfBR", "CPF inválido");
        }

        private void MergeAttribute(IDictionary<string, string> attributes, string key, string value)
        {
            if (!attributes.ContainsKey(key))                        
                attributes.Add(key, value);            
        }

        public static bool CpfEhValido(string cpf)
        {            
            cpf = RemoveNaoNumericos(cpf);
            if (cpf.Length > 11)
                return false;
            while (cpf.Length != 11)
                cpf = '0' + cpf;
            bool igual = true;
            for (int i = 1; i < 11 && igual; i++)
                if (cpf[i] != cpf[0])
                    igual = false;
            if (igual || cpf == "12345678909")
                return false;
            int[] numeros = new int[11];
            for (int i = 0; i < 11; i++)
                numeros[i] = int.Parse(cpf[i].ToString());
            int soma = 0;
            for (int i = 0; i < 9; i++)
                soma += (10 - i) * numeros[i];
            int resultado = soma % 11;
            if (resultado == 1 || resultado == 0)
            {
                if (numeros[9] != 0)
                    return false;
            }
            else if (numeros[9] != 11 - resultado)
                return false;
            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += (11 - i) * numeros[i];
            resultado = soma % 11;
            if (resultado == 1 || resultado == 0)
            {
                if (numeros[10] != 0)
                    return false;
            }
            else
                if (numeros[10] != 11 - resultado)
                    return false;
            return true;
        }
        
        public static string RemoveNaoNumericos(string text)
        {
            System.Text.RegularExpressions.Regex reg = new System.Text.RegularExpressions.Regex(@"[^0-9]");
            string ret = reg.Replace(text, string.Empty);
            return ret;
        }

        
    }
}