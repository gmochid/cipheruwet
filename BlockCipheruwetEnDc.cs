using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cipheruwet
{
    class BlockCipheruwetEnDc
    {
        public BlockCipheruwetEnDc(byte[] key, byte[] input)
        {
            Input = duplicate(input);
            Key = duplicate(key);

            Table = new byte[SIZE];
            generateRandom(sumKeys());
        }

        public byte[] encrypt()
        {
            return encryptTransposition(Input);
        }

        public byte[] decrypt()
        {
            return decryptTransposition(Input);
        }

        private byte[] decryptTransposition(byte[] input)
        {
            byte[] temp = reverseKey(input);
            byte[] cipher = new byte[SIZE];

            for (int i = 0; i < SIZE; i++)
            {
                cipher[Table[i]] = temp[i];
            }

            return cipher;
        }

        private byte[] encryptTransposition(byte[] input)
        {
            byte[] temp = reverseKey(input);
            byte[] cipher = new byte[SIZE];

            for (int i = 0; i < SIZE; i++)
            {
                cipher[i] = temp[Table[i]];
            }

            return cipher;
        }

        private byte[] reverseKey(byte[] input)
        {
            byte[] cipher = new byte[SIZE];
            byte[] key = new byte[SIZE];
            cipher = duplicate(input);
            for (int i = 0; i < 8; i++)
            {
                cipher = keyXOR(cipher);
                Key.Reverse();
            }
            return cipher;
        }

        private byte[] keyXOR(byte[] input)
        {
            byte[] cipher = new byte[SIZE];
            for (int i = 0; i < SIZE; i++)
            {
                cipher[i] = (byte)(input[i] ^ Key[i]);
            }
            return cipher;
        }

        private void generateRandom(int key)
        {
            Random r = new Random(key);
            for (int i = 0; i < SIZE; i++)
            {
                Table[i] = 0;
            }

            for (int i = 1; i < SIZE; i++)
            {
                int x = r.Next(SIZE);
                while (Table[x] != 0)
                {
                    x = (x + 1) % SIZE;
                }
                Table[x] = (byte)i;
            }
        }

        private int sumKeys()
        {
            int x = 0;
            for (int i = 0; i < SIZE; i++)
            {
                x = (x + Key[i]) % (1 << 16);
            }
            return x;
        }

        private byte[] duplicate(byte[] x)
        {
            byte[] ret = new byte[x.Length];
            for (int i = 0; i < x.Length; i++)
            {
                ret[i] = x[i];
            }
            return ret;
        }

        private byte[] Key;
        private byte[] Input;

        private byte[] Table;

        public const int SIZE = (1 << 7);
    }
}
