using System;
using System.Collections.Generic;

public class Program
{
    public static void Main()
    {
        // Chama a função MakeChange com n = 12 e armazena o resultado em 'results'
        var results = MakeChange(12);

        // Itera sobre cada resultado e imprime no console
        foreach (var result in results)
        {
            Console.WriteLine($"[{result[0]}, {result[1]}, {result[2]}, {result[3]}]");
        }
    }

    public static HashSet<int[]> MakeChange(int n)
    {
        // Cria um HashSet para armazenar os resultados. Cada resultado é um array de inteiros.
        HashSet<int[]> results = new HashSet<int[]>(new IntArrayEqualityComparer());

        // Três loops aninhados para iterar sobre todas as possíveis combinações de quarters, dimes e nickels
        for (int quarters = 0; quarters * 25 <= n; quarters++)
        {
            for (int dimes = 0; dimes * 10 <= n - quarters * 25; dimes++)
            {
                for (int nickels = 0; nickels * 5 <= n - quarters * 25 - dimes * 10; nickels++)
                {
                    // Calcula o número de pennies para a combinação atual de quarters, dimes e nickels
                    int pennies = n - quarters * 25 - dimes * 10 - nickels * 5;

                    // Adiciona a combinação atual ao conjunto de resultados
                    results.Add(new int[] { quarters, dimes, nickels, pennies });
                }
            }
        }

        // Retorna o conjunto de resultados
        return results;
    }
}

// Classe para comparar arrays de inteiros. Necessária para que o HashSet possa determinar a igualdade entre os arrays.
public class IntArrayEqualityComparer : IEqualityComparer<int[]>
{
    public bool Equals(int[] x, int[] y)
    {
        // Se os arrays têm tamanhos diferentes, eles não são iguais
        if (x.Length != y.Length)
        {
            return false;
        }

        // Compara cada elemento dos arrays. Se algum elemento não é igual, os arrays não são iguais.
        for (int i = 0; i < x.Length; i++)
        {
            if (x[i] != y[i])
            {
                return false;
            }
        }

        // Se chegou até aqui, todos os elementos são iguais e os arrays são iguais.
        return true;
    }

    public int GetHashCode(int[] obj)
    {
        // Calcula um hash code para o array. Isso é necessário para que o HashSet possa armazenar os arrays de forma eficiente.
        int result = 17;
        for (int i = 0; i < obj.Length; i++)
        {
            unchecked
            {
                result = result * 31 + obj[i];
            }
        }
        return result;
    }
}