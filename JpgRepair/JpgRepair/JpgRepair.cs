using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Metadata.Profiles.Exif;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

namespace JpgRepair
{
    internal static class JpgRepair
    {
        public static void RewriteAndVerifyHeaders(string filePath)
        {
            // Проверяем, существует ли файл
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"Файл '{filePath}' не найден.");
            }

            // Читаем файл в байтовый массив
            byte[] fileData = File.ReadAllBytes(filePath);

            // Удаляем старые заголовки, если они есть
            RemoveOldHeaders(fileData);

            // Записываем заголовки JPG
            WriteJpgHeaders(fileData);

            // Проверяем, что файл теперь является JPG
            if (!IsJpgFile(fileData))
            {
                throw new Exception($"Файл '{filePath}' не является файлом JPG.");
            }

            // Записываем измененный файл
            File.WriteAllBytes(filePath, fileData);

            Console.WriteLine($"Заголовки файла '{filePath}' перезаписаны на заголовки JPG и проверены.");
        }

        private static void RemoveOldHeaders(byte[] fileData)
        {
            // Ищем маркеры JPG в файле
            int startMarkerIndex = 0;
            int endMarkerIndex = fileData.Length - 1;

            for (int i = 0; i < fileData.Length; i++)
            {
                if (fileData[i] == 0xFF && fileData[i + 1] == 0xD8)
                {
                    startMarkerIndex = i;
                    i += 2; // Пропускаем маркер
                }

                if (fileData[i] == 0xFF && fileData[i + 1] == 0xD9)
                {
                    endMarkerIndex = i;
                    break;
                }
            }

            // Удаляем старые заголовки
            Array.Clear(fileData, startMarkerIndex, endMarkerIndex - startMarkerIndex + 1);
        }

        private static void WriteJpgHeaders(byte[] fileData)
        {
            // Записываем заголовок JPG
            fileData[0] = 0xFF;
            fileData[1] = 0xD8;
            fileData[2] = 0xFF;
            fileData[3] = 0xE0;
            fileData[4] = 0x00;
            fileData[5] = 0x10;
            fileData[6] = 0x4A;
            fileData[7] = 0x46;
            fileData[8] = 0x49;
            fileData[9] = 0x46;
            fileData[10] = 0x00;
            fileData[11] = 0x01;
            fileData[12] = 0x01;
            fileData[13] = 0x00;
            fileData[14] = 0x00;
            fileData[15] = 0x00;
            fileData[16] = 0xFF;
            fileData[17] = 0xDB;
            fileData[18] = 0x00;
            fileData[19] = 0x43;
        }

        private static bool IsJpgFile(byte[] fileData)
        {
            // Проверяем наличие маркеров JPG
            for (int i = 0; i < fileData.Length - 1; i++)
            {
                if (fileData[i] == 0xFF && fileData[i + 1] == 0xD8)
                {
                    return true;
                }
            }

            return false;
        }

        public static bool TryRepairJpeg(string corruptedFilePath, string repairedFilePath)
        {

        }
    }
}
