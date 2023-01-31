namespace TestTask;

public class StatisticService
{
    public async Task Start(string[] args)
    {
        IReadOnlyStream inputStream1 = GetInputStream(args[0]);
        IReadOnlyStream inputStream2 = GetInputStream(args[1]);

        IList<LetterStats> singleLetterStats = await FillSingleLetterStats(inputStream1);
        IList<LetterStats> doubleLetterStats = await FillDoubleLetterStats(inputStream2);

        RemoveCharStatsByType(singleLetterStats, CharType.Vowel);
        RemoveCharStatsByType(doubleLetterStats, CharType.Consonants);

        PrintStatistic(singleLetterStats);
        PrintStatistic(doubleLetterStats);

        // TODO : Необжодимо дождаться нажатия клавиши, прежде чем завершать выполнение программы.
    }

    /// <summary>
    /// Ф-ция возвращает экземпляр потока с уже загруженным файлом для последующего посимвольного чтения.
    /// </summary>
    /// <param name="fileFullPath">Полный путь до файла для чтения</param>
    /// <returns>Поток для последующего чтения.</returns>
    private IReadOnlyStream GetInputStream(string fileFullPath)
    {
        return new ReadOnlyStream(fileFullPath);
    }

    /// <summary>
    /// Ф-ция считывающая из входящего потока все буквы, и возвращающая коллекцию статистик вхождения каждой буквы.
    /// Статистика РЕГИСТРОЗАВИСИМАЯ!
    /// </summary>
    /// <param name="stream">Стрим для считывания символов для последующего анализа</param>
    /// <returns>Коллекция статистик по каждой букве, что была прочитана из стрима.</returns>
    private async Task<IList<LetterStats>> FillSingleLetterStats(IReadOnlyStream stream)
    {
        var stats = new List<LetterStats>();

        stream.ResetPositionToStart();
        while (!stream.IsEof)
        {
            var c = await stream.ReadNextChar();
            var str = c.ToString();

            if (c is '\n' or '\r' or ' ' or '\0')
            {
                continue;
            }

            var index = stats.FindIndex(s => s.Letter == str);

            if (index == -1)
            {
                var newStat = IncStatistic(new LetterStats
                {
                    Letter = str
                });
                stats.Add(newStat);
                continue;
            }

            var stat = IncStatistic(stats[index]);
            stats[index] = stat;
        }

        return stats;
    }

    /// <summary>
    /// Ф-ция считывающая из входящего потока все буквы, и возвращающая коллекцию статистик вхождения парных букв.
    /// В статистику должны попадать только пары из одинаковых букв, например АА, СС, УУ, ЕЕ и т.д.
    /// Статистика - НЕ регистрозависимая!
    /// </summary>
    /// <param name="stream">Стрим для считывания символов для последующего анализа</param>
    /// <returns>Коллекция статистик по каждой букве, что была прочитана из стрима.</returns>
    private async Task<IList<LetterStats>> FillDoubleLetterStats(IReadOnlyStream stream)
    {
        stream.ResetPositionToStart();
        while (!stream.IsEof)
        {
            char c = await stream.ReadNextChar();
            // TODO : заполнять статистику с использованием метода IncStatistic. Учёт букв - НЕ регистрозависимый.
        }

        //return ???;

        throw new NotImplementedException();
    }

    /// <summary>
    /// Ф-ция перебирает все найденные буквы/парные буквы, содержащие в себе только гласные или согласные буквы.
    /// (Тип букв для перебора определяется параметром charType)
    /// Все найденные буквы/пары соответствующие параметру поиска - удаляются из переданной коллекции статистик.
    /// </summary>
    /// <param name="letters">Коллекция со статистиками вхождения букв/пар</param>
    /// <param name="charType">Тип букв для анализа</param>
    private void RemoveCharStatsByType(IList<LetterStats> letters, CharType charType)
    {
        // TODO : Удалить статистику по запрошенному типу букв.
        switch (charType)
        {
            case CharType.Consonants:
                break;
            case CharType.Vowel:
                break;
        }
    }

    /// <summary>
    /// Ф-ция выводит на экран полученную статистику в формате "{Буква} : {Кол-во}"
    /// Каждая буква - с новой строки.
    /// Выводить на экран необходимо предварительно отсортировав набор по алфавиту.
    /// В конце отдельная строчка с ИТОГО, содержащая в себе общее кол-во найденных букв/пар
    /// </summary>
    /// <param name="letters">Коллекция со статистикой</param>
    private void PrintStatistic(IEnumerable<LetterStats> letters)
    {
        // TODO : Выводить на экран статистику. Выводить предварительно отсортировав по алфавиту!
        throw new NotImplementedException();
    }

    /// <summary>
    /// Метод увеличивает счётчик вхождений по переданной структуре.
    /// </summary>
    /// <param name="letterStats"></param>
    private LetterStats IncStatistic(LetterStats letterStats)
    {
        letterStats.Count++;
        return letterStats;
    }
}