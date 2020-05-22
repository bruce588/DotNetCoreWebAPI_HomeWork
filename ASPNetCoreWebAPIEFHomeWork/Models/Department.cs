﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASPNetCoreWebAPIEFHomeWork.Models
{
    [Table("Department")]
    public partial class Department
    {
        public Department()
        {
            Courses = new HashSet<Course>();
        }

        [Key]
        [Column("DepartmentID")]
        public int DepartmentId { get; set; }
        [StringLength(50)]
        public string Name { get; set; }
        [Column(TypeName = "money")]
        public decimal Budget { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime StartDate { get; set; }
        [Column("InstructorID")]
        public int? InstructorId { get; set; }
        [Required]
        public byte[] RowVersion { get; set; }

        [ForeignKey(nameof(InstructorId))]
        [InverseProperty(nameof(Person.Departments))]
        public virtual Person Instructor { get; set; }
        [InverseProperty(nameof(Course.Department))]
        public virtual ICollection<Course> Courses { get; set; }

        //新建立的資料欄位
        [Column(TypeName = "datetime")]
        public DateTime? DateModified { get; set; }

        [Column("IsDeleted", TypeName = "bit")]
        public bool IsDeleted { get; set; }
    }
}