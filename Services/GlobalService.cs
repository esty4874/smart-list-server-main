    namespace SmartList.Services
{
    public class GlobalService : IGlobalServiceService
    {
        private readonly SmartListContext _context;
        public GlobalService(SmartListContext context)
        {
            _context = context;
        }

        public async Task<List<MetadataDto>> GetCategoryList()
        {
            var categoryList = await _context.Categories.Select
                (category => new MetadataDto()
                {
                    Id = category.Id,
                    Name = category.Name,
                }
                ).ToListAsync();
            return categoryList;
        }

    public async Task<List<MetadataDto>> GetCompanyList()

        {
            var companyList = await _context.Companies.Select
                (company => new MetadataDto()
                {
                    Id = company.Id,
                    Name = company.Name,
                }
                ).ToListAsync();
            return companyList;
        }

      
    }
}
