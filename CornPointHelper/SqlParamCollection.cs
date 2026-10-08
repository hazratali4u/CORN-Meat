using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace CornPointHelper
{
    public class SqlParamCollection:IDisposable
    {
        private Boolean IsDiposed = false;
        private List<SqlParameter> paramCollection;

        public SqlParamCollection() { paramCollection = new List<SqlParameter>(); }

        public List<SqlParameter> ParamCollection
        {
            get { return paramCollection; }
            set { paramCollection = value; }
        }

        public Int32 Count
        {
            get { return paramCollection.Count; }

        }

        public void Add(SqlParameter SqlParam)
        {
            paramCollection.Add(SqlParam);
        }

        public void Remove(SqlParameter SqlParam)
        {
            paramCollection.Remove(SqlParam);
        }

        public void Clear()
        {
            paramCollection = new List<SqlParameter>();
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
