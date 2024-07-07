
using System;

namespace GeneticAlgorithm;

public class GlobalVariables
{
    // Variável global
    public static int POPULATION_SIZE = 600;
    public static int GENERATIONS = 1000;
    public static double taxaElitismo = 0.01;
    
}



class Program
{
    private static double[,] GeraMatriz(int size)
    {
        double[,] matriz = new double[size, 5];
        Random rnd = new Random();
        
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                double randomDouble = rnd.NextDouble();
                double adjustedDouble = (randomDouble * 2) - 1;
                double resultDouble = Math.Round(adjustedDouble, 4);
                matriz[i,j] = resultDouble;
            }
        }
        return matriz;
    }

    static void PrintMatrix(double[,] matrix)
    {
        int rows = matrix.GetLength(0);
        int cols = matrix.GetLength(1);

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                Console.Write($"[{matrix[i,j]}]" + "\t"); // "\t" adiciona um tab para melhor formatação
            }
            Console.WriteLine();
        }
    }

    static double[] TargetFuntion()
    {
        double[] FunctRef = new double[22];
        int u = 0;
        for (double x = -1; x < 1.1; x += 0.1)
        {
            FunctRef[u] = - 0.3 + (0.1 * x) - (0.5 * x * x) + (0.4 * x * x * x);
            u++;
        }
        //for (int i = 0; i < FunctRef.Length; i++)
        //{
        //    Console.WriteLine($"Funct[{i}] = {FunctRef[i]}" );
        //}

        return FunctRef;
    }

    static void calcRMSE(ref double[,] Population, double[] FuncRef)
    {
        double[] individualResp = new double[22];

        for (int i = 0; i < GlobalVariables.POPULATION_SIZE; i++)
        {
            int u = 0;
            double somatoria = 0;
            for (double x = -1; x < 1.1; x +=0.1)
            {
                individualResp[u] = Population[i, 0] + Population[i, 1] * x + Population[i, 2] * x * x + Population[i, 3] * x * x * x;
                u++;    
            }

            for (int j = 0; j < 22;j ++)
            {
                somatoria += Math.Pow(FuncRef[j] - individualResp[j], 2);
            }
            double rmse = Math.Sqrt(somatoria/22);
            rmse = Math.Round(rmse, 5);
            Population[i,4] = rmse;

        }
    }

    static void Main(string[] args)
    {
        double[] ReferenceFunction = TargetFuntion();

        double[,] population = GeraMatriz(GlobalVariables.POPULATION_SIZE);
        calcRMSE(ref population,ReferenceFunction);
        PrintMatrix(population);




    }
}




