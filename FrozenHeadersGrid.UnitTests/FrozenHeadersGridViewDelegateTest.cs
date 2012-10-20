using System;
using System.Drawing;
using NUnit.Framework;

namespace FrozenHeadersGrid.UnitTests
{
    [TestFixture]
    public class FrozenHeadersGridViewDelegateTest
    {
        [Test]
        public void NumberOfColumns_ReturnsZero()
        {
            Assert.AreEqual(0, CreateDelegate().NumberOfColumns(null));
        }

        [Test]
        public void NumberOfRows_ReturnsZero()
        {
            Assert.AreEqual(0, CreateDelegate().NumberOfRows(null));
        }

        [Test]
        public void TitleForColumn_ReturnsEmptyString()
        {
            Assert.AreEqual(string.Empty, CreateDelegate().TitleForColumn(null, 0));
        }

        [Test]
        public void TitleForRow_ReturnsEmptyString()
        {
            Assert.AreEqual(string.Empty, CreateDelegate().TitleForRow(null, 0));
        }

        [Test]
        public void ViewForCell_ReturnsNull()
        {
            Assert.Null(CreateDelegate().ViewForCell(null, Point.Empty));
        }

        FrozenHeadersGridViewDelegate CreateDelegate()
        {
            return new FrozenHeadersGridViewDelegate();
        }
    }
}
