namespace tests_.src.Application.Repositories
{
    using System;

    public static class ObjectIdGenerator
    {
        private static Random _random = new Random();

        public static string GenerateRandomObjectId()
        {
            byte[] buffer = new byte[12]; // 12 bytes
            _random.NextBytes(buffer); // Preenche o buffer com bytes aleatórios
            return BitConverter.ToString(buffer).Replace("-", "").ToLower(); // Converte para hexadecimal e remove os traços
        }
    }

}
