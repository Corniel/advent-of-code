namespace Advent_of_Code_2020;

public class Day_25
{
    [TestCase(08, 05764801)]
    [TestCase(11, 17807724)]
    public void Loop_size_is(int loopSize, long publicKey)
    {
        Assert.AreEqual(loopSize, LoopSize(publicKey));
    }

    [TestCase(14897079, 17807724, 8)]
    [TestCase(14897079, 5764801, 11)]
    public void Encryption_key_is(long key, long subject, int loops)
    {
        Assert.AreEqual(key, EncryptionKey(subject, loops));
    }

    [Example(answer: 14897079, input: "5764801\r\n17807724")]
    [Puzzle(answer: 16902792, input: "18356117\r\n5909654")]
    public long part_one(string input)
    {
        var numbers = input.Int32s().ToArray();
        var key0 = EncryptionKey(numbers[0], LoopSize(numbers[1]));
        var key1 = EncryptionKey(numbers[1], LoopSize(numbers[0]));
        Assert.AreEqual(key0, key1);
        return key1;
    }

    [Puzzle(answer: "You only have to pay 49 stars", input: "You only have to pay 49 stars")]
    public string part_two(string input) => input;

    private static int LoopSize(long key)
    {
        var loop = 0;
        long val = 1;
        for (; val != key; loop++) { val = (val * 7) % 20201227; }
        return loop;
    }
    private static long EncryptionKey(long subject, int loops)
    {
        long key = 1;
        for (var i = 0; i < loops; i++) { key = (key * subject) % 20201227; }
        return key;
    }
}
