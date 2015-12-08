using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace glitch_art
{
    public static class ArrayExtensions
    {
        public static void Populate<T>(this T[] array, T value)
        {
            for (var i = 0; i < array.Length; ++i)
            {
                array[i] = value;
            }
        }
    }
    class Program
    {
        private static string glitch = "unflattering";
        private static string glitchEnd = ".bmp";
        static Random random = new Random();
        //private const int bufferSize = 750000;
        private const int headerSize = 100;
        static void Main(string[] args)
        {
            //Scramble(glitch + glitchEnd);
            Swap(glitch + glitchEnd);
            //Sort(glitch + glitchEnd);
            //Copy(glitch + glitchEnd);
        }
        /// <summary>
        /// Takes a fileName, then opens that file and scrambles the contents.
        /// The scrambling method will be decided later
        /// </summary>
        /// <param name="path"></param>
        //static void Scramble(string fileName)
        //{
        //    using (var stream = new FileStream(fileName, FileMode.Open, FileAccess.ReadWrite))
        //    {

        //        try
        //        {
        //            stream.Position = 100;
        //            int newValue;
        //            int[] buffer = new int[1000];
        //            int i = 0;
        //            while (stream.Position < stream.Length - 100)
        //            {
        //                if (stream.Position < stream.Length - 1000 /*&& stream.Position % 2 == 0*/)
        //                {
        //                    for (i = 0; i < buffer.Length; i++)
        //                    {
        //                        buffer[i] = stream.ReadByte();
        //                    }
        //                    stream.Position -= buffer.Length + 1;

        //                    newValue = Average(buffer);
        //                }
        //                //else if (stream.Position % 3 == 0 || stream.Position % 2 != 0)
        //                //{
        //                //    int nextByte = stream.ReadByte();
        //                //    stream.Position -= 2;
        //                //    newValue = (int)(255 * Math.Sqrt(Math.Cos(Math.PI * nextByte)));
        //                //}
        //                //else if (stream.Position % 7 == 0)
        //                //{
        //                //     int nextByte = stream.ReadByte();
        //                //    stream.Position -= 2;
        //                //    newValue = random.Next(0, nextByte);
        //                //}
        //                else
        //                {
        //                    int nextByte = stream.ReadByte();
        //                    stream.Position -= 2;
        //                    newValue = random.Next(0, nextByte);
        //                }


        //                #region Basic stuff
        //                //if (stream.Position < stream.Length / 2)
        //                //{
        //                //    for (i = 0; i < buffer.Length; i++)
        //                //    {
        //                //        buffer[i] = stream.ReadByte();
        //                //    }
        //                //    stream.Position -= 3;
        //                //    newValue = (int)((buffer[0] + buffer[1] + buffer[2]) / (float)buffer.Length);
        //                //}
        //                //else
        //                //{
        //                //    int nextByte = stream.ReadByte();
        //                //    stream.Position -= 2;
        //                //    newValue = random.Next(0, nextByte);
        //                //}
        //                #endregion
        //                stream.WriteByte((Byte)newValue);
        //                stream.Position++;
        //            }
        //        } catch(Exception e)
        //        {
        //            Console.WriteLine(e.Message);
        //        }
        //    }
        //}   

        static void Swap(string fileName)
        {
            int bufferSize = random.Next(36700, 79823);

            var firstBuffer = new byte[bufferSize];
            var secondBuffer = new byte[bufferSize];
            var swapBuffer = new byte[bufferSize];
            var headerBuffer = new byte[headerSize];
            int multiplier = random.Next(0, 3);
            byte[] restOfTheDamnedFile;
            
            try
            {
                using (var readStream = new FileStream(fileName, FileMode.Open, FileAccess.ReadWrite))
                {
                    using (var logStream = new FileStream("log.txt", FileMode.Open, FileAccess.Write))
                    {
                        // Log buffersize to a file for future reference
                        logStream.Write(Encoding.ASCII.GetBytes(bufferSize.ToString() + "\n"), 0, Encoding.ASCII.GetBytes(bufferSize.ToString() + "\n").Length);
                    }
                        // Read the first hundred bytes into an array
                        readStream.Read(headerBuffer, 0, headerSize);
                    using (var writeStream = File.OpenWrite(glitch + "glitched" + glitchEnd))
                    {
                        // Write the headerBuffer into a new file
                        writeStream.Write(headerBuffer, 0, headerSize);

                        // Read the thingies
                        readStream.Position = bufferSize * multiplier;
                        readStream.Read(firstBuffer, 0, bufferSize);
                        readStream.Read(secondBuffer, 0, bufferSize);

                        Array.Copy(firstBuffer, swapBuffer, bufferSize);
                        Array.Copy(secondBuffer, firstBuffer, bufferSize);

                        //firstBuffer.Populate((byte)random.Next(0, (int)AverageByte(secondBuffer)));

                        writeStream.Write(headerBuffer, 0, headerSize);
                        writeStream.Write(firstBuffer, 0, bufferSize);
                        writeStream.Write(swapBuffer, 0, bufferSize);
                        // Implement "write rest of the damned image", you lazy dunce
                        restOfTheDamnedFile = new byte[readStream.Length - (bufferSize * 2) - headerSize];

                        readStream.Read(restOfTheDamnedFile, 0, restOfTheDamnedFile.Length);
                        writeStream.Write(restOfTheDamnedFile, 0, restOfTheDamnedFile.Length);

                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        static void Sort(string fileName)
        {
            int bufferSize = random.Next(367003, 598234);
            var headerBuffer = new byte[headerSize];

            var buffer = new byte[bufferSize];
            try
            {
                using (var ioStream = new FileStream(fileName, FileMode.Open, FileAccess.ReadWrite))
                {
                    // Read the first hundred bytes into an array
                    ioStream.Read(headerBuffer, 0, headerSize);
                    using (var writeStream = File.OpenWrite(glitch + "glitched" + glitchEnd))
                    {
                        // Write the headerBuffer into a new file
                        writeStream.Write(headerBuffer, 0, headerSize);
                        ioStream.Position = bufferSize;
                        ioStream.Read(buffer, 0, bufferSize);
                        Array.Sort(buffer);
                        writeStream.Write(buffer, 0, bufferSize);
                        byte[] restOfTheDamnedFile = new byte[ioStream.Length - bufferSize - headerSize];

                        ioStream.Read(restOfTheDamnedFile, 0, restOfTheDamnedFile.Length);
                        writeStream.Write(restOfTheDamnedFile, 0, restOfTheDamnedFile.Length);
                    }
                }
            }
            catch (Exception ex)
            {
                using (var logStream = new FileStream("log.txt", FileMode.Open, FileAccess.Write))
                {
                    // Log buffersize to a file for future reference
                    logStream.Write(Encoding.ASCII.GetBytes(ex.StackTrace + "\n" + ex.Message), 0, (ex.StackTrace + "\n" + ex.Message).Length);
                }
            }
        }
        static void Copy(string fileName)
        {
            int bufferSize = random.Next(3670000, 7982300);

            var buffer = new byte[bufferSize];
            var headerBuffer = new byte[headerSize];
            int multiplier = random.Next(0, 3);
            byte[] restOfTheDamnedFile;

            try
            {
                using (var readStream = new FileStream(fileName, FileMode.Open, FileAccess.ReadWrite))
                {
                    using (var logStream = new FileStream("log.txt", FileMode.Open, FileAccess.Write))
                    {
                        // Log buffersize to a file for future reference
                        logStream.Write(Encoding.ASCII.GetBytes(bufferSize.ToString() + "\n"), 0, Encoding.ASCII.GetBytes(bufferSize.ToString() + "\n").Length);
                    }
                    // Read the first hundred bytes into an array
                    readStream.Read(headerBuffer, 0, headerSize);
                    using (var writeStream = File.OpenWrite(glitch + "glitched" + glitchEnd))
                    {
                        // Write the headerBuffer into a new file
                        writeStream.Write(headerBuffer, 0, headerSize);

                        // Read the thingies
                        readStream.Position = bufferSize * multiplier;
                        readStream.Read(buffer, 0, bufferSize);
                        

                        //firstBuffer.Populate((byte)random.Next(0, (int)AverageByte(secondBuffer)));

                        writeStream.Write(headerBuffer, 0, headerSize);
                        writeStream.Write(buffer, 0, bufferSize);
                        writeStream.Write(buffer, 0, bufferSize);
                        // Implement "write rest of the damned image", you lazy dunce
                        restOfTheDamnedFile = new byte[readStream.Length- bufferSize - headerSize];

                        readStream.Read(restOfTheDamnedFile, 0, restOfTheDamnedFile.Length);
                        writeStream.Write(restOfTheDamnedFile, 0, restOfTheDamnedFile.Length);

                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        static byte AverageByte(byte[] ba)
        {
            int sum = 0;
            foreach(byte b in ba)
            {
                sum += (int)b;
            }
            sum /= ba.Length;

            return (byte) sum;
        }

    }

}
