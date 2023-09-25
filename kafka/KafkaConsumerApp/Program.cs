using System;
using Confluent.Kafka;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using kafkaConsumerApp.Data;

class Program
{
    static void Main(string[] args)
    {
        var config = new ConsumerConfig
        {
            BootstrapServers = "localhost:9092",
            GroupId = "popularidad_usuario_group",
            AutoOffsetReset = AutoOffsetReset.Earliest,
        };

        using var consumer = new ConsumerBuilder<string, string>(config).Build();
        consumer.Subscribe("PopularidadUsuario");

        var timer = new System.Timers.Timer(30000); // Timer para leer cada 30 segundos
        timer.Elapsed += (sender, e) =>
        {
            ConsumeMessages(consumer);
        };

        timer.Start();

        // Manejo de cierre controlado
        Console.CancelKeyPress += (sender, e) =>
        {
            consumer.Close(); // Cierre del consumidor al detener la aplicación
        };

        // Mantén la aplicación en funcionamiento hasta que se presione Enter
        Console.WriteLine("Presiona Enter para salir...");
        Console.ReadLine();
    }

    static void ConsumeMessages(IConsumer<string, string> consumer)
    {
        while (true)
        {
            var result = consumer.Consume();

            // Procesa el mensaje recibido y actualiza la base de datos
            var key = result.Message.Key;
            var value = result.Message.Value;

            // Realiza la lógica de comparación y actualización en la base de datos aquí
            if (IsValidInput(key, value))
            {
                UpdateUserPopular(key, value);
            }
        }
    }

    static bool IsValidInput(string username, string increment)
    {
        // Realiza validación de entrada aquí, por ejemplo, asegurándote de que increment sea numérico
        return int.TryParse(increment, out _);
    }

    static void UpdateUserPopular(string username, string increment)
    {
        try
        {
            // Define tu cadena de conexión real aquí
            var connectionString = "Server=localhost;Port=3306;Database=chefencasagrupoj;User=root;Password=root;";
            var serverVersion = new MySqlServerVersion(new Version(8, 0, 28));
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseMySql(connectionString, serverVersion);

            using (var dbContext = new ApplicationDbContext(optionsBuilder.Options))
            {
                var user = dbContext.Usuario.FirstOrDefault(u => u.user == username);

                if (user != null)
                {
                    if (int.TryParse(increment, out var incrementValue))
                    {
                        user.popular += incrementValue;

                        dbContext.SaveChanges();

                        Console.WriteLine($"Actualización de popularidad para {username}: +{incrementValue}");
                    }
                    else
                    {
                        Console.WriteLine($"Error: No se pudo convertir el incremento '{increment}' a un valor numérico.");
                    }
                }
                else
                {
                    Console.WriteLine($"Error: Usuario '{username}' no encontrado en la base de datos.");
                }
            }
        }
        catch (Exception ex)
        {
            // Manejo de errores: registra el error o toma medidas adicionales según sea necesario
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}
