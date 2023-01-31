namespace TestTask.Tests;

public class ReadOnlyStreamTests
{
    private const string FILE_NAME = "Text.txt";

    [Fact]
    public async Task CheckReturnChar()
    {
        var inputStream = new ReadOnlyStream(FILE_NAME);
        var c = await inputStream.ReadNextChar();
        inputStream.Close();

        Assert.Equal('А', c);
    }
}