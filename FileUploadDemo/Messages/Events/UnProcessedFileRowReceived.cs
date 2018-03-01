using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messages.Events
{
    public interface IUnProcessedFileRowReceived
    {
        Guid EventId { get; }
        string ProjectId { get; }

        string RowData { get; }
    }

    public class UnProcessedFileRowReceived : IUnProcessedFileRowReceived
    {
        public UnProcessedFileRowReceived(string projectID, string rowData)
        {
            EventId = Guid.NewGuid();
            ProjectId = projectID;
            RowData = rowData;
        }

        public Guid EventId
        {
            get;
            private set;
        }

        public string ProjectId
        {
            get;
            private set;
        }

        public string RowData
        {
            get;
            private set;
        }
    }
}
