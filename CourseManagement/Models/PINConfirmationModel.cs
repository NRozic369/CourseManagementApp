namespace CourseManagement.Models
{
    public class PINConfirmationModel
    {
        public int Id { get; set; }
        public bool IsDialogOk { get; set; }
        public string? InsertedPIN { get; set; }
        public string AfterDialogActionType { get; set; } = null!;

    }
}
