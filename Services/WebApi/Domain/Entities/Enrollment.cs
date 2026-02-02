// using WebApi.Domain.Contracts;
// using WebApi.Domain.Enums;

// namespace WebApi.Domain.Entities;

// public class Enrollment
// {
//     public Guid Id { get; }
//     public Guid StudentId { get; }
//     public Guid CourseAssignmentId { get; }
//     public EnrollmentStatus Status { get; private set; }
//     public DateTime EnrolledAt { get; private set; }
//     public DateTime? WithdrawnAt { get; private set; }
//     public virtual Student Student { get; set; } = null!;
//     public virtual CourseAssignment CourseAssignment { get; set; } = null!;

//     protected Enrollment() { }

//     private Enrollment(Guid studentId, Guid courseAssignmentId)
//     {
//         Id = Guid.NewGuid();
//         StudentId = studentId;
//         CourseAssignmentId = courseAssignmentId;
//         Status = EnrollmentStatus.Active;
//         EnrolledAt = DateTime.UtcNow;
//     }

//     public static Enrollment Create(IEnrollmentDto enrollmentDto)
//     {
//         var studentId = enrollmentDto.GetStudentId();
//         var courseAssignmentId = enrollmentDto.GetCourseAssignmentId();
//         if (studentId.Equals(Guid.Empty))
//         {
//             throw new ArgumentException("Student is required");
//         }
//         if (courseAssignmentId.Equals(Guid.Empty))
//         {
//             throw new ArgumentException("Course assignment is required");
//         }

//         return new Enrollment(studentId, courseAssignmentId);
//     }

//     public void Withdraw()
//     {
//         if (Status != EnrollmentStatus.Active)
//         {
//             throw new InvalidOperationException("Only active enrollments can be withdrawn");
//         }
//         Status = EnrollmentStatus.Withdrawn;
//         WithdrawnAt = DateTime.UtcNow;
//     }

//     public void Reactivate()
//     {
//         if (Status != EnrollmentStatus.Withdrawn)
//         {
//             throw new InvalidOperationException("Only withdrawn enrollments can be reactivated");
//         }
//         Status = EnrollmentStatus.Active;
//         WithdrawnAt = null;
//     }
// }
