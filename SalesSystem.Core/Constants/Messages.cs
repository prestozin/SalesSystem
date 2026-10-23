
namespace SalesSystem.Core.Constants;

public class Messages
{
    public const string PRODUCT_NOT_FOUND = "Produto não encontrado.";
    public const string PRODUCT_INVALID_ID = "ID do produto inválido.";
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
    public const string PRODUCT_RETRIEVED_SUCCESSFULLY = "Produto encontrado com sucesso.";
    public const string PRODUCT_DEACTIVATED_SUCCESSFULLY = "Produto desativado com sucesso.";
    public const string PRODUCT_ALREADY_DEACTIVATED = "O produto já está desativado.";
    public const string PRODUCT_STOCK_LESS_THAN_RESERVED = "O estoque não pode ser menor que a quantidade reservada.";
    public const string PRODUCT_RESTOCK_QUANTITY_INVALID = "A quantidade para reposição deve ser maior que zero.";

    public const string ORDER_ITEM_QUANTITY_INVALID = "A quantidade do produto no pedido deve ser maior que zero.";
    public const string ORDER_ITEMS_REQUIRED = "O pedido deve conter pelo menos um produto.";
    public const string ORDER_CREATED_SUCCESSFULLY = "Pedido criado com sucesso.";
    public const string ORDER_PRODUCT_NOT_FOUND = "Um ou mais produtos do pedido não foram encontrados.";
    public const string ORDER_PRODUCT_INACTIVE = "Um ou mais produtos do pedido estão inativos.";
    public const string ORDER_INSUFFICIENT_STOCK = "Estoque insuficiente para um ou mais produtos do pedido.";
    public const string ORDER_DUPLICATE_PRODUCTS = "O pedido não pode conter produtos repetidos.";
}
