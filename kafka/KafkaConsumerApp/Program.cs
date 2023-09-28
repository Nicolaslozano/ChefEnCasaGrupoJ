using System;
using Confluent.Kafka;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using kafkaConsumerApp.Data;
using System.Text.Json;
using KafkaConsumerApp.Models;

class Program
{
    static void Main(string[] args)
    {
        //se configura el consumidor kafka
        
        var config = new ConsumerConfig
        {
            BootstrapServers = "localhost:9092", //direccion del kafka
            GroupId = "popularidad_usuario_group", //id del grupo de consumidores
            AutoOffsetReset = AutoOffsetReset.Earliest, // Reiniciar al principio cuando no hay un offset guardado
        };

        var config2 = new ConsumerConfig
        {
            BootstrapServers = "localhost:9092", //direccion del kafka
            GroupId = "popularidad_receta_group", //id del grupo de consumidores
            AutoOffsetReset = AutoOffsetReset.Earliest, // Reiniciar al principio cuando no hay un offset guardado
        };

        var config3 = new ConsumerConfig
        {
            BootstrapServers = "localhost:9092", //direccion del kafka
            GroupId = "popularidad_comentarios_group", //id del grupo de consumidores
            AutoOffsetReset = AutoOffsetReset.Earliest, // Reiniciar al principio cuando no hay un offset guardado
        };

        using var consumer = new ConsumerBuilder<string, string>(config).Build();  //creo el consumidor kafka para usuario
        consumer.Subscribe("PopularidadUsuario"); //me suscribo al topic "PopularidadUsuario"

        using var consumerReceta = new ConsumerBuilder<string, string>(config3).Build();  //creo el consumidor kafka comentarios
        consumerReceta.Subscribe("PopularidadReceta"); //me suscribo al topic "PopularidadReceta"

        using var consumerComentarios = new ConsumerBuilder<string, string>(config2).Build();  //creo el consumidor kafka receta
        consumerComentarios.Subscribe("Comentarios"); //me suscribo al topic "Comentarios"

        var timer = new System.Timers.Timer(30000); // configuro el temporalizador cada 30 segundos
        timer.Elapsed += (sender, e) =>
        {
            ConsumeMessagesTotales(consumer, consumerReceta, consumerComentarios);
        };
        timer.Start();

        // Manejo de cierre controlado
        Console.CancelKeyPress += (sender, e) =>
        {
            consumer.Close(); // Cierre del consumidor al detener la aplicación
            consumerReceta.Close(); // Cierre del consumidor al detener la aplicación
            consumerComentarios.Close(); // Cierre del consumidor al detener la aplicación

        };

        // Mantén la aplicación en funcionamiento hasta que se presione Enter
        Console.WriteLine("Presiona Enter para salir...");
        Console.ReadLine();
    }

    static void ConsumeMessagesTotales(IConsumer<string, string> consumerUsuario, IConsumer<string, string> consumerReceta, IConsumer<string, string> consumerComentarios )
    {

        Task.Run(() => ConsumeMessagesRecetas(consumerReceta));
        Task.Run(() => ConsumeMessages(consumerUsuario));
        Task.Run(() => ConsumeMessagesComentarios(consumerComentarios));
        Task.WaitAll();
    }


    static void ConsumeMessages(IConsumer<string, string> consumer)
    {
        while (true)
        {
            var result = consumer.Consume(); //agarro el mensaje kafka

            // Procesa el mensaje recibido y actualiza la base de datos
            var key = result.Message.Key; //clave que mande en el mensaje
            var value = result.Message.Value; //valor que mande en el mensaje

            // Realiza la lógica de comparación y actualización en la base de datos aquí
            if (IsValidInput(key, value))
            {
                UpdateUserPopular(key, value); //funcion para actualizar la base de datos de usuario popularidad
            }
        }
    }

    static void ConsumeMessagesRecetas(IConsumer<string, string> consumerReceta)
    {
        while (true)
        {
            var result = consumerReceta.Consume(); //agarro el mensaje kafka

            var message = result.Message.Value;

            try
            {
                // Deserializa el mensaje JSON en un objeto JsonDocument
                using (JsonDocument doc = JsonDocument.Parse(message))
                {
                    // Obtén el objeto raíz
                    JsonElement root = doc.RootElement;

                    // Obtiene los valores de IdReceta y Puntaje
                    if (root.TryGetProperty("IdReceta", out var idRecetaProp) && root.TryGetProperty("Puntaje", out var puntajeProp))
                    {
                        if (idRecetaProp.ValueKind == JsonValueKind.Number && puntajeProp.ValueKind == JsonValueKind.Number)
                        {
                            int recetaId = idRecetaProp.GetInt32();
                            int puntaje = puntajeProp.GetInt32();

                            UpdateRecetaPopular(recetaId, puntaje);
                        }
                        else
                        {
                            Console.WriteLine("Error: Los valores de IdReceta y Puntaje deben ser números.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Error: El mensaje JSON debe contener las propiedades IdReceta y Puntaje.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al procesar el mensaje JSON: {ex.Message}");
            }
            
        }
    }

    static void ConsumeMessagesComentarios(IConsumer<string, string> consumerComentarios)
    {
        while (true)
        {
            var result = consumerComentarios.Consume(); //agarro el mensaje kafka

            var message = result.Message.Value;

            try
            {
                // Deserializa el mensaje JSON en un objeto JsonDocument
                using (JsonDocument doc = JsonDocument.Parse(message))
                {
                    // Obtén el objeto raíz
                    JsonElement root = doc.RootElement;

                    // Obtiene los valores de Usuario, Receta y Comentario
                    if (root.TryGetProperty("Usuario", out var usuarioProp) && 
                    root.TryGetProperty("Receta", out var recetaProp) && 
                    root.TryGetProperty("Comentario", out var comentarioProp))
                    {
                        if (usuarioProp.ValueKind == JsonValueKind.String && 
                            recetaProp.ValueKind == JsonValueKind.Number &&
                            comentarioProp.ValueKind == JsonValueKind.String)
                        {
                            string usuario = usuarioProp.GetString();
                            int receta = recetaProp.GetInt32();
                            string comentario = comentarioProp.GetString();

                            AddComentario(usuario, receta, comentario);
                        }
                        else
                        {
                            Console.WriteLine("Error: Los valores no respetan el tipo correspondiente.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Error: El mensaje JSON debe contener las propiedades Usuario, Receta y Comentario.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al procesar el mensaje JSON: {ex.Message}");
            }
            
        }
    }






    static bool IsValidMessage(string message)
    {
        return !string.IsNullOrWhiteSpace(message);
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
            //configura el contexto de la BBDD
            var serverVersion = new MySqlServerVersion(new Version(8, 0, 28));
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseMySql(connectionString, serverVersion);

            using (var dbContext = new ApplicationDbContext(optionsBuilder.Options)) 
            {
                var user = dbContext.Usuario.FirstOrDefault(u => u.user == username); //consulta en la base de datos si u.user = ussername enviado por parametro para ver si esta la persona y asi poder modificar la base

                if (user != null)
                {
                    if (int.TryParse(increment, out var incrementValue))
                    {
                        user.popular += incrementValue; // Actualiza la popularidad del usuario
                        dbContext.SaveChanges(); //// Guarda los cambios en la base de datos
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


    static void UpdateRecetaPopular(int recetaId, int puntaje)
    {
        try
        {
            // Define tu cadena de conexión real aquí
            var connectionString = "Server=localhost;Port=3306;Database=chefencasagrupoj;User=root;Password=root;";
            // Configura el contexto de la BBDD
            var serverVersion = new MySqlServerVersion(new Version(8, 0, 28));
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseMySql(connectionString, serverVersion);
            
            using (var dbContext = new ApplicationDbContext(optionsBuilder.Options))
            {
                var receta = dbContext.Receta.FirstOrDefault(r => r.idreceta == recetaId);

                if (receta != null)
                {
                    // Actualiza recetaPopular con el nuevo valor
                    receta.recetaPopular += puntaje;

                    dbContext.SaveChanges(); // Guarda los cambios en la base de datos
                    Console.WriteLine($"Actualización de popularidad para Receta ID {recetaId}: +{puntaje}");
                }
                else
                {
                    Console.WriteLine($"Error: Receta con ID {recetaId} no encontrada en la base de datos.");
                }
            }
        }
        catch (Exception ex)
        {
            // Manejo de errores: registra el error o toma medidas adicionales según sea necesario
            Console.WriteLine($"Error: {ex.Message}");
        }
    }


    static void AddComentario(string usuario, int receta, string comentario1)
    {
        try
        {
            // Define tu cadena de conexión real aquí
            var connectionString = "Server=localhost;Port=3306;Database=chefencasagrupoj;User=root;Password=root;";
            // Configura el contexto de la BBDD
            var serverVersion = new MySqlServerVersion(new Version(8, 0, 28));
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseMySql(connectionString, serverVersion);
            
            var comen = new Comen
            {
                recet = receta,
                usuario_comen = usuario,
                comentario = comentario1
            };
            using (var dbContext = new ApplicationDbContext(optionsBuilder.Options))
            {
                dbContext.Comentarios.Add(comen);
                dbContext.SaveChanges();
            }
            Console.WriteLine($"Comentario recibido - Receta: {receta}, Usuario: {usuario}, Comentario: {comentario1}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al procesar el mensaje JSON: {ex.Message}");
        }
    }
}
