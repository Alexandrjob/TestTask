namespace TestTask
{
    public class ReadOnlyStream : IReadOnlyStream
    {
        private readonly Stream _localStream;
        private int position;

        /// <summary>
        /// Конструктор класса. 
        /// Т.к. происходит прямая работа с файлом, необходимо 
        /// обеспечить ГАРАНТИРОВАННОЕ закрытие файла после окончания работы с таковым!
        /// </summary>
        /// <param name="fileFullPath">Полный путь до файла для чтения</param>
        public ReadOnlyStream(string fileFullPath)
        {
            IsEof = true;
            position = 0;

            _localStream = new FileStream(fileFullPath, FileMode.Open, FileAccess.Read);
            _localStream.Position = 0;
        }

        /// <summary>
        /// Флаг окончания файла.
        /// </summary>
        public bool IsEof { get; private set; }

        /// <summary>
        /// Ф-ция чтения следующего символа из потока.
        /// Если произведена попытка прочитать символ после достижения конца файла, метод 
        /// должен бросать соответствующее исключение
        /// </summary>
        /// <returns>Считанный символ.</returns>
        public async Task<char> ReadNextChar()
        {
            var buffer = new char[1];
            var charCount = 0;

            _localStream.Seek(position, SeekOrigin.Begin);
            var reader = new StreamReader(_localStream);

            charCount = await reader.ReadAsync(buffer, 0, 1);

            if (charCount == 1)
            {
                position++;
                return buffer[0] == 65533 ? (char)65533 : buffer[0];
            }

            if (IsEof)
                throw new Exception("Reached end of file.");

            IsEof = true;
            return '\0';
        }

        /// <summary>
        /// Сбрасывает текущую позицию потока на начало.
        /// </summary>
        public void ResetPositionToStart()
        {
            if (_localStream == null)
            {
                IsEof = true;
                return;
            }

            _localStream.Position = 0;
            IsEof = false;
        }

        public void Close()
        {
            _localStream.Close();
            _localStream.Dispose();
        }
    }
}