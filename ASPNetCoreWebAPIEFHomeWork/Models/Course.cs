﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASPNetCoreWebAPIEFHomeWork.Models
{
    [Table("Course")]
    public partial class Course
    {
        public Course()
        {
            CourseInstructors = new HashSet<CourseInstructor>();
            Enrollments = new HashSet<Enrollment>();
        }

        [Key]
        [Column("CourseID")]
        public int CourseId { get; set; }
        [StringLength(50)]
        public string Title { get; set; }
        public int Credits { get; set; }
        [Column("DepartmentID")]
        public int DepartmentId { get; set; }

        [ForeignKey(nameof(DepartmentId))]
        [InverseProperty("Courses")]
        public virtual Department Department { get; set; }
        [InverseProperty(nameof(CourseInstructor.Course))]
        public virtual ICollection<CourseInstructor> CourseInstructors { get; set; }
        [InverseProperty(nameof(Enrollment.Course))]
        public virtual ICollection<Enrollment> Enrollments { get; set; }

        //新建立的資料欄位
        [Column(TypeName = "datetime")]
        public DateTime? DateModified { get; set; }

        [Column("IsDeleted", TypeName = "bit")]
        public bool IsDeleted { get; set; }
    }
}