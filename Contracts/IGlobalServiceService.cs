namespace SmartList.Contracts
{
    public interface IGlobalServiceService
    {
        Task<List<MetadataDto>> GetCompanyList();
        Task<List<MetadataDto>> GetCategoryList();
       
    }
}
