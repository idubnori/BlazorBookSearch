using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OldBookSearch
{
    public class IsbnValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var str = value as string;
            if (str.Length != 13)
            {
                return new ValidationResult("13桁の数値を入力してください。", new[] { validationContext.MemberName });
            }
            if (!System.Text.RegularExpressions.Regex.IsMatch(str, @"^[0-9]+$"))
            {
                return new ValidationResult("13桁の数値を入力してください。", new[] { validationContext.MemberName });
            }
            // calc check sum
            var checkedNum = str.Remove(str.Length - 1).Aggregate(new { Sum = 0, IsOdd = true }, (total, next) =>
               {
                   var num = next - '0';
                   var sum = total.Sum + (num * (total.IsOdd ? 1 : 3));

                   return new { Sum = sum, IsOdd = !total.IsOdd };
               });
            var expected = 0;
            if (checkedNum.Sum != 0)
            {
                var mod = checkedNum.Sum % 10;
                // Console.WriteLine($"mod:{mod}, sum:{checkedNum.Sum}");
                if (mod != 0)
                {
                    expected = 10 - mod;
                }
                else
                {
                    expected = 0;
                }
            }

            var actual = (str.Skip(str.Length - 1).ToArray()[0]) - '0';
            if (actual != expected)
            {
                return new ValidationResult($"チェックサムが異常です。expected:{expected},actual:{actual}", new[] { validationContext.MemberName });
            }

            return ValidationResult.Success;
        }
    }
}
