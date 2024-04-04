

namespace RDFSurveyForm.Dto.SetupDto.GroupSurveyDto
{
    public class AddGroupSurveyDto
    {
        //public int Id { get; set; }
        public int? GroupsId { get; set; }
        //public int? SurveyGeneratorId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }

        public int? SurveyGeneratorId { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }



        public List<UpdateSurveyScore> UpdateSurveyScores { get; set; }
        public class UpdateSurveyScore
        {
            public int Id { get; set; }
            public decimal Score { get; set; }
        }

    }
}
