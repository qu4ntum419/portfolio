namespace Backend
{
    public class Attendance
    {
        public int Id { get; set; }
        public string MatricNumber { get; set; }
        public string Name { get; set; }
        public string Course { get; set; }
        public DateTime AttendanceTime { get; set; }
    }
}