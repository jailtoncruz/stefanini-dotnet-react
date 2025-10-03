using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace StefaniniDotNetReactChallenge.Application.Validations
{
    public class CpfAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            if (value == null) return false;
            var cpf = value.ToString()?.Replace(".", "").Replace("-", "").Trim();

            if (string.IsNullOrEmpty(cpf) || cpf.Length != 11 || !Regex.IsMatch(cpf, @"^\d{11}$"))
                return false;

            if (new string(cpf[0], 11) == cpf)
                return false;

            var numbers = cpf.Select(c => int.Parse(c.ToString())).ToArray();

            for (int j = 9; j < 11; j++)
            {
                int sum = 0;
                for (int i = 0; i < j; i++)
                    sum += numbers[i] * ((j + 1) - i);

                int result = (sum * 10) % 11;
                if (result == 10) result = 0;

                if (numbers[j] != result)
                    return false;
            }

            return true;
        }
    }
}
