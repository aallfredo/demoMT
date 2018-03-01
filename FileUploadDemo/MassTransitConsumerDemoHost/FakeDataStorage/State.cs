using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MassTransitConsumerDemoHost.FakeDataStorage
{
    public static class State
    {
        public static List<ProjectState> ProjectsStatus = new List<ProjectState>();

        public static bool FakeErrors { set; get; }

        public static bool SimulateNonPassingValidations { set; get; }
    }

    public class ProjectState
    {
        public ProjectState(int expectedTotalValidations, string projectId)
        {
            ProjectId = projectId;
            ValidationErrorMessages = new List<string>();
            HasTerminated = false;
            ValidationPhasePassed = false;
            ExpectedTotalValidations = expectedTotalValidations;
        }

        public int ExpectedTotalValidations { private set; get; }

        public int _validationsPassed { private set; get; }

        public int _validationsFailed { private set; get; }

        public string ProjectId { private set; get; }

        public bool HasTerminated { set; get; }

        public bool ValidationPhasePassed { set; get; }

        public List<string> ValidationErrorMessages { private set; get; }

        internal void ValidationFailed()
        {
            _validationsFailed++;
            if (!HasTerminated)
            {
                if (_validationsPassed + _validationsFailed == ExpectedTotalValidations)
                {
                    HasTerminated = true;
                    ValidationPhasePassed = true;
                }
            }
        }

        public void ValidationPassed() {
            _validationsPassed++;
            if (_validationsPassed + _validationsFailed == ExpectedTotalValidations)
            {
                HasTerminated = true;
                ValidationPhasePassed = true;
            }
        }


    }
}
