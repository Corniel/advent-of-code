using Qowaiv;

namespace Specs.Advent_date;

public class All_available
{
    [Test]
    public void None_before_2015_december_1()
        => AdventDate.AllAvailable(new Date(2015, 11, 30)).Should().BeEmpty();

    [Test]
    public void Two_at_2015_december_1_after_5AM()
        => AdventDate.AllAvailable(new DateTime(2015, 12, 01, 05, 00, 00, DateTimeKind.Utc)).Should().HaveCount(2);

    [Test]
    public void _50_per_year()
        => AdventDate.AllAvailable(new Date(2015, 12, 26)).Should().HaveCount(50);
}
