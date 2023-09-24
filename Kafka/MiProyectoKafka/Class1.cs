namespace MiProyectoKafka
{
    class Class1
    {
        static void Main()
        {
            var kafkaBootstrapServers = "direccion_del_servidor_kafka"; // Reemplaza con la dirección correcta de Kafka
            var kafkaGroupId = "mi_grupo_de_consumo"; // Cambia esto según tus necesidades
            var kafkaTopicName = "PopularidadUsuario"; // Nombre del topic de Kafka

            var calculator = new PopularidadCalculator(kafkaBootstrapServers, kafkaGroupId, kafkaTopicName);
            calculator.CalcularPopularidadPeriodicamente();

            // Aquí puedes iniciar tu aplicación web u otros componentes de tu programa
        }
    }
}
