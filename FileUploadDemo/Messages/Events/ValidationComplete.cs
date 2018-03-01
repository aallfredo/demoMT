using Messages.DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messages.Events
{
    public interface IValidationComplete
    {
        Guid EventId { get; }
        string ProjectId { get; }
        ProcessedDataRow RowData { get; }
        
        bool ValidationPassed { get; }

        bool CriticalValidation { get; }

        string ErrorMessage { get; }

    }

    public class ValidationComplete : IValidationComplete
    {
        public ValidationComplete(string projectID,
            ProcessedDataRow rowData,
            bool validationPassed,
            bool criticalValidation,
            string errorMessage)
        {
            EventId = Guid.NewGuid();
            ProjectId = projectID;
            RowData = rowData;
            ValidationPassed = validationPassed;
            CriticalValidation = criticalValidation;
            ErrorMessage = errorMessage;
        }

        public bool CriticalValidation
        {
            get;
            private set;
        }

        public string ErrorMessage
        {
            get;
            private set;
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

        public bool ValidationPassed
        {
            get;
            private set;
        }       
    }
}
