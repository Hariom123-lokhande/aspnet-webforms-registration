using System;
using System.IO;

class Program
{
    static void Main()
    {
        // Desktop path (safe location)
        string filePath = @"D:\ExceptionConsole.txt";// @"C:\Users\HP\Desktop\ExceptionConsole.txt";
        
        try
        {
            Console.WriteLine("Writing to file...");

            // Write data
            File.WriteAllText(filePath,
                "Hello Hariom\nWelcome to C# File Handling\n");

            Console.WriteLine("File written successfully.\n");

            // Read data
            Console.WriteLine("Reading from file...");
            string content = File.ReadAllText(filePath);
            Console.WriteLine("\nFile Content:\n" + content);

            // Custom Exception Example
            int age = 5;

            if (age < 0)
            {
                throw new ArgumentException("Age cannot be negative");
            }

            // Divide by zero Example
            int number = 0;

            if (number == 0)
            {
                throw new DivideByZeroException("Number cannot be zero");
            }

            int result = 10 / number;
            Console.WriteLine(result);
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine("\nArgument Error: " + ex.Message);
        }
        catch (DivideByZeroException ex)
        {
            Console.WriteLine("\nMath Error: " + ex.Message);
        }
        catch (IOException ex)
        {
            Console.WriteLine("\nIO Error: " + ex.Message);
        }
        catch (Exception ex)
        {
            Console.WriteLine("\nGeneral Error: " + ex.Message);
        }
        finally
        {
            Console.WriteLine("\nExecution completed.");
        }

        Console.WriteLine("\nPress Enter to exit...");
        Console.ReadLine();
    }
}