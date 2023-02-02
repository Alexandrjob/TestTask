namespace TestTask.Tests;

public class ReadOnlyStreamTests
{
    private const string FILE_NAME_SINGLE = "TextSingle.txt";
    private const string FILE_NAME_DOUBLE = "TextDouble.txt";

    [Fact]
    public async Task CheckSingleFileReturnChar()
    {
        var inputStream = new ReadOnlyStream(FILE_NAME_SINGLE);
        inputStream.ResetPositionToStart();
        
        var c = await inputStream.ReadNextChar();
        inputStream.Close();

        Assert.Equal('А', c);
    }
    
    [Fact]
    public async Task CheckDoubleFileReturnChar()
    {
        var inputStream = new ReadOnlyStream(FILE_NAME_DOUBLE);
        inputStream.ResetPositionToStart();
        
        var c = await inputStream.ReadNextChar();
        inputStream.Close();

        Assert.Equal('А', c);
    }
}