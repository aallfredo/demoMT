using Messages.DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messages.Events
{
    public interface IProjectRowsPublished
    {
        Guid EventId { get; }
        string ProjectId { get; }
        int RowNumber { get; }
    }

    public class ProjectRowsPublished : IProjectRowsPublished
    {
        public ProjectRowsPublished(string projectID, int rowNumber)
        {
            EventId = Guid.NewGuid();
            ProjectId = projectID;
            RowNumber = rowNumber;
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
     
        public int RowNumber
        {
            get;
            private set;
        }
    }
}
