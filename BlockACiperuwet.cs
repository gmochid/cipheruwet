using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cipheruwet
{
    class BlockACiperuwet
    {
        public BlockACiperuwet(byte[] key, byte[] input)
        {
            mInput = input;
            mKey = key;
            mTable = new byte[SIZE];
            generateRandom(sumKeys());
        }

        public byte[] encrypt()
        {
            return encryptC(mInput);
        }

        public byte[] decrypt()
        {
            return decryptC(mInput);
        }

        private byte[] decryptC(byte[] input)
        {
            byte[] temp = encryptB(input);
            byte[] cipher = new byte[SIZE];

            for (int i = 0; i < SIZE; i++)
            {
                cipher[mTable[i]] = temp[i];
            }

            return cipher;
        }

        private byte[] encryptC(byte[] input)
        {
            byte[] temp = encryptB(input);
            byte[] cipher = new byte[SIZE];

            for (int i = 0; i < SIZE; i++)
            {
                cipher[i] = temp[mTable[i]];
            }

            return cipher;
        }

        private byte[] encryptB(byte[] input)
        {
            byte[] cipher = new byte[SIZE];
            byte[] key = new byte[SIZE];
            input.CopyTo(cipher, 0);
            for (int i = 0; i < 8; i++)
			{
                cipher = encryptA(cipher);
                mKey.Reverse();
			}
            return cipher;
        }

        private byte[] encryptA(byte[] input)
        {
            byte[] cipher = new byte[SIZE];
            for (int i = 0; i < cipher.Length; i++)
            {
                cipher[i] = (byte) (input[i] ^ mKey[i]);
            }
            return cipher;
        }

        private void generateRandom(int key)
        {
            Random r = new Random(key);
            for (int i = 0; i < SIZE; i++)
            {
                mTable[i] = 0;
            }

            for (int i = 1; i < SIZE; i++)
            {
                int x = r.Next(SIZE);
                while (mTable[x] != 0)
                {
                    x = (x + 1) % SIZE;
                }
                mTable[x] = (byte) i;
            }
        }

        private int sumKeys()
        {
            int x = 0;
            for (int i = 0; i < SIZE; i++)
            {
                x = (x + mKey[i]) % (1 << 16);
            }
            return x;
        }

        private byte[] mKey;
        private byte[] mInput;

        private byte[] mTable;

        public static const int SIZE = (1 << 8);
    }
}
