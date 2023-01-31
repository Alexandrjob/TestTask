namespace TestTask.Tests;

public class StatisticServiceTests
{
    private const string FILE_NAME = "Text.txt";
    private const string FILE_NAME_DOUBLE = "TextDouble.txt";

    [Fact]
    public async Task FillSingleLetterStatsCheckForDesiredBehavior()
    {
        var service = new StatisticService();
        var inputStream = new ReadOnlyStream(FILE_NAME);
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
    public void File_Exists_ReturnsTrue_ForExistingFile()
    {
        // Act
        var result = File.Exists(FILE_NAME);

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
                Letter = ((char)('А' + i)).ToString(),
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
                Letter = ((char)('а' + i)).ToString() + ((char)('а' + i)).ToString(),
                Count = 2
            };
        }
        
        return stats;
    }
}