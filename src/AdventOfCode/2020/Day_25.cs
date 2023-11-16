namespace Advent_of_Code_2020;

[Category(Category.Cryptography)]
public class Day_25
{
    [TestCase(08, 05764801)]
    [TestCase(11, 17807724)]
    public void Loop_size_is(int loopSize, long publicKey)
        => LoopSize(publicKey).Should().Be(loopSize);

    [TestCase(14897079, 17807724, 8)]
    [TestCase(14897079, 5764801, 11)]
    public void Encryption_key_is(long key, long subject, int loops)
        => EncryptionKey(subject, loops).Should().Be(key);   
    
    [Example(answer: 14897079,"5764801;17807724")]
    [Puzzle(answer: 16902792, "18356117;5909654", O.ms10)]
    public long part_one(string input)
    {
        var numbers = input.Int32s().ToArray();
        return EncryptionKey(numbers[0], LoopSize(numbers[1]));
    }

    [Puzzle(answer: "You only have to pay 49 stars", "You only have to pay 49 stars")]
    public string part_two(string input) => input;

    static int LoopSize(long key)
    {
        var loop = 0;
        long val = 1;
        for (; val != key; loop++) { val = (val * 7) % 20201227; }
        return loop;
    }
    static long EncryptionKey(long subject, int loops)
    {
        long key = 1;
        for (var i = 0; i < loops; i++) { key = (key * subject) % 20201227; }
        return key;
    }
}
