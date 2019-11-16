using System;
using System.IO;

namespace my_first_calculator
{
    internal class GeneratorPs
    {
        private static void Main()
        {
            Console.WriteLine("Insert max number");
            int maxNumber = int.TryParse(Console.ReadLine(), out var number) ? number + 1 : 51;
            string output = $"Write-Host \"Welcome to this calculator!\"{Environment.NewLine}" +
                            $"Write-Host \"It can add, subtract, multiply and divide whole numbers from 0 to {maxNumber}\"{Environment.NewLine}" +
                            $"[double]$num1 = Read-Host -Prompt \"Please choose your first number\"{Environment.NewLine}" +
                            $"$sign = Read-Host - Prompt \"What do you want to do? +, -, /, or *\"{Environment.NewLine}" +
                            $"[double]$num2 = Read-Host - Prompt \"Please choose your second number\"{Environment.NewLine}";

            foreach (var op in new string[] { "+", "-", "*", "/" })
            {
                int firstNumber = 0;
                int secondNumber = 0;
                while (firstNumber < maxNumber)
                {
                    secondNumber = 0;
                    while (secondNumber < maxNumber)
                    {
                        var answer = op switch
                        {
                            "+" => $"{firstNumber + secondNumber}",
                            "-" => $"{firstNumber - secondNumber}",
                            "*" => $"{firstNumber * secondNumber}",
                            "/" => (secondNumber == 0) ? "NaN" : $"{firstNumber / (double)secondNumber}"
                        };
                        var finalEquation = $"if ($num1 -eq {firstNumber} -and $num2 -eq {secondNumber} -and $sign -eq \"{op}\") {{{Environment.NewLine}" +
                                            $"     Write-Host \"$($num1) {op} $($num2) = {answer}\"{Environment.NewLine}" +
                                            $"}}{Environment.NewLine}";
                        output = string.Concat(output, finalEquation);
                        secondNumber++;
                    }
                    firstNumber++;
                }
            }
            output = string.Concat(output, $"Write-Host \"Thanks for using this calculator, goodbye:)\"{Environment.NewLine}");
            File.WriteAllText("my_first_calculator.ps1", output);
        }
    }
}