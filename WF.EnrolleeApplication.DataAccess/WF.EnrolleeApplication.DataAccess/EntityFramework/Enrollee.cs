namespace WF.EnrolleeApplication.DataAccess.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Enrollee")]
    public partial class Enrollee
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Enrollee()
        {
            Assessment = new HashSet<Assessment>();
            AtributeForEnrollee = new HashSet<AtributeForEnrollee>();
            PriorityOfSpeciality = new HashSet<PriorityOfSpeciality>();
        }

        public int EnrolleeId { get; set; }

        public int SpecialityId { get; set; }

        public int CitizenshipId { get; set; }

        public int CountryId { get; set; }

        public int AreaId { get; set; }

        public int DistrictId { get; set; }

        public int SettlementTypeId { get; set; }

        public int StreetTypeId { get; set; }

        public int DocumentId { get; set; }

        public int SchoolTypeId { get; set; }

        public int ForeignLanguageId { get; set; }

        public int ReasonForAddmissionId { get; set; }

        public int StateTypeId { get; set; }

        public int EmployeeId { get; set; }

        public int FinanceTypeId { get; set; }

        public int? DecreeId { get; set; }

        public int? SecondarySpecialityId { get; set; }

        public int? TargetWorkPlaceId { get; set; }

        [Required]
        [StringLength(255)]
        public string RuSurname { get; set; }

        [Required]
        [StringLength(255)]
        public string RuName { get; set; }

        [Required]
        [StringLength(255)]
        public string RuPatronymic { get; set; }

        [Required]
        [StringLength(255)]
        public string BlrSurname { get; set; }

        [Required]
        [StringLength(255)]
        public string BlrName { get; set; }

        [Required]
        [StringLength(255)]
        public string BlrPatronymic { get; set; }

        [Required]
        [StringLength(10)]
        public string Gender { get; set; }

        [Column(TypeName = "date")]
        public DateTime DateOfBirthday { get; set; }

        [StringLength(255)]
        public string FatherFullname { get; set; }

        public string FatherAddress { get; set; }

        [StringLength(255)]
        public string MotherFullname { get; set; }

        public string MotherAddress { get; set; }

        [Required]
        [StringLength(255)]
        public string SettlementName { get; set; }

        public int SettlementIndex { get; set; }

        [Required]
        [StringLength(255)]
        public string StreetName { get; set; }

        [Required]
        [StringLength(50)]
        public string NumberHouse { get; set; }

        [StringLength(50)]
        public string NumberFlat { get; set; }

        [Required]
        [StringLength(255)]
        public string HomePhone { get; set; }

        [Required]
        [StringLength(255)]
        public string MobilePhone { get; set; }

        [Required]
        [StringLength(10)]
        public string DocumentSeria { get; set; }

        [Required]
        [StringLength(25)]
        public string DocumentNumber { get; set; }

        [Column(TypeName = "date")]
        public DateTime DocumentDate { get; set; }

        [Required]
        public string DocumentWhoGave { get; set; }

        [Required]
        [StringLength(255)]
        public string DocumentPersonalNumber { get; set; }

        [Required]
        [StringLength(255)]
        public string SchoolName { get; set; }

        [Required]
        [StringLength(10)]
        public string SchoolYear { get; set; }

        [Required]
        public string SchoolAddress { get; set; }

        public bool IsBRSM { get; set; }

        public int NumberOfDeal { get; set; }

        [Column(TypeName = "date")]
        public DateTime DateDeal { get; set; }

        [Column(TypeName = "date")]
        public DateTime StateDateChange { get; set; }

        [Required]
        public string PersonInCharge { get; set; }

        [StringLength(255)]
        public string WorkPlace { get; set; }

        [StringLength(255)]
        public string WorkPost { get; set; }

        [StringLength(10)]
        public string Seniority { get; set; }

        [StringLength(10)]
        public string CurrentNumberCurs { get; set; }

        public string CurrentUniversity { get; set; }

        public string CurrentSpeciality { get; set; }

        [StringLength(255)]
        public string AttestatEstimationString { get; set; }

        [StringLength(255)]
        public string DiplomPtuEstimationString { get; set; }

        [StringLength(255)]
        public string DiplomSusEstimationString { get; set; }

        public int? BeforeEnrollSpecialityId { get; set; }

        public int? BeforeEnrollNumberOfDeal { get; set; }

        public virtual Area Area { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Assessment> Assessment { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AtributeForEnrollee> AtributeForEnrollee { get; set; }

        public virtual Citizenship Citizenship { get; set; }

        public virtual Country Country { get; set; }

        public virtual Decree Decree { get; set; }

        public virtual District District { get; set; }

        public virtual Document Document { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual TypeOfFinance TypeOfFinance { get; set; }

        public virtual ForeignLanguage ForeignLanguage { get; set; }

        public virtual ReasonForAddmission ReasonForAddmission { get; set; }

        public virtual TypeOfSchool TypeOfSchool { get; set; }

        public virtual SecondarySpeciality SecondarySpeciality { get; set; }

        public virtual TypeOfSettlement TypeOfSettlement { get; set; }

        public virtual Speciality Speciality { get; set; }

        public virtual TypeOfState TypeOfState { get; set; }

        public virtual TypeOfStreet TypeOfStreet { get; set; }

        public virtual TargetWorkPlace TargetWorkPlace { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PriorityOfSpeciality> PriorityOfSpeciality { get; set; }
    }
}
