using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;

namespace TTCBDD.ExcelReader
{
    public class ExcelReaderHelper
    {
        private static IDictionary<string, IExcelDataReader> _cache;
        private static FileStream stream;
        private static IExcelDataReader reader;

        private static IExcelDataReader GetExcelReader(string XlPath,string sheetName)
        {
            if (_cache.ContainsKey(sheetName))
            {
                reader = _cache[sheetName];
            }
            else
            {
                stream = new FileStream(XlPath, FileMode.Open, FileAccess.Read);
                reader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                _cache.Add(sheetName, reader);
            }
            return reader;
        }

        public static int GetTotalRows(string XlPath,string sheetName)
        {
            IExcelDataReader _reader = GetExcelReader(XlPath,sheetName);
            return _reader.AsDataSet().Tables[sheetName].Rows.Count;
        }
        static ExcelReaderHelper()
        {
            _cache = new Dictionary<string, IExcelDataReader>();
        }

        public static object GetCellData(string XlPath,string sheetName,int row,int column)
        {
            IExcelDataReader _reader = GetExcelReader(XlPath, sheetName);
            DataTable table = _reader.AsDataSet().Tables[sheetName];
            //return table.Rows[row][column].ToString();
            return GetData(table.Rows[row][column].GetType(), table.Rows[row][column]);
        }
        private static object GetData(Type type,object data)
        {
            switch (type.Name)
            {
                case "String":
                    return data.ToString();
                case "Double":
                    return Convert.ToDouble(data);
                case "DateTime":
                    return Convert.ToDateTime(data);
                case "Int":
                    return Convert.ToInt32(data);
                default:
                    return data.ToString();
            }
        }
    }
}
