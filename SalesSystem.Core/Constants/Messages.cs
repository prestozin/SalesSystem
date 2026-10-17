
namespace SalesSystem.Core.Constants;

public class Messages
{
    public const string PRODUCT_NOT_FOUND = "Produto não encontrado.";
    public const string INVALID_PRODUCT_ID = "ID do produto inválido.";
    public const string PRODUCT_CREATED_SUCCESSFULLY = "Produto criado com sucesso.";
    public const string PRODUCT_UPDATED_SUCCESSFULLY = "Produto atualizado com sucesso.";
    public const string PRODUCT_DELETED_SUCCESSFULLY = "Produto excluído com sucesso.";
    public const string PRODUCTS_RETRIEVED_SUCCESSFULLY = "Produtos listados com sucesso.";
    public const string PRODUCT_NAME_REQUIRED = "O nome do produto é obrigatório.";
    public const string PRODUCT_NAME_MAX_LENGTH = "O nome do produto deve ter no máximo 100 caracteres.";
    public const string PRODUCT_DESCRIPTION_REQUIRED = "A descrição do produto é obrigatória.";
    public const string PRODUCT_DESCRIPTION_MAX_LENGTH = "A descrição do produto deve ter no máximo 500 caracteres.";
    public const string PRODUCT_PRICE_INVALID = "O preço do produto deve ser maior que zero.";
    public const string PRODUCT_STOCK_QUANTITY_INVALID = "A quantidade em estoque não pode ser negativa.";
    public const string PRODUCT_IMAGE_URL_MAX_LENGTH = "A URL da imagem deve ter no máximo 2048 caracteres.";
}
