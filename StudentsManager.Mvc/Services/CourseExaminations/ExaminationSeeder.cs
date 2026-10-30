using StudentsManager.Mvc.Domain.Entities;

namespace StudentsManager.Mvc.Services.CourseExaminations
{
    public class ExaminationSeeder
    {
        public static readonly CourseExaminationSetting FirstSetting = new()
        {
            Type = CourseExaminationConstants.FirstType,
            Enabled = true
        };

        public static readonly CourseExaminationSetting SecondSetting = new()
        {
            Type = CourseExaminationConstants.SecondType,
            Enabled = false
        };

        public static readonly List<CourseExaminationSetting> Settings = new()
        {
            FirstSetting, SecondSetting
        };

        public static readonly CourseExamination ExaminationVariantA = new()
        {
            Type = CourseExaminationConstants.FirstType,
            ResourceUrl = "https://github.com/profjordanov/storage/blob/main/variantA.pdf",
            ExtraResourceUrl = "https://github.com/profjordanov/storage/raw/main/variantA.zip"
        };

        public static readonly CourseExamination ExaminationVariantC = new()
        {
            Type = CourseExaminationConstants.FirstType,
            ResourceUrl = "https://github.com/profjordanov/storage/blob/main/variantC.pdf",
            ExtraResourceUrl = "https://github.com/profjordanov/storage/raw/main/variantC.zip"
        };

        public static readonly CourseExamination SecondExaminationVariantX = new()
        {
            Type = CourseExaminationConstants.SecondType,
            ResourceUrl = "https://github.com/",
            ExtraResourceUrl = "https://github.com/"
        };

        public static readonly List<CourseExamination> CourseExaminations = new()
        {
            ExaminationVariantA, ExaminationVariantC,
            SecondExaminationVariantX
        };
    }
}