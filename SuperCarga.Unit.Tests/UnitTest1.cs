using NUnit.Framework;
using System;
using System.IO;

namespace SuperCarga.Unit.Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            var bytes1 = File.ReadAllBytes("C:\\Users\\ultra\\Desktop\\SC\\1.png");
            var file1 = Convert.ToBase64String(bytes1);

            var bytes2 = File.ReadAllBytes("C:\\Users\\ultra\\Desktop\\SC\\2.png");
            var file2 = Convert.ToBase64String(bytes2);

            Assert.Pass();
        }
    }
}