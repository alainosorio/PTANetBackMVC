namespace Retailer.Service.UnitTest;

public class StorageServiceTests
{
    private readonly Mock<RetailerDbContext> _dbContextMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly StorageService _storageService;

    public StorageServiceTests()
    {
        var options = new DbContextOptionsBuilder<RetailerDbContext>().Options;

        _dbContextMock = new Mock<RetailerDbContext>(options);
        _mapperMock = new Mock<IMapper>();
        _storageService = new StorageService(_dbContextMock.Object, _mapperMock.Object);
    }

    [Fact]
    public async Task GetRetailerById_WithValidId_ShouldReturnRetail()
    {
        // Arrange
        var id = "ES-Test Code";
        var retailEntity = new Retail { Id = id, Name = "Test Retail", Code = "Test Code", CodingScheme = "Test Code Scheme", Country = "ES" };
        var retailModel = new Client.Model.Retail { Name = "Test Retail", Code = "Test Code", CodingScheme = "Test Code Scheme", Country = "ES" };

        _dbContextMock.Setup(db => db.Retails.FindAsync(id)).ReturnsAsync(retailEntity);
        _mapperMock.Setup(mapper => mapper.Map<Client.Model.Retail>(retailEntity)).Returns(retailModel);

        // Act
        var result = await _storageService.GetRetailerById(id);

        // Assert
        result.Should().BeEquivalentTo(retailModel);
    }
}
