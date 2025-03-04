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
        ExpectGoodResult(result, "7 degrees");
    }

    [TestMethod]
    public void Get_WithInvalidString_ReturnsBadRequest()
    {
        var result = _controller.Get("invalid");
        ExpectBadResult(result, "Invalid time format use H:m, HH:mm or combination");
    }

    [TestMethod]
    public void Get_WithInvalidHourString_ReturnsBadRequest()
    {
        var result = _controller.Get("25:25");
        ExpectBadResult(result, "Invalid hour format, can't be greater than 24");
    }

    [TestMethod]
    public void Get_WithInvalidMinuteString_ReturnsBadRequest()
    {
        var result = _controller.Get("5:65");
        ExpectBadResult(result, "Invalid minute format, can't be greater than 60");
    }

    [TestMethod]
    public void Get_WithValidInts_ReturnsCorrectAngle()
    {
        _mockService.Setup(s => s.Calculate(3, 15)).Returns(10);

        var result = _controller.Get(3, 15);
        ExpectGoodResult(result, "10 degrees");
    }

    [TestMethod]
    public void Get_WithInvalidHourInt_ReturnsBadRequest()
    {
        var result = _controller.Get(25, 25);
        ExpectBadResult(result, "Invalid hour format, can't be greater than 24");
    }

    [TestMethod]
    public void Get_WithInvalidMinuteInt_ReturnsBadRequest()
    {
        var result = _controller.Get(5, 65);
        ExpectBadResult(result, "Invalid minute format, can't be greater than 60");
    }

    private void ExpectBadResult(ActionResult<string> result, string message) 
    {
        Xunit.Assert.NotNull(result.Result);
        Xunit.Assert.IsType<BadRequestObjectResult>(result.Result);

        var badResult = result.Result as BadRequestObjectResult;

        Xunit.Assert.NotNull(badResult);
        Xunit.Assert.Equal(400, badResult.StatusCode);
        Xunit.Assert.Equal(message, badResult.Value);
    }

    private void ExpectGoodResult(ActionResult<string> result, string value) 
    {
        Xunit.Assert.NotNull(result.Result);
        Xunit.Assert.IsType<OkObjectResult>(result.Result);

        var okResult = result.Result as OkObjectResult;

        Xunit.Assert.NotNull(okResult);
        Xunit.Assert.Equal(200, okResult.StatusCode);
        Xunit.Assert.Equal(value, okResult.Value);
    }
}