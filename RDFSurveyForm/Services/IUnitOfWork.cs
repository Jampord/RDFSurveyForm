using RDFSurveyForm.Data;
using RDFSurveyForm.DataAccessLayer.Interface;
using RDFSurveyForm.DataAccessLayer.IR_Setup.Interface;
using RDFSurveyForm.DataAccessLayer.IR_Unit_Subunit.Interface;

namespace RDFSurveyForm.Services
{
    public interface IUnitOfWork
    {
        IUserRepository Customer {  get; }

        IRoleRepository CRole { get; }

        IDepartmentRepository Department { get; }
        IBranchRepository Branches { get; }
        IGroupRepository Groups { get; }
        ICategoryRepository Category { get; }
        IGroupSurveyRepository GroupSurvey { get; }
        IUnitRepository Unit { get; }
        ISubunitRepository Subunit { get; }

        Task CompleteAsync();

    }
}
