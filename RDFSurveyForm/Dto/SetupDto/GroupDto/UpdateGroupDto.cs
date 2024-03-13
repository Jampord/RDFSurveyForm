﻿namespace RDFSurveyForm.Dto.SetupDto.GroupDto
{
    public class UpdateGroupDto
    {
        public int Id { get; set; }
        public string GroupName { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
    }
}