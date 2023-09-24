using System;
using System.Linq;
using System.Threading;
using Confluent.Kafka; 

public class PopularidadCalculator
{
    private readonly string bootstrapServers;
    private readonly string groupId;
    private readonly string topicName;

    public PopularidadCalculator(string bootstrapServers, string groupId, string topicName)
    {
        this.bootstrapServers = bootstrapServers;
        this.groupId = groupId;
        this.topicName = topicName;
    }

    public void CalcularPopularidadPeriodicamente()
    {
        // Configura un temporizador para ejecutar el cálculo cada hora
        var timer = new Timer(ProcesarMensajesKafka, null, TimeSpan.Zero, TimeSpan.FromSeconds(30));
    }

    private void ProcesarMensajesKafka(object state)
    {
        var config = new ConsumerConfig
        {
            BootstrapServers = bootstrapServers,
            GroupId = groupId,
            AutoOffsetReset = AutoOffsetReset.Earliest // Puedes ajustar esto según tus necesidades
        };

        using (var consumer = new ConsumerBuilder<Ignore, string>(config).Build())
        {
            consumer.Subscribe(topicName);

            while (true)
            {
                try
                {
                    var consumeResult = consumer.Consume();
                    if (consumeResult != null)
                    {
                        var key = consumeResult.Message.Key;
                        var puntaje = int.Parse(consumeResult.Message.Value);

                        // Lógica para calcular la popularidad y actualizar la base de datos
                        ActualizarPopularidad(key, puntaje);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error al consumir mensaje de Kafka: {ex.Message}");
                }
            }
        }
    }

    private void ActualizarPopularidad(string usuario, int puntaje)
    {
        try
            {
            using (var cnx = new MySqlConnection("server=localhost;user=root;password=root;port=3306;database=chefencasagrupoj;"))
                {
                cnx.Open();

                // Recupera la popularidad actual del usuario desde la base de datos
                string selectQuery = $"SELECT idusuario, popular FROM usuario WHERE user = '{usuario}'";
                using (var cmd = new MySqlCommand(selectQuery, cnx))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int idUsuario = reader.GetInt32("idusuario");
                            int popularidadActual = reader.GetInt32("popular");

                            // Calcula la nueva popularidad sumando el puntaje
                            int nuevaPopularidad = popularidadActual + puntaje;

                            // Actualiza la base de datos con la nueva popularidad
                            string updateQuery = $"UPDATE usuario SET popular = {nuevaPopularidad} WHERE idusuario = {idUsuario}";
                            using (var updateCmd = new MySqlCommand(updateQuery, cnx))
                            {
                                updateCmd.ExecuteNonQuery();
                            }
                        }
                    }
                }
            }

            Console.WriteLine($"Actualización de popularidad para {usuario}, Puntaje: {puntaje}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al actualizar la popularidad: {ex.Message}");
        }
    }

}
