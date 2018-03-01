using Messages.DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messages.Events
{
    public interface IProcessedFileRowCreated
    {
        Guid EventId { get; }
        string ProjectId { get; }
        ProcessedDataRow RowData { get; }
    }

    public class ProcessedFileRowCreated : IProcessedFileRowCreated
    {
        public ProcessedFileRowCreated(string projectID, ProcessedDataRow rowData)
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

        public ProcessedDataRow RowData
        {
            get;
            private set;
        }
    }
}
