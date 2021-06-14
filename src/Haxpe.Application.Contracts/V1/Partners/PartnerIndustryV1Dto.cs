using System.ComponentModel.DataAnnotations;

namespace Haxpe.V1.Partners
{
    public class PartnerIndustryV1Dto
    {
        [Required]
        public int IndustryId { get; set; }
    }
}