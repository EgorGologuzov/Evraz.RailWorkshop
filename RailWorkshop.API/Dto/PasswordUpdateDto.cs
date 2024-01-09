namespace RailWorkshop.API.Dto
{
    public class PasswordUpdateDto
    {
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public Guid EmployeeId { get; set; }
    }
}