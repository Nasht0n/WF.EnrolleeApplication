using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WF.EnrolleeApplication.DataAccess.EntityFramework;
using WF.EnrolleeApplication.DataAccess.Services;

namespace WF.EnrolleeApplication.App.Views
{
    public partial class frmEnrolleeCard : Form
    {
        private Faculty faculty;
        private FormOfStudy formOfStudy;
        private Speciality speciality;
        private Citizenship citizenship;
        private Document document;
        private Enrollee enrollee;
        private TypeOfSettlement typeOfSettlement;
        private TypeOfSchool typeOfSchool;
        private TypeOfStreet typeOfStreet;
        private SecondarySpeciality secondarySpeciality;
        private ForeignLanguage foreignLanguage;
        private List<IntegrationOfSpecialities> integrationOfSpecialities;

        private FacultyService facultyService;
        private FormOfStudyService formOfStudyService;
        private SpecialityService specialityService;
        private CitizenshipService citizenshipService;
        private DocumentService documentService;
        private EnrolleeService enrolleeService;
        private TypeOfSettlementService typeOfSettlementService;
        private TypeOfStreetService typeOfStreetService;
        private TypeOfSchoolService typeOfSchoolService;
        private SecondarySpecialityService secondarySpecialityService;
        private ForeignLanguageService foreignLanguageService;
        private IntegrationOfSpecialitiesService integrationOfSpecialitiesService;

        public frmEnrolleeCard()
        {
            InitializeComponent();
            InitializeDataAccessServices();
            InitializeComboBoxes();
        }

        private void InitializeComboBoxes()
        {
            InitializeFacultyComboBox();
            InitializeFormOfStudyComboBox();
            InitializeSpecialityComboBox();
            InitializeCitizenshipComboBox();
            InitializeDocumentComboBox();
            InitializeTypeOfSettlementComboBox();
            InitializeTypeOfStreetComboBox();
            InitializeTypeOfSchoolComboBox();
            InitializeSecondarySpecialityComboBox();
        }

        private void InitializeSecondarySpecialityComboBox()
        {
            cbSecondarySpeciality.SelectedValueChanged -= cbSecondarySpeciality_SelectedValueChanged;
            List<IntegrationOfSpecialities> integrationOfSpecialities = integrationOfSpecialitiesService.GetIntegrationOfSpecialities(speciality);
            List<SecondarySpeciality> secondarySpecialities = new List<SecondarySpeciality>();
            foreach (var integration in integrationOfSpecialities)
            {
                var ss = secondarySpecialityService.GetSecondarySpeciality(integration.SecondarySpecialityId);
                secondarySpecialities.Add(ss);
            }
            cbSecondarySpeciality.DataSource = secondarySpecialities;
            cbSecondarySpeciality.DisplayMember = "Fullname";
            cbSecondarySpeciality.ValueMember = "SecondarySpecialityId";
            if (secondarySpecialities.Count != 0) secondarySpeciality = secondarySpecialities[0];
            cbSecondarySpeciality.SelectedValueChanged += cbSecondarySpeciality_SelectedValueChanged;
        }

        private void InitializeTypeOfSchoolComboBox()
        {
            cbTypeOfSchool.SelectedValueChanged -= cbTypeOfSchool_SelectedValueChanged;
            var schools = typeOfSchoolService.GetTypeOfSchools();
            cbTypeOfSchool.DataSource = schools;
            cbTypeOfSchool.DisplayMember = "Name";
            cbTypeOfSchool.ValueMember = "SchoolTypeId";
            if (schools.Count != 0) typeOfSchool = schools[0];
            cbTypeOfSchool.SelectedValueChanged += cbTypeOfSchool_SelectedValueChanged;
        }

        private void InitializeTypeOfStreetComboBox()
        {
            cbTypeOfStreet.SelectedValueChanged -= cbTypeOfStreet_SelectedValueChanged;
            var streets = typeOfStreetService.GetTypeOfStreets();
            cbTypeOfStreet.DataSource = streets;
            cbTypeOfStreet.DisplayMember = "Fullname";
            cbTypeOfStreet.ValueMember = "SteetTypeId";
            if (streets.Count != 0) typeOfStreet = streets[0];
            cbTypeOfStreet.SelectedValueChanged += cbTypeOfStreet_SelectedValueChanged;
        }

        private void InitializeTypeOfSettlementComboBox()
        {
            cbTypeOfSettlement.SelectedValueChanged -= cbTypeOfSettlement_SelectedValueChanged;
            var settlements = typeOfSettlementService.GetTypeOfSettlements();
            cbTypeOfSettlement.DataSource = settlements;
            cbTypeOfSettlement.DisplayMember = "Fullname";
            cbTypeOfSettlement.ValueMember = "SettlementTypeId";
            if (settlements.Count != 0) typeOfSettlement = settlements[0];
            cbTypeOfSettlement.SelectedValueChanged += cbTypeOfSettlement_SelectedValueChanged;
        }

        private void InitializeFacultyComboBox()
        {
            cbFaculty.SelectedValueChanged -= cbFaculty_SelectedValueChanged;
            var faculties = facultyService.GetFaculties();
            cbFaculty.DataSource = faculties;
            cbFaculty.DisplayMember = "Shortname";
            cbFaculty.ValueMember = "FacultyId";
            if (faculties.Count != 0) faculty = faculties[0];
            cbFaculty.SelectedValueChanged += cbFaculty_SelectedValueChanged;
        }

        private void InitializeFormOfStudyComboBox()
        {
            cbFormOfStudy.SelectedValueChanged -= cbFormOfStudy_SelectedValueChanged;
            var formsOfStudies = formOfStudyService.GetFormOfStudies();
            cbFormOfStudy.DataSource = formsOfStudies;
            cbFormOfStudy.DisplayMember = "Fullname";
            cbFormOfStudy.ValueMember = "FormOfStudyId";
            if (formsOfStudies.Count != 0) formOfStudy = formsOfStudies[0];
            cbFormOfStudy.SelectedValueChanged += cbFormOfStudy_SelectedValueChanged;
        }

        private void InitializeSpecialityComboBox()
        {
            if (faculty != null && formOfStudy != null)
            {
                cbSpeciality.SelectedValueChanged -= cbSpeciality_SelectedValueChanged;
                var specialities = specialityService.GetSpecialities(faculty, formOfStudy);
                cbSpeciality.DataSource = specialities;
                cbSpeciality.DisplayMember = "Fullname";
                cbSpeciality.ValueMember = "SpecialityId";
                if (specialities.Count != 0) speciality = specialities[0];
                cbSpeciality.SelectedValueChanged += cbSpeciality_SelectedValueChanged;
            }
        }

        private void InitializeCitizenshipComboBox()
        {
            cbCitizenship.SelectedValueChanged -= cbCitizenship_SelectedValueChanged;
            var citizenships = citizenshipService.GetCitizenships();
            cbCitizenship.DataSource = citizenships;
            cbCitizenship.DisplayMember = "Fullname";
            cbCitizenship.ValueMember = "CitizenshipId";
            if (citizenships.Count != 0) citizenship = citizenships[0];
            cbCitizenship.SelectedValueChanged += cbCitizenship_SelectedValueChanged;
        }

        private void InitializeDocumentComboBox()
        {
            cbDocument.SelectedValueChanged -= cbDocument_SelectedValueChanged;
            var documents = documentService.GetDocuments();
            cbDocument.DataSource = documents;
            cbDocument.DisplayMember = "Name";
            cbDocument.ValueMember = "DocumentId";
            if (documents.Count != 0) document = documents[0];
            cbDocument.SelectedValueChanged += cbDocument_SelectedValueChanged;
        }

        private void InitializeDataAccessServices()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["EnrolleeContext"].ConnectionString;
            facultyService = new FacultyService(connectionString);
            formOfStudyService = new FormOfStudyService(connectionString);
            specialityService = new SpecialityService(connectionString);
            enrolleeService = new EnrolleeService(connectionString);
            citizenshipService = new CitizenshipService(connectionString);
            documentService = new DocumentService(connectionString);
            typeOfSettlementService = new TypeOfSettlementService(connectionString);
            typeOfStreetService = new TypeOfStreetService(connectionString);
            typeOfSchoolService = new TypeOfSchoolService(connectionString);
            secondarySpecialityService = new SecondarySpecialityService(connectionString);
            foreignLanguageService = new ForeignLanguageService(connectionString);
            integrationOfSpecialitiesService = new IntegrationOfSpecialitiesService(connectionString);
    }

        private void btChooseSertificate_Click(object sender, EventArgs e)
        {
            frmChooseSertificate chooseSertificateCard = new frmChooseSertificate();
            DialogResult chooseSertificateResult = chooseSertificateCard.ShowDialog();
            if(chooseSertificateResult == DialogResult.OK)
            {

            }
        }

        private void cbFaculty_SelectedValueChanged(object sender, EventArgs e)
        {
            if(cbFaculty.SelectedValue != null)
            {
                int id = (int)cbFaculty.SelectedValue;
                faculty = facultyService.GetFaculty(id);
                InitializeSpecialityComboBox();
            }
        }

        private void cbFormOfStudy_SelectedValueChanged(object sender, EventArgs e)
        {
            if(cbFormOfStudy.SelectedValue != null)
            {
                int id = (int)cbFormOfStudy.SelectedValue;
                formOfStudy = formOfStudyService.GetFormOfStudy(id);
                InitializeSpecialityComboBox();
            }
        }

        private void cbSpeciality_SelectedValueChanged(object sender, EventArgs e)
        {
            if(cbSpeciality.SelectedValue != null)
            {
                int id = (int)cbSpeciality.SelectedValue;
                speciality = specialityService.GetSpeciality(id);
                InitializeSecondarySpecialityComboBox();
            }
        }

        private void cbCitizenship_SelectedValueChanged(object sender, EventArgs e)
        {
            if(cbCitizenship.SelectedValue != null)
            {
                int id = (int)cbCitizenship.SelectedValue;
                citizenship = citizenshipService.GetCitizenship(id);
            }
        }

        private void cbDocument_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cbDocument.SelectedValue != null)
            {
                int id = (int)cbDocument.SelectedValue;
                document = documentService.GetDocument(id);
            }
        }

        private void cbTypeOfSettlement_SelectedValueChanged(object sender, EventArgs e)
        {
            if(cbTypeOfSettlement.SelectedValue != null)
            {
                int id = (int)cbTypeOfSettlement.SelectedValue;
                typeOfSettlement = typeOfSettlementService.GetTypeOfSettlement(id);
            }
        }

        private void cbTypeOfStreet_SelectedValueChanged(object sender, EventArgs e)
        {
            if(cbTypeOfStreet.SelectedValue != null)
            {
                int id = (int)cbTypeOfStreet.SelectedValue;
                typeOfStreet = typeOfStreetService.GetTypeOfStreet(id);
            }
        }

        private void cbTypeOfSchool_SelectedValueChanged(object sender, EventArgs e)
        {
            if(cbTypeOfSchool.SelectedValue != null)
            {
                int id = (int)cbTypeOfSchool.SelectedValue;
                typeOfSchool = typeOfSchoolService.GetTypeOfSchool(id);
            }
        }

        private void cbSecondarySpeciality_SelectedValueChanged(object sender, EventArgs e)
        {
            if(cbSecondarySpeciality.SelectedValue != null)
            {
                int id = (int)cbSecondarySpeciality.SelectedValue;
                secondarySpeciality = secondarySpecialityService.GetSecondarySpeciality(id);
            }
        }

        private void cbForeignLanguage_SelectedValueChanged(object sender, EventArgs e)
        {
            if(cbForeignLanguage.SelectedValue != null)
            {
                int id = (int)cbForeignLanguage.SelectedValue;
                foreignLanguage = foreignLanguageService.GetForeignLanguage(id);
            }
        }
    }
}
