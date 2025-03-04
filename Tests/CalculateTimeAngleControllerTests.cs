using Xunit;
using Moq;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using CalculateTimeAngle.Controllers;
using CalculateTimeAngle.Services;

namespace CalculateTimeAngle.Tests;

[TestClass]
public class CalculateTimeAngleControllerTests
{
    private readonly CalculateTimeAngleController _controller;
    private readonly Mock<TimeAngleService> _mockService;   

    public CalculateTimeAngleControllerTests()
    {
        _mockService = new Mock<TimeAngleService>();
        var logger = new NullLogger<CalculateTimeAngleController>();
        _controller = new CalculateTimeAngleController(logger, _mockService.Object);
    }

    [TestMethod]
    public void Get_WithValidString_ReturnsCorrectAngle()
    {
        _mockService.Setup(s => s.Calculate(3, 15)).Returns(7);

        var result = _controller.Get("03:15");

        Xunit.Assert.NotNull(result.Result);
        Xunit.Assert.IsType<OkObjectResult>(result.Result);

        var okResult = result.Result as OkObjectResult;

        Console.WriteLine($"Result Value: {okResult.Value}");

        Xunit.Assert.NotNull(okResult);
        Xunit.Assert.Equal(200, okResult.StatusCode);
        Xunit.Assert.Equal("7 degrees", okResult.Value);
    }

    [TestMethod]
    public void Get_WithInvalidString_ReturnsBadRequest()
    {
        var result = _controller.Get("invalid");

        Xunit.Assert.NotNull(result.Result);
        Xunit.Assert.IsType<BadRequestObjectResult>(result.Result);

        var badResult = result.Result as BadRequestObjectResult;

        Xunit.Assert.NotNull(badResult);
        Xunit.Assert.Equal(400, badResult.StatusCode);
        Xunit.Assert.Equal("Invalid time format use H:m, HH:mm or combination", badResult.Value);
    }
}