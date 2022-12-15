namespace Specs.Order_of_magnitude;

public class From
{
    [TestCase(10.1, O.ns10)]
    [TestCase(540.1, O.ns100)]
    public void nanaseconds(double seconds, O o)
        => TimeSpan.FromMicroseconds(seconds / 1000).O().Should().Be(o);

    [TestCase(1.1, O.μs)]
    [TestCase(10.1, O.μs10)]
    [TestCase(540.1, O.μs100)]
    public void microseconds(double seconds, O o)
        => TimeSpan.FromMicroseconds(seconds).O().Should().Be(o);

    [TestCase(1.1, O.ms)]
    [TestCase(10.1, O.ms10)]
    [TestCase(540.1, O.ms100)]
    public void milliseconds(double seconds, O o)
        => TimeSpan.FromMilliseconds(seconds).O().Should().Be(o);

    [TestCase(1.1, O.s)]
    [TestCase(10.1, O.s10)]
    [TestCase(540.1, O.s100)]
    [TestCase(9540.1, O.s1000)]
    public void seconds(double seconds, O o)
        => TimeSpan.FromSeconds(seconds).O().Should().Be(o);
}
