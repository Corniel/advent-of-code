using Advent_of_Code;
using NUnit.Framework;
using SmartAss.Parsing;
using System.Linq;

namespace Advent_of_Code_2020
{
    public class Day_25
    {
        [TestCase(08, 7, 05764801)]
        [TestCase(11, 7, 17807724)]
        public void Loop_size_is(int loopSize, long subjectNumber, long publicKey)
        {
            Assert.AreEqual(loopSize, LoopSize(subjectNumber, publicKey));
        }

        [TestCase(14897079, 17807724, 8)]
        [TestCase(14897079, 5764801, 11)]
        public void Encryption_key_is(long encryptionKey, long publicKey, int loopsize)
        {
            Assert.AreEqual(encryptionKey, EncryptionKey(publicKey, loopsize));
        }

        [Example(answer: 14897079, input: "5764801\r\n17807724")]
        [Puzzle(answer: 16902792, input: "18356117\r\n5909654")]
        public long part_one(string input)
        {
            var numbers = input.Int32s().ToArray();
            var encryptionKey0 = EncryptionKey(numbers[0], LoopSize(7, numbers[1]));
            var encryptionKey1 = EncryptionKey(numbers[1], LoopSize(7, numbers[0]));
            Assert.AreEqual(encryptionKey0, encryptionKey1);
            return encryptionKey1;
        }

        public static int LoopSize(long subjectNumber, long publicKey)
        {
            var loop = 0;
            long value = 1;
            while(value != publicKey)
            {
                value = (value * subjectNumber) % 20201227;
                loop++;
            }
            return loop;
        }
        public static long EncryptionKey(long publicKey, int loops)
        {
            long key = 1;
            foreach (var loop in Enumerable.Range(0, loops))
            {
                key = (key * publicKey) % 20201227;
            }
            return key;
        }

        [Puzzle(answer: "You only have to pay 49 stars", input: "You only have to pay 49 stars")]
        public string part_two(string input) => input;
    }
}