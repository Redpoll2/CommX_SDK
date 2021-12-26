using PackageManager.IO.Tests.Resources;
using System;
using System.IO;
using Xunit;

namespace PackageManager.IO.Tests
{
    public class AsciizBinaryReaderTest
    {
        [Fact]
        public void ReadString()
        {
            using Stream stream = TestResources.GetResourceStream("NullTerminatedString.bin") ?? throw new Exception("Can't find test resource.");
            using var reader = new AsciizBinaryReader(stream);

            string str = reader.ReadString();

            Assert.Equal("Null-terminated string", str);
        }

        [Fact]
        public void ReadString_Empty()
        {
            using Stream stream = TestResources.GetResourceStream("Empty.bin") ?? throw new Exception("Can't find test resource.");
            using var reader = new AsciizBinaryReader(stream);

            string str = reader.ReadString();

            Assert.Equal(string.Empty, str);
        }
    }
}