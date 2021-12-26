using System.IO;
using System.Text;

namespace PackageManager.IO
{
    /// <summary>
    /// Represents a <see cref="BinaryReader"/> that reads null-terminated strings and other primitive data types.
    /// </summary>
    public class AsciizBinaryReader : BinaryReader
    {
        private readonly StringBuilder _stringBuilder = new();

        /// <summary>
        /// Initializes a new instance of the <see cref="AsciizBinaryReader"/> class
        /// based on the specified stream and using ASCII encoding.
        /// </summary>
        /// <param name="input">The input stream.</param>
        /// <exception cref="System.ArgumentException">The stream does not support reading, is null, or is already closed.</exception>
        public AsciizBinaryReader(Stream input) : this(input, Encoding.ASCII, false)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AsciizBinaryReader"/> class
        /// based on the specified stream and character encoding.
        /// </summary>
        /// <param name="input">The input stream.</param>
        /// <param name="encoding">The character encoding to use.</param>
        /// <exception cref="System.ArgumentException">The stream does not support reading, is null, or is already closed.</exception>
        /// <exception cref="System.ArgumentNullException">encoding is null.</exception>
        public AsciizBinaryReader(Stream input, Encoding encoding) : this(input, encoding, false)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AsciizBinaryReader"/> class based on
        /// the specified stream and character encoding, and optionally leaves the stream open.
        /// </summary>
        /// <param name="input">The input stream.</param>
        /// <param name="encoding">The character encoding to use.</param>
        /// <param name="leaveOpen">true to leave the stream open after the <see cref="AsciizBinaryReader"/> object is disposed; otherwise, false.</param>
        /// <exception cref="System.ArgumentException">The stream does not support reading, is null, or is already closed.</exception>
        /// <exception cref="System.ArgumentNullException">encoding or input is null.</exception>
        public AsciizBinaryReader(Stream input, Encoding encoding, bool leaveOpen) : base(input, encoding, leaveOpen)
        {
        }

        /// <summary>
        /// Reads a null-terminated <see cref="string"/> from the current stream.
        /// </summary>
        /// <returns>The string being read.</returns>
        public override string ReadString()
        {
            _stringBuilder.Clear();

            while (true)
            {
                int character = Read();

                if (character is char.MinValue or -1)
                    break;

                _stringBuilder.Append((char)character);
            }

            return _stringBuilder.ToString();
        }
    }
}
