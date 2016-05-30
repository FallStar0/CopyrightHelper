//===================================================
//  Copyright @  Thpower.com 2016
//  作者：Fallstar
//  时间：2016-05-30 14:27:11
//  说明：
//===================================================
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CopyrightHelper.Core;

namespace UnitTest
{
    [TestClass]
    public class CopyrightCoreTest
    {
        [TestMethod]
        public void TestLoad()
        {
            CopyrightCore.Load();
        }

        [TestMethod]
        public void TestSave()
        {
            CopyrightCore.Save();
        }

        [TestMethod]
        public void TestGetContentByExtension()
        {
            CopyrightCore.Load();
            var txt = CopyrightCore.GetContentByExtension(".cs");
            Assert.IsNotNull(txt);

        }
    }
}
