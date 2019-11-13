using ExcelDataReader;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTCBDD.ExcelReader
{
    [TestFixture]
    public class TestExcelData
    {
        //[Test]

        public void TestReadExcel()
        {
            string xlpath = @"C:\Users\Ketan Naik\Desktop\Selenium\ReadData.xlsx";
            string sheetname = "Read";
            FileStream stream = new FileStream(xlpath, FileMode.Open, FileAccess.Read);
            IExcelDataReader reader = ExcelReaderFactory.CreateOpenXmlReader(stream);
            DataTable table = reader.AsDataSet().Tables["Read"];
            for (int i = 0; i < table.Rows.Count; i++)
            {
                var col = table.Rows[i];
                for (int j = 0; j < col.ItemArray.Length; j++)
                {
                    Console.WriteLine("Data : {0}", col.ItemArray[j]);
                }
            }
            //Console.WriteLine(table.Rows[0][0]);

            Console.WriteLine(ExcelReaderHelper.GetCellData(xlpath, sheetname, 1, 0));
            Console.WriteLine(ExcelReaderHelper.GetCellData(xlpath, sheetname, 1, 1));
            Console.WriteLine(ExcelReaderHelper.GetCellData(xlpath, sheetname, 1, 2));
        }
    }
}
