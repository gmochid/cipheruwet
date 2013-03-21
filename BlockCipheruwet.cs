using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cipheruwet
{
    class BlockCipheruwet
    {
        public BlockCipheruwet(byte[] input, byte[] key)
        {
            input.CopyTo(Input, 0);
            key.CopyTo(Key, 0);
        }

        public byte[] encrypt()
        {
            return encryptFeistel();
        }

        public byte[] decrypt()
        {
            return encryptFeistel();
        }

        private byte[] encryptFeistel()
        {
            initialization();
            byte[] tempCipherA = new byte[SIZE7];
            byte[] tempCipherB = new byte[SIZE7];
            for (int i = 1; i < FEISTEL_COUNT; i++)
            {
                CipherB.CopyTo(tempCipherA, 0);
                tempCipherB = bitwiseXOR(CipherB, new BlockCipheruwetEnDc(Key, CipherA).encrypt());

                tempCipherA.CopyTo(CipherA, 0);
                tempCipherB.CopyTo(CipherB, 0);
            }
            CipherB = bitwiseXOR(CipherB, new BlockCipheruwetEnDc(Key, CipherA).encrypt());

            byte[] ret = new byte[SIZE8];
            CipherA.CopyTo(ret, 0);
            CipherB.CopyTo(ret, SIZE7);
            return ret;
        }

        private byte[] bitwiseXOR(byte[] A, byte[] B)
        {
            byte[] ret = new byte[A.Length];
            for (int i = 0; i < A.Length; i++)
            {
                ret[i] = (byte) (A[i] ^ B[i]);
            }
            return ret;
        }

        private void initialization()
        {
            int i = 0;
            for (; i < SIZE7; i++)
            {
                CipherA[i] = Input[i];
            }
            for (int k = 0; i < SIZE8; i++, k++)
            {
                CipherB[k] = Input[i];
            }
        }

        private byte[] Input;
        private byte[] Key;
        private byte[] CipherA;
        private byte[] CipherB;

        public static const int SIZE8 = (1 << 8);
        public static const int SIZE7 = (1 << 7);
        public static const int FEISTEL_COUNT = (1 << 4);
    }
}
