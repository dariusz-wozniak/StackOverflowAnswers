using System;
using System.Globalization;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace StackOverflowAnswers.Csharp
{
    /// <summary>
    /// https://stackoverflow.com/questions/3395381/c-sharp-converting-a-string-containing-a-floating-point-to-an-integer/3395473#3395473
    /// </summary>
    public class ConvertingAStringContainingAFloatingPointToAnInteger
    {
        [Test]
        [SetCulture("en-US")]
        public void Repro1()
        {
            var a = (int)Convert.ToDouble("1.2");
            a.Should().Be(1);
        }

        [Test]
        public void Repro2()
        {
            var a = (int)Convert.ToDouble("1.2", CultureInfo.InvariantCulture.NumberFormat);
            a.Should().Be(1);
        }

        [Test]
        public void Repro3()
        {
            var a = int.Parse("1.2".Split('.')[0]);
            a.Should().Be(1);
        }

        [Test]
        public void Repro4()
        {
            var a = int.Parse("1.2".Split('.').First());
            a.Should().Be(1);
        }
    }
}
