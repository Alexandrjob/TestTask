namespace TestTask.Tests;

public class StatisticServiceTests
{
    private const string FILE_NAME_SINGLE = "TextSingle.txt";
    private const string FILE_NAME_DOUBLE = "TextDouble.txt";

    private const string VOWELS = "аеёиоуыэюя";
    private const string CONSONANTS = "бвгджзйклмнпрстфхцчшщьъ";

    [Fact]
    public async Task FillSingleLetterStatsCheckForDesiredBehavior()
    {
        var service = new StatisticService();
        var inputStream = new ReadOnlyStream(FILE_NAME_SINGLE);
        var stats = await service.FillSingleLetterStats(inputStream);
        inputStream.Close();

        var testStats = GetStatsSingleLetterList();
        Assert.Equal(testStats, stats);
    }

    [Fact]
    public async Task FillDoubleLetterStatsCheckForDesiredBehavior()
    {
        var service = new StatisticService();
        var inputStream = new ReadOnlyStream(FILE_NAME_DOUBLE);
        var stats = await service.FillDoubleLetterStats(inputStream);
        inputStream.Close();

        var testStats = GetStatsDoubleLetterList();
        Assert.Equal(testStats, stats);
    }

    [Fact]
    public async Task RemoveSingleLettersVowels()
    {
        var service = new StatisticService();
        var inputStream = new ReadOnlyStream(FILE_NAME_SINGLE);
        var result = await service.FillSingleLetterStats(inputStream);

        var stats = service.RemoveCharStatsByType(result, CharType.Vowel);

        var testStats = GetSingleLettersWithout(VOWELS);
        Assert.Equal(testStats, stats);
    }

    [Fact]
    public async Task RemoveSingleLettersConsonants()
    {
        var service = new StatisticService();
        var inputStream = new ReadOnlyStream(FILE_NAME_SINGLE);
        var result = await service.FillSingleLetterStats(inputStream);

        var stats = service.RemoveCharStatsByType(result, CharType.Consonants);

        var testStats = GetSingleLettersWithout(CONSONANTS);
        Assert.Equal(testStats, stats);
    }

    [Fact]
    public async Task RemoveDoubleLettersVowels()
    {
        var service = new StatisticService();
        var inputStream = new ReadOnlyStream(FILE_NAME_DOUBLE);
        var result = await service.FillDoubleLetterStats(inputStream);

        var stats = service.RemoveCharStatsByType(result, CharType.Vowel);

        var testStats = GetDoubleLettersWithout(VOWELS);
        Assert.Equal(testStats, stats);
    }

    [Fact]
    public async Task RemoveDoubleLettersConsonants()
    {
        var service = new StatisticService();
        var inputStream = new ReadOnlyStream(FILE_NAME_DOUBLE);
        var result = await service.FillDoubleLetterStats(inputStream);

        var stats = service.RemoveCharStatsByType(result, CharType.Consonants);

        var testStats = GetDoubleLettersWithout(CONSONANTS);
        Assert.Equal(testStats, stats);
    }

    [Fact]
    public void File_Exists_ReturnsTrue_ForExistingFile()
    {
        // Act
        var result = File.Exists(FILE_NAME_SINGLE);

        // Assert
        Assert.True(result);
    }

    private IList<LetterStats> GetStatsSingleLetterList()
    {
        var stats = new List<LetterStats>();

        //А - Я
        for (var c = 'А'; c <= 'Я'; c++)
        {
            stats.Add(new LetterStats()
            {
                Letter = c.ToString(),
                Count = 1
            });
        }

        //а - е
        for (var c = 'а'; c <= 'е'; c++)
        {
            stats.Add(new LetterStats()
            {
                Letter = c.ToString(),
                Count = 1
            });
        }

        //а - д
        for (var i = 0; i < 5; i++)
        {
            stats[i] = new LetterStats()
            {
                Letter = ((char) ('А' + i)).ToString(),
                Count = 2
            };
        }

        return stats;
    }

    private IList<LetterStats> GetStatsDoubleLetterList()
    {
        var stats = new List<LetterStats>();

        //аа - цц
        for (var c = 'а'; c <= 'ц'; c++)
        {
            stats.Add(new LetterStats()
            {
                Letter = c.ToString() + c.ToString(),
                Count = 1
            });
        }

        //аа - ее
        for (var i = 0; i <= 5; i++)
        {
            stats[i] = new LetterStats()
            {
                Letter = ((char) ('а' + i)).ToString() + ((char) ('а' + i)).ToString(),
                Count = 2
            };
        }

        return stats;
    }

    private List<LetterStats> GetSingleLettersWithout(string lettersDelete)
    {
        var stats = GetStatsSingleLetterList();

        var newStats = new List<LetterStats>();
        foreach (var stat in stats)
        {
            if (!lettersDelete.Contains(stat.Letter.ToLower()))
            {
                newStats.Add(stat);
            }
        }

        return newStats;
    }

    private List<LetterStats> GetDoubleLettersWithout(string lettersDelete)
    {
        var stats = GetStatsDoubleLetterList();

        var newStats = new List<LetterStats>();
        foreach (var stat in stats)
        {
            var letter = stat.Letter.ToLower();

            if (letter.Length > 1)
            {
                letter = letter.Substring(1);
            }

            if (!lettersDelete.Contains(letter))
            {
                newStats.Add(stat);
            }
        }

        return newStats;
    }
}