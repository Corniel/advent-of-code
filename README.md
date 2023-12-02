# [Advent of Code](https://adventofcode.com/)
**[adventofcode.com](https://adventofcode.com)**
    
                            *
                           <o>
                          <o>O>
                         <o>>O>>
                        <<O>>>o>>
                       <<o<O>>@>>>
                      <<*<<@>>>*>>>
                     <<@<<<O>>>@>@>>
                    <<O>>o<<<o<<*>>>>
                   <<o<<<O<*<@>@>>>O>>
                  <@<O<<<*<@<<@<o>@>>>>
                 <<*>o<*>>>*<<<O<<<O>>>>
                <*>>>O>>>@<@>>>O<<<@<<<*>
               <<o>*<o>O<<O>>>O>>*>>>o>>o>
              <<@<<<@>>O<o<@<<<*>>>o>>>*<O>
             <O<<<o<o<<*<<<@>@>>>O<<<o>>>o>>
            <<o>>*<O<@<<*>*>>*<@<<O<<<O>>>*>>
           <O>>O>>>*<<O<*<<o<*<<o<O<*>>>@<<o>>
          <<O<@<O<<<o<o>>>O<o>O>>>O<<*>>@>>o>*>
         <<@<<<O<<o<o<<*>o>*<*<<o<<<*>>o>o>o<@>>
        <<@<<<O>>O<<<*>@>>>@<<*<@>>O>>>o<*>>>O>O>
       <<o<*<<<*>>>o<<@<<<O>@<<*<*>>*<<o<<<@<@>>*>
      <*>o<<<O>>>o<<O>*>@<<@>O>@>@>>@<O<@>>>@<O<o>>
     <o>>>O>O<<O<<<*<O>>O>>>O<o<<@<<<o>>>O<o>>>O>>>>
    <@<<@>o>*>>O<o>>o<*>>>O<<<@>>*<<<O<O>O>@>>>o<<*>>
                          |   |
                          |   |
               _  _ __ ___|___|___ __ _  _

Advent of Code is an Advent calendar of small programming puzzles for a variety
of skill sets and skill levels that can be solved in any programming language
you like. People use them as a speed contest, interview prep, company training,
university coursework, practice problems, or to challenge each other.

## Participating friends
* [Ad](https://github.com/advdh/AdventOfCode) _private_
* [Chiel](https://github.com/cvooster/advent_of_code)
* [Jack](https://github.com/Sjaaky/advent-of-code)
* [Lannie](https://github.com/lannieligthart/advent_of_code)
* [Laura](https://github.com/LauraXiulan/advent-of-code)

## Lines of Code (LoC)
> When a measure becomes a target, it ceases to be a good measure.
We all know it to be true, so keeping track of the lines of code per puzzle is
questionable, nevertheless, I do: [overview](LoC.md).

Note that comments, whitespace, namespace declarations, main class declarations,
and lines only containing brackets or attributes are ignored.

## Unit test driven
All puzzles can be run as unit tests. To reduce plumbing code I've extended on
[NUnit](https://nunit.org/). All puzzles have the following template:

``` C#
namespace Advent_of_Code_@Year;

[Category(Category.SomeCatagory)]
public class Day_@Day
{
    [Example(answer: 666, "Short Example")]
    [Puzzle(answer: 666, O.μs10)]
    public long part_one(string str)
    {
        throw new NoAnswer();
    }

    [Example(answer: 666, "Short Example")]
    [Puzzle(answer: 666, O.μs10)]
    public long part_two(string str)
    {
        throw new NoAnswer();
    }
}
```
