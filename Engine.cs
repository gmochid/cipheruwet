using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace cipheruwet
{
    class Engine
    {
        public const byte ECB = 1;
        public const byte CBC = 2;
        public const byte CFB = 3;
        public const byte OFB = 4;

        public static BlockCipheruwet Cipher;

        private static byte[] XorByteArray(byte[] a, byte[] b)
        {
            byte[] c = new byte[a.Length];

            for (int i = 0; i < c.Length; ++i)
            {
                c[i] = Convert.ToByte(a[i] ^ b[i]);
            }

            return c;
        }

        /// <summary>
        /// Encrypt a given source file and save it to a given target file.
        /// </summary>
        /// <param name="sourceFileName">The filename of the source file. Presumed to exist.</param>
        /// <param name="destinationFileName">The filename of the target file. Will be overwritten.</param>
        /// <param name="key">The key to encrypt with</param>
        /// <param name="mode">The mode of encryption. Use Engine.* constants.</param>
        /// <param name="blockSize">The chosen block size.</param>
        public static void StartEncryption(String sourceFileName, String destinationFileName, String key, byte mode, int blockSize)
        {
            if (mode != ECB && mode != CBC && mode != CFB && mode != OFB)
            {
                throw new Exception("Invalid block cipher mode of operation.");
            }

            // Auxiliary variables
            blockSize = 128;
            int blockSizeInBytes = blockSize / 8;
            byte[] crlf = { Convert.ToByte('\r'), Convert.ToByte('\n') };
            char[] keyChars = key.ToCharArray();
            byte[] keyBytes = new byte[key.Length];
            for (var i = 0; i < key.Length; ++i)
            {
                keyBytes[i] = Convert.ToByte(keyChars[i]);
            }

            // Open read handle. Treat all files as binary.
            FileStream fr = File.OpenRead(sourceFileName);
            BinaryReader br = new BinaryReader(fr);

            // Open write handle.
            FileStream fw = File.OpenWrite(destinationFileName);
            BinaryWriter bw = new BinaryWriter(fw);

            // Start by writing the header

            // The header consists of the following bytes:
            //  1-8  Original file length
            //  9    Block size
            // 10    Block cipher mode of operation
            // 11-12 CRLF
            // 13-.. Initialization vector (according to blocksize)
            // ..-.. CRLF

            long originalLength = Convert.ToInt64(fr.Length);
            Console.WriteLine(originalLength.ToString("X8"));
            bw.Write(originalLength);

            byte blockSizeByte = Convert.ToByte(blockSize);
            bw.Write(blockSizeByte);

            bw.Write(mode);

            bw.Write(crlf);

            Random iv = new Random();
            byte[] initializationVector = new byte[blockSizeInBytes];
            iv.NextBytes(initializationVector);
            bw.Write(initializationVector);

            bw.Write(crlf);

            // Header is written.

            // Write body
            long pos = 0;
            long length = fr.Length;
            byte[] readBuffer;
            BlockCipheruwet Cipher;

            byte[] previousBlock = (byte[])initializationVector.Clone();

            while (pos < length)
            {
                // Load one block into buffer
                readBuffer = br.ReadBytes(blockSizeInBytes);

                switch (mode)
                {
                    case ECB:
                        Cipher = new BlockCipheruwet(readBuffer, keyBytes);
                        bw.Write(Cipher.encrypt());
                        break;
                    case CBC:
                        byte[] preEncrypt = XorByteArray(readBuffer, previousBlock);
                        Cipher = new BlockCipheruwet(preEncrypt, keyBytes);
                        byte[] ciphertext = Cipher.encrypt();
                        bw.Write(ciphertext);
                        previousBlock = (byte[])ciphertext.Clone();
                        break;
                    default:
                        bw.Write(readBuffer);
                        break;
                }

                pos += blockSizeInBytes;
            }

            // Write padding

            // Body is written.

            // Close write handle
            bw.Close();
            fw.Close();

            // Close read handle
            br.Close();
            fr.Close();
        }

        /// <summary>
        /// Decrypt a given source file and save it to a given target file.
        /// </summary>
        /// <param name="sourceFileName">The filename of the source file. Presumed to exist.</param>
        /// <param name="destinationFileName">The filename of the target file. Will be overwritten.</param>
        /// <param name="key">The key to decrypt with.</param>
        public static void StartDecryption(String sourceFileName, String destinationFileName, String key)
        {
            // Open read handle. Treat all files as binary.
            FileStream fr = File.OpenRead(sourceFileName);
            BinaryReader br = new BinaryReader(fr);

            // Open write handle.
            //FileStream fw = File.OpenWrite(destinationFileName);
            //BinaryWriter bw = new BinaryWriter(fw);

            long x = br.ReadInt64();
            Console.WriteLine(x.ToString("X8"));
            br.Close();
            // Mode is read from the header of the file
        }
    }
}
