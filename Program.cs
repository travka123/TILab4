using System;
using System.IO;

namespace TILab4 {
    class Program {
        static void Main(string[] args) {
            //Вариант 7

            //LSFR1 x^29 + x^2 + 1
            //LSFR2 x^37 + x^12 + x^10 + x^2 + 1
            //LSFR3 x^27 + x^8 + x^7 + x^1 + 1

            GeneratorGeffe generatorGeffe;

            using (StreamReader streamReader = File.OpenText("..\\..\\init_values.txt")) {
                string lfsr1str = streamReader.ReadLine();
                string lfsr2str = streamReader.ReadLine();
                string lfsr3str = streamReader.ReadLine();

                if (lfsr1str.Length != 29) {
                    throw new Exception($"Длинна строки 1 {lfsr1str.Length}, ожидалось - 29");
                }
                else if (lfsr2str.Length != 37) {
                    throw new Exception($"Длинна строки 2 {lfsr2str.Length}, ожидалось - 37");
                }
                else if (lfsr3str.Length != 27) {
                    throw new Exception($"Длинна строки 3 {lfsr3str.Length}, ожидалось - 27");
                }

                generatorGeffe = new GeneratorGeffe(
                    new LFSR(new int[] { 1 }, lfsr1str),
                    new LFSR(new int[] { 11, 9, 1}, lfsr2str),
                    new LFSR(new int[] { 7, 6, 0 }, lfsr3str));
            }

            Console.WriteLine("Введите путь к файлу");
            FileInfo fileInfo = new FileInfo(Console.ReadLine());

            using (BinaryReader binaryReader = new BinaryReader(File.Open(fileInfo.FullName, FileMode.Open, FileAccess.Read))) {
                using (BinaryWriter binaryWriter = new BinaryWriter(File.Open(
                        $"{fileInfo.Directory}\\{fileInfo.Name.Replace(fileInfo.Extension, "")}_encrypted{fileInfo.Extension}",
                        FileMode.Create, FileAccess.Write))) {

                    FileStream source = binaryReader.BaseStream as FileStream;
                    while (source.Position < source.Length) {
                        binaryWriter.Write((byte)(binaryReader.ReadByte() ^ generatorGeffe.Next()));
                    }
                }
            }
        }
    }
}
