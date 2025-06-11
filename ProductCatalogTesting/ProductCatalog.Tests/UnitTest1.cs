using ProductCatalogAPI.Services;
using ProductCatalogAPI.Controllers;
using Moq;

namespace ProductCatalog.Tests;

public class Tests
{
    private Mock<ICategoryService> _categoryServiceMock;
    private ProductCatalogController _controller;

    [SetUp]
    public void Setup()
    {
        _categoryServiceMock = new Mock<ICategoryService>();
        _controller = new ProductCatalogController(_categoryServiceMock.Object);
    }

    [Test]
    [TestCase(-1)]
    public void GetCategoriesByIdAsync_InvalidId_ReturnsInvalidId(int id)
    {

    }
}