using Xunit;
using Moq;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using CalculateTimeAngle.Services;

namespace CalculateTimeAngle.Tests;

[TestClass]
public class TimeAngleServiceTests
{
    private readonly TimeAngleService _service;   

    public TimeAngleServiceTests()
    {
        _service = new TimeAngleService();
    }

    [DataTestMethod]
    [DataRow(3, 0, 90)]
    [DataRow(6, 0, 180)]
    [DataRow(9, 0, 270)]
    [DataRow(0, 0, 0)]
    [DataRow(0, 15, 97.5)]
    [DataRow(0, 30, 195)]
    [DataRow(0, 45, 292.5)]
    [DataRow(0, 60, 390)] // odd
    [DataRow(3, 30, 285)]
    [DataRow(6, 15, 277.5)]
    [DataRow(9, 45, 562.5)]
    public void Get_WithValidInts_ReturnsCorrectAngle(int hour, int minute, double expected)
    {
        var result = _service.Calculate(hour, minute);
        Xunit.Assert.Equal(result, expected);
    }
}