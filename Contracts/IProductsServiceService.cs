namespace SmartList.Contracts
{
    public interface IProductsServiceService
    {
        Task<List<ProductDto>> GetProductList();
        Task<ProductDto> AddProduct(ProductDto productDto);
        //Task<AnswerOptionItemDto?> AddItemToList(int id,AnswerOptionItemDto answerOptionItem);
        //Task<OptionsListDto?> CreateList(OptionsListDto optionsList);
        //Task<AnswerOptionItemDto?> DeleteItemFromList( int id);
        //Task<List<OptionsListDto>> GetAllOptionsLists();
        //Task<List<AnswerOptionItemDto>> GetAllItemsToList(int id);
    }
}
