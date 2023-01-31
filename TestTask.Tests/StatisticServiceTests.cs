namespace TestTask.Tests;

public class StatisticServiceTests
{
    private const string FILE_NAME = "Text.txt";

    [Fact]
    public async Task FillSingleLetterStatsCheckForDesiredBehavior()
    {
        var service = new StatisticService();
        var inputStream = new ReadOnlyStream(FILE_NAME);
        var stats = await service.FillSingleLetterStats(inputStream); 
        inputStream.Close();
        
        var testStats = GetStatsList();
        Assert.Equal(testStats, stats);
    }

    [Fact]
    public void File_Exists_ReturnsTrue_ForExistingFile()
    {
        // Act
        var result = File.Exists(FILE_NAME);

        // Assert
        Assert.True(result);
    }
    
    private IList<LetterStats> GetStatsList()
    {
        var stats = new List<LetterStats>();

        for (var c = 'А'; c <= 'Я'; c++)
        {
            stats.Add(new LetterStats()
            {
                Letter = c.ToString(),
                Count = 1
            });
        }

        for (var c = 'а'; c <= 'е'; c++)
        {
            stats.Add(new LetterStats()
            {
                Letter = c.ToString(),
                Count = 1
            });
        }
        
        stats[0] = new LetterStats()
        {
            Letter = "А",
            Count = 2
        };
        stats[1] = new LetterStats()
        {
            Letter = "Б",
            Count = 2
        };
        stats[2] = new LetterStats()
        {
            Letter = "В",
            Count = 2
        };
        stats[3] = new LetterStats()
        {
            Letter = "Г",
            Count = 2
        };
        stats[4] = new LetterStats()
        {
            Letter = "Д",
            Count = 2
        };
        
        return stats;
    }
}